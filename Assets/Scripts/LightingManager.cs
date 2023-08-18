using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //Scene References
    [SerializeField] private Light sunLight;
    [SerializeField] private Light moonLight;
    [SerializeField] private LightingPreset sunPreset;
    [SerializeField] private LightingPreset moonPreset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [SerializeField, Range(0, 24000)] private float fixedTimeOfDay;
    [SerializeField] private bool isDay;

    void Start()
    {
        moonLight.transform.rotation = Quaternion.Euler(new Vector3(sunLight.transform.rotation.x - 180f, sunLight.transform.rotation.y, sunLight.transform.rotation.z));
    }

    void Update()
    {
        if (sunPreset == null)
            return;

        if (Application.isPlaying)
        {
            fixedTimeOfDay += Time.fixedDeltaTime;
            TimeOfDay = (fixedTimeOfDay / 1000) * 3;
            TimeOfDay %= 24; //Modulus to ensure always between 0-24
            UpdateLighting(TimeOfDay / 24f);
            if (TimeOfDay >= 8 && TimeOfDay < 16 && !isDay) { isDay = true; }
            else if (isDay) { isDay = false; }
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }


    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = sunPreset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = sunPreset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (sunLight != null)
        {
            sunLight.color = sunPreset.DirectionalColor.Evaluate(timePercent);
            sunLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
        if (moonLight != null)
        {
            moonLight.color = moonPreset.DirectionalColor.Evaluate(timePercent);
            moonLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 270f, 170f, 0));
        }
    }

    //Try to find a directional light to use if we haven't set one
    void OnValidate()
    {
        if (sunLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            sunLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    sunLight = light;
                    return;
                }
            }
        }
    }
}