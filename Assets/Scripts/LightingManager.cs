using UnityEngine;
using TMPro;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    /// <summary>
    /// This controls the lighting/time e.g. the sun and the lights during the night
    /// </summary>
    //Scene References
    public Light sunLight;
    public Light moonLight;
    public LightingPreset sunPreset;
    public LightingPreset moonPreset;
    public GameObject daysLeftText;

    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [SerializeField, Range(0, 240000)] private float fixedTimeOfDay;
    [SerializeField] private bool isDay;
    public int numberOfDays;

    void Start()
    {
        // Sets the moon(directional light) to the opposite rotation of the rotation
        moonLight.transform.rotation = Quaternion.Euler(new Vector3(sunLight.transform.rotation.x - 180f, sunLight.transform.rotation.y, sunLight.transform.rotation.z));
        numberOfDays = 0; // Sets the number of days to 0 at the start of the game
        isDay = true; // Sets isDay to true as the game starts in the day
    }

    void Update()
    {
        if (sunPreset == null)
            return;
        // Makes sure that the sun preset has loaded before attempting to run the rest of the script

        if (Application.isPlaying)
        {
            // If the script is running
            fixedTimeOfDay += Time.fixedDeltaTime;
            TimeOfDay = (fixedTimeOfDay / 1000) * 3;
            TimeOfDay %= 24; //Modulus to ensure always between 0-24
            UpdateLighting(TimeOfDay / 24f);
            if (TimeOfDay >= 6.5 && TimeOfDay < 17.5 && !isDay) {
                // If the time of day is after 6:30am, before 5:30pm and not day
                isDay = true; // Set to day
                numberOfDays++; // Increase the number of days
                daysLeftText.GetComponent<TextMeshPro>().text = "Days left: " + (10 - numberOfDays).ToString(); // Shows the number of days left on a sign next to Mr Beast
            }
            else if (isDay && TimeOfDay >= 17.5) { isDay = false; } // If night begins set day to false
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f); // Updates the lighting based on the current time of day
        }
    }


    private void UpdateLighting(float timePercent)
    {
        // Set ambient and fog
        RenderSettings.ambientLight = sunPreset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = sunPreset.FogColor.Evaluate(timePercent);

        // If the directional light is set then rotate and set it's color
        if (sunLight != null)
        {
            sunLight.color = sunPreset.DirectionalColor.Evaluate(timePercent);
            sunLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

        // Sets the moon to the opposite of the sun if it is set
        if (moonLight != null)
        {
            moonLight.color = moonPreset.DirectionalColor.Evaluate(timePercent);
            moonLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 270f, 170f, 0));
        }
    }
}