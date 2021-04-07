using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishHunger : MonoBehaviour
{
    public DateTime lastTimeFed;
    private bool hungry = false;

    TimeSpan difference;

    TimeSpan timeSinceLastFed;
    int hoursSinceLastFed;
    private bool fedDuringGameOpen = false;
    Collections col;
    void Start()
    {
        difference = GameObject.Find("GameController").GetComponent<SaveLoadTime>().difference;
        lastTimeFed = System.DateTime.Now;
        col = GameObject.Find("GameController").GetComponent<Collections>();
    }

    void Update()
    {
        timeSinceLastFed = System.DateTime.Now.Subtract(lastTimeFed);
        //time since game opened + hours that passed OR time since last fed + 0
        hoursSinceLastFed = timeSinceLastFed.Hours + (fedDuringGameOpen ? difference.Hours : 0);

        if (hoursSinceLastFed > 48)
        {
            hungry = true;
            //change sprite 

            // if (hoursSinceLastFed < 96)
            // {
            //     hungry = true; //fish becomes hungry
            // }
            // else
            // {
            //     Debug.Log("im leaving"); //fish leaves
            //     col.removeFish(gameObject.tag);
            // }
        }
    }

    void feed()
    {
        lastTimeFed = System.DateTime.Now;
        fedDuringGameOpen = true;
        hungry = false;
        //change sprite
    }
}
