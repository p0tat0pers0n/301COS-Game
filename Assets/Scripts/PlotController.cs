using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotController : MonoBehaviour
{
    [SerializeField] private GameObject plotContainer;
    private int growthState;
    private int growthTime
    // Start is called before the first frame update
    void Start()
    {
        growthState = 0;

    }

    void FixedUpdate()
    {
        growthTime++;
    }
}
