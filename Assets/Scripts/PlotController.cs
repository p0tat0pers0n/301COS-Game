using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotController : MonoBehaviour
{
    [SerializeField] private GameObject text;

    private int growthState, growthTime, requiredGrowthTime, intervalGrowth;
    private double fixedGrowthTime;
    private bool planted;
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
        mask = LayerMask.GetMask("FarmPlot");
    }
    // To start normal farm 

    private void OnMouseOver()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance <= 5)
            {   
                Debug.Log("Player Within Range");
            }
        }
    }

    void FixedUpdate()
    {
        if (planted)
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
