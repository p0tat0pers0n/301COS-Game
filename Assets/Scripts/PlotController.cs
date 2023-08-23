using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotController : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject player;

    private int growthState, growthTime, requiredGrowthTime, intervalGrowth;
    private double fixedGrowthTime;
    private bool planted, withinRange, watered;
    private LayerMask mask;
    public List<Material> materials = new List<Material>(3);
    // Start is called before the first frame update
    void Start()
    {
        growthState = 0;
        growthTime = 0;
        fixedGrowthTime = 0;
        intervalGrowth = 5;
        planted = false;
        watered = false;
        mask = LayerMask.GetMask("FarmPlot");
    }

    private void OnMouseOver()
    {
        if (!planted || !watered)
        {
            Ray ray;
            RaycastHit hit;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.distance <= 5)
                {
                    withinRange = true;
                    if (!planted) { interactText.GetComponent<Text>().text = "Plant Potatoes"; }
                    if (!watered) { interactText.GetComponent<Text>().text = "Water Crops"; }
                }
            }
        }
    }

    private void OnMouseExit()
    {
        withinRange = false;
        interactText.GetComponent<Text>().text = "";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && withinRange && !planted)
        {
            planted = true;
            withinRange = false;
            interactText.GetComponent<Text>().text = "";
            gameObject.GetComponent<MeshRenderer>().material = materials[0];
        }

        if (Input.GetMouseButtonDown(0) && withinRange && !watered)
        {
            /*if (player)
            {

            }*/
        }
    }

    void FixedUpdate()
    {
        if (planted && watered)
        {
            fixedGrowthTime++;
            growthTime = (int)fixedGrowthTime / 50;
            requiredGrowthTime = intervalGrowth * growthState;

            text.GetComponent<TextMesh>().text = growthTime.ToString();

            switch (growthState)
            {
                case 0:
                    if (growthTime >= requiredGrowthTime)
                    {
                        growthState = 1;
                        gameObject.GetComponent<MeshRenderer>().material = materials[growthState];
                    }
                    break;
                case 1:
                    if (growthTime >= requiredGrowthTime)
                    {
                        growthState = 2;
                        gameObject.GetComponent<MeshRenderer>().material = materials[growthState];
                    }
                    break;
                case 2:
                    if (growthTime >= requiredGrowthTime)
                    {
                        growthState = 3;
                        gameObject.GetComponent<MeshRenderer>().material = materials[growthState];
                    }
                    break;
            }
        }
    }
}
