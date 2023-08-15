using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public int currentTime, numberOfDays;
    public GameObject sun;

    private double rotationNeeded, previousRotation;
    private int previousTime;
    private bool isDay, isNight;
    // Start is called before the first frame update
    void Start()
    {
        isDay = true;
        isNight = false;
        numberOfDays = 0;
        currentTime = (int)Time.time;
        previousTime = -1;
        previousRotation = 0;
        Time.timeScale = 10;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = (int)Time.time - (numberOfDays * 600);

        if (previousTime != currentTime)
        {
            previousTime = currentTime;
            if (currentTime % 300 == 0 && currentTime != 0)
            {
                Debug.Log("YES");
                if (!isDay)
                {
                    numberOfDays++;
                }
                isDay = !isDay;
                isNight = !isNight;
            }

            rotationNeeded = -(600 / 360) * currentTime;
            sun.transform.Rotate(new Vector3((float)rotationNeeded - (float)previousRotation, 0, 0));
            previousRotation = rotationNeeded;
        }
    }
}
