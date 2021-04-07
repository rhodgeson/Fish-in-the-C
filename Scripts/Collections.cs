using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//class keeps track of what is within the tank (can be used for saving purposes)
//tracks&changes plant decor
//keeps track of fish in tank
[System.Serializable]
public class Collections : MonoBehaviour
{
    private int shortDec, tallDec, plant; // to be saved
    //maps type of fish to a list
    //length of list = how many fish of that type are present
    //each DateTime in the list is the lastTimeFed of the fish of that type
    public Hashtable fish = new Hashtable(){
        {"clown", new List<DateTime>()},
        {"tang", new List<DateTime>()},
        {"sturg", new List<DateTime>()},
        {"damsel", new List<DateTime>()},
        {"angel", new List<DateTime>()},
        {"card", new List<DateTime>()},
        {"wrass", new List<DateTime>()},
        {"axol", new List<DateTime>()},
        {"star", new List<DateTime>()},
        {"horse", new List<DateTime>()}
    }; //to be saved


    private SpriteRenderer sprShort, sprTall, sprPlant;
    public Sprite[] shortD;
    public Sprite[] tallD;
    public Sprite[] plants;

    void Start()
    {
        GameObject.Find("ShortDecor").GetComponent<SpriteRenderer>();
        GameObject.Find("TallDecor").GetComponent<SpriteRenderer>();
        GameObject.Find("Plant").GetComponent<SpriteRenderer>();
    }
    //adds fish of type fishName and sets its lastTimeFed to now
    public void addFish(string fishName)
    {
        //list of lasttimefeds for present fish of certain type
        List<DateTime> list = (List<DateTime>)fish[fishName];
        list.Add(System.DateTime.Now);
        //fish[fishName] = (int)fish[fishName] + 1;

    }

    //removes fish of type fishName with oldest lastTimeFed
    public void removeFish(string fishName)
    {
        List<DateTime> list = (List<DateTime>)fish[fishName];
        DateTime oldest = System.DateTime.Now;
        if (list.Capacity > 0)
        {
            foreach (DateTime dt in list)
            {
                if (DateTime.Compare(dt, oldest) < 0)
                {
                    //t1 earlier than t2
                    oldest = dt;
                }
            }
            list.Remove(oldest);
        }

    }

    public void changeShortDec()
    {
        //switch to next sprite
        if (shortDec + 1 < shortD.Length)
        {
            shortDec += 1;
            sprShort.sprite = shortD[shortDec];
        }
    }

    public void changeTallDec()
    {
        //switch to next sprite
        if (tallDec + 1 < tallD.Length)
        {
            tallDec += 1;
            sprTall.sprite = tallD[tallDec];
        }
    }

    public void changePlant()
    {
        //switch to next sprite
        if (plant + 1 < plants.Length)
        {
            plant += 1;
            sprPlant.sprite = plants[plant];
        }
    }

}
