using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//spawns fish when game opens
public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishObj;
    private string[] fishNames = { "clown", "tang", "sturg", "damsel", "angel", "card", "wrass", "axol", "star", "horse" };
    private Collections c;
    public int numFishLeft = 0; //to show at beginning of game
    TimeSpan timeSinceLastFed;

    void Start()
    {
        c = GameObject.Find("GameController").GetComponent<Collections>();
        spawnFish();
    }


    //spawns fish from Collections.cs and sets their lastTimeFed
    //removes
    public void spawnFish()
    {
        List<DateTime> lis;
        for (int i = 0; i < fishNames.Length; i++)
        {
            lis = (List<DateTime>)c.fish[fishNames[i]];
            if (lis.Capacity > 0)
            {
                foreach (DateTime d in lis)
                {
                    timeSinceLastFed = System.DateTime.Now.Subtract(fishObj[i].GetComponent<FishHunger>().lastTimeFed);
                    if (timeSinceLastFed.Hours > 96)
                    {
                        c.removeFish(fishNames[i]); //removes fish from collections
                        numFishLeft += 1;
                    }
                    else
                    {
                        Instantiate(fishObj[i], new Vector3(i * 2, 0, 0), Quaternion.identity);
                        fishObj[i].GetComponent<FishHunger>().lastTimeFed = d;
                    }
                }
            }

        }
    }

    void Update()
    {

    }
}
