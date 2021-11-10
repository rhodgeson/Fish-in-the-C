using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json.Linq;

//spawns fish when game opens
public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishObj;
    private string[] fishNames = { "clown", "tang", "sturg", "damsel", "angel", "axol", "star" };
    private Collections c;

    //number of fish that have LEFT the tank 
    public int numFishLeft = 0; //to show at beginning of game
    public bool fishJoined; //number of fish that have joined the tank
    TimeSpan timeSinceLastFed;
    private bool newFishAdded = false;
    private bool tableFilled = false;
    private bool starExists = false; //true if there is already a starfish spawned
    private bool axoExists = false;
    public Hashtable newFish = new Hashtable(){
        {"clown", new List<DateTime>()},
        {"tang", new List<DateTime>()},
        {"sturg", new List<DateTime>()},
        {"damsel", new List<DateTime>()},
        {"angel", new List<DateTime>()},
        {"axol", new List<DateTime>()},
        {"star", new List<DateTime>()}
    };
    void Awake() //changed to awake to debug
    {
        c = GameObject.Find("GameController").GetComponent<Collections>();
        Debug.Log("FS AWAKE");
        if (c == null)
        {
            Debug.Log("NULL");
        }
        else
        {
            Debug.Log("NOT NULL");
        }
    }
    void Update()
    {
        // Debug.Log("collections hastable list is type: " + c.fish["clown"].GetType().Name);
        // if (!tableFilled)
        // {
        //     refillTable();
        //     spawnFish();
        //     tableFilled = true;
        // }
    }
    public void refillTable()
    {
        JArray ja;
        JValue jv;
        List<DateTime> curList;
        print(c.fish["tang"]);
        print(c.fish[fishNames[0]].GetType());
        for (int i = 0; i < fishNames.Length; i++)
        {
            if (c.fish[fishNames[i]] == null) 
            {
                Debug.Log("NULL");
            }
            else
            {
                Debug.Log("NOT NULL");
            }
        ja = (JArray)c.fish[fishNames[i]];
            print(c.fish[fishNames[i]]);
            //looking at jarray - equivalent to list of datetimes
            for (int j = 0; j < ja.Count; j++)
            {
                jv = (JValue)ja[j]; //grab current jvalue
                                    //grab corresponding hashtable list
                curList = (List<DateTime>)newFish[fishNames[i]];
                //fill in with DateTime
                curList.Add((DateTime)jv.Value);
                Debug.Log("adding to hashtable: " + jv.Value);
            }
        }
        //replace table in collections
        c.fish = newFish;
    }
    //spawns fish from Collections.cs and sets their lastTimeFed
    //if fish has been fed in last 24 hours and less than 6 fish are in tank, spawn new fish
    //if tank is empty, spawn new fish

    public void spawnFish()
    {
        List<DateTime> lis;
        DateTime dt;
        int totalFish = 0;
        fishJoined = false;
        int indexToSpawn;
        for (int k = 0; k < fishNames.Length; k++)
        {
            lis = (List<DateTime>)c.fish[fishNames[k]];
            if (k == 6 && lis.Count > 0)
            {
                starExists = true;
            }
            else if (k == 5 && lis.Count > 0)
            {
                axoExists = true;
            }
            totalFish += lis.Count;
        }
        //loop looks thru Collections and adds fish if needed
        for (int i = 0; i < fishNames.Length; i++)
        {
            lis = (List<DateTime>)c.fish[fishNames[i]];
            if (lis.Capacity > 0)
            {
                for (int j = 0; j < lis.Count; j++)
                {
                    dt = (DateTime)lis[j];
                    timeSinceLastFed = System.DateTime.Now.Subtract(dt);
                    if (timeSinceLastFed.Hours < 24 && totalFish < 6 && !fishJoined)
                    {
                        totalFish += 1;
                        if (axoExists)
                        {
                            indexToSpawn = UnityEngine.Random.Range(0, fishNames.Length - 2);
                        }
                        else if (starExists)
                        {
                            indexToSpawn = UnityEngine.Random.Range(0, fishNames.Length - 1);
                        }
                        else
                        {
                            indexToSpawn = UnityEngine.Random.Range(0, fishNames.Length);
                            if (indexToSpawn == 6)
                            {
                                starExists = true;
                            }
                            else if (indexToSpawn == 5)
                            {
                                axoExists = true;
                            }
                        }
                        c.addFish(fishNames[indexToSpawn]);
                        fishJoined = true;
                    }
                }
            }
            else
            {
                if (!fishJoined && totalFish < 6)
                {
                    totalFish += 1;
                    // numFish += 1;
                    // c.addFish(fishNames[UnityEngine.Random.Range(0, fishNames.Length)]);
                    if (axoExists)
                    {
                        indexToSpawn = UnityEngine.Random.Range(0, fishNames.Length - 2);
                    }
                    if (starExists)
                    {
                        indexToSpawn = UnityEngine.Random.Range(0, fishNames.Length - 1);
                    }
                    else
                    {
                        indexToSpawn = UnityEngine.Random.Range(0, fishNames.Length);
                        if (indexToSpawn == 6)
                        {
                            starExists = true;
                        }
                        else if (indexToSpawn == 5)
                        {
                            axoExists = true;
                        }
                    }
                    c.addFish(fishNames[indexToSpawn]);
                    fishJoined = true;
                }

            }
        }

        //looks thru collections, spawns fish, and removes certain fish
        for (int i = 0; i < fishNames.Length; i++)
        {
            lis = (List<DateTime>)c.fish[fishNames[i]];
            if (lis.Capacity > 0)
            {
                for (int j = 0; j < lis.Count; j++)
                {
                    dt = (DateTime)lis[j];
                    timeSinceLastFed = System.DateTime.Now.Subtract(dt);
                    if (timeSinceLastFed.Hours > 96)
                    {
                        c.removeFish(fishNames[i]); //removes fish from collections
                        numFishLeft += 1;
                        if (i == 5)
                        {
                            axoExists = false;
                        }
                        else if (i == 6)
                        {
                            starExists = false;
                        }
                    }
                    else
                    {
                        if (i == 5)
                        {
                            //axolotl
                            Instantiate(fishObj[i], new Vector3(2, -2, -7), Quaternion.identity);
                        }
                        else if (i == 6)
                        {
                            //starfish
                            Instantiate(fishObj[i], new Vector3(0, 2, -7), Quaternion.identity);
                        }
                        else
                        {
                            //all other fish
                            Instantiate(fishObj[i], new Vector3(0, 1, -7), Quaternion.identity);
                        }

                        fishObj[i].GetComponent<FishHunger>().lastTimeFed = dt;
                    }
                }
            }
        }
        // Debug.Log("Total fish:" + totalFish);
    }

    //     void OnApplicationQuit()
    //     {
    //         Debug.Log("hashtable is now" + c.fish["angel"].GetType().Name);
    //         //     Debug.Log("PRINTING");
    //         //     List<DateTime> lis;
    //         //     for (int k = 0; k < fishNames.Length; k++)
    //         //     {
    //         //         lis = (List<DateTime>)c.fish[fishNames[k]];
    //         //         foreach (DateTime d in lis)
    //         //         {
    //         //             Debug.Log(fishNames[k]);
    //         //         }
    //         //     }
    //     }
}
