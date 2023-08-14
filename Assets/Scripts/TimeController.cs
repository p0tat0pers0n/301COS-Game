using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public int currentTime;
    public GameObject sun;

    private double rotationNeeded;
    private int numberOfDays;
    private bool isDay, isNight;
    // Start is called before the first frame update
    void Start()
    {
        isDay = true;
        isNight = false;
        numberOfDays = 0;
        currentTime = (int)Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = (int)Time.time - (numberOfDays * 600);
        if (currentTime % 300 == 0 && currentTime != 0)
        {
            if (!isDay)
            {
                numberOfDays++;
            }
            isDay = !isDay;
            isNight = !isNight;
        }

        if (currentTime % 3 == 0)
        {
            rotationNeeded = currentTime / 360;
            if (rotationNeeded >= 360) { rotationNeeded = 0; }
            sun.transform.rotation = Quaternion.Euler(new Vector3((float)rotationNeeded, 0, 0));
        }
    }
}
