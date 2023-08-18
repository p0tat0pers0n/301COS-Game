using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotController : MonoBehaviour
{
    [SerializeField] private GameObject plotContainer;
    private int growthState;
    // Start is called before the first frame update
    void Start()
    {
        growthState = 0;
    }

    void checkGrowth()
    {
        if (growthState == 0)
        {
            for (int i = 0; i < plotContainer.transform.childCount; i++)
            {
                //plotContainer.transform.GetChild(i).
            }
        }
    }
}
