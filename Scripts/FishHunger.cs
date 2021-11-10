using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class FishHunger : MonoBehaviour
{
    public DateTime lastTimeFed;
    [SerializeField]
    private bool hungry = false;
    SpriteRenderer sprite;
    DateTime newTimeFed;
    TimeSpan difference;

    TimeSpan timeSinceLastFed;
    int hoursSinceLastFed;
    private bool fedDuringGameOpen = false;
    Collections col;
    DateTime curRefTime;
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        difference = GameObject.Find("GameController").GetComponent<SaveLoadTime>().difference;
        //lastTimeFed = System.DateTime.Now;
        col = GameObject.Find("GameController").GetComponent<Collections>();
        // DateTime date1 = DateTime.Parse("2021-04-13T22:57:23.3474764-05:00",
        //                   System.Globalization.CultureInfo.InvariantCulture);
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

        if(hungry == true)
        {
            sprite.color = Color.gray;
        }
        else
        {
            sprite.color = Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            feed();
        }
    }

    public void feed()
    {
        int minIndex = 0;
        int i = 0;
        newTimeFed = System.DateTime.Now;
        curRefTime = System.DateTime.Now;
        List<DateTime> lis = (List<DateTime>)col.fish[gameObject.tag];
        if (lis.Capacity > 0)
        {
            for (i = 0; i < lis.Count; i++)
            {
                //update oldest time of that fish in the table
                if (DateTime.Compare((DateTime)lis[i], curRefTime) < 0)
                {
                    //current time in table is the oldest time
                    minIndex = i;
                    curRefTime = lis[i];
                }
                // if (DateTime.Compare(lastTimeFed, (DateTime)lis[i]) == 0)
                // {
                //     //found same DateTime
                //     lis[i] = newTimeFed;
                //     break;
                // }
            }
        }
        lis[minIndex] = newTimeFed;
        lastTimeFed = newTimeFed;
        fedDuringGameOpen = true;
        hungry = false;

        //change sprite
    }
}
