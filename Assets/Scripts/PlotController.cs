using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotController : MonoBehaviour
{
    [SerializeField] private GameObject plotContainer;
    private int growthState, growthTime, requiredGrowthTime;
    private double fixedGrowthTime;
    private bool planted;
    public List<Material> materials = new List<Material>(3);
    // Start is called before the first frame update
    void Start()
    {
        growthState = 0;
        growthTime = 0;
        fixedGrowthTime = 0;
        requiredGrowthTime = 120;
        planted = false;
    }

    void FixedUpdate()
    {
        if (planted)
        {
            fixedGrowthTime++;
            growthTime = (int)fixedGrowthTime / 50;
            switch (growthState)
            {
                case 1:
                    if (growthTime >= requiredGrowthTime)
                    {
                        growthState = 2;
                    }
                    break;
                case 2:
                    if (growthTime >= requiredGrowthTime)
                    {
                        growthState = 3;
                    }
                    break;
                case 3:
                    if (growthTime >= requiredGrowthTime)
                    {
                        growthState = 4;
                    }
                    break;
            }
        }
    }
}
