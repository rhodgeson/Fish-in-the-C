using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    FoodSpawner feed;
    public GameObject collectedFish;
    int fishBtnCounter;

    Collections c;
    Button plant, shortD, tallD;
    private bool editing = false;
    void Start()
    {
        feed = GameObject.Find("FoodSpawner").GetComponent<FoodSpawner>();
        c = GameObject.Find("GameController").GetComponent<Collections>();
        plant = GameObject.Find("Plant").GetComponent<Button>();
        shortD = GameObject.Find("ShortDecor").GetComponent<Button>();
        tallD = GameObject.Find("TallDecor").GetComponent<Button>();
    }

    public void onFoodPress()
    {
        Debug.Log("food!");
        StartCoroutine(feed.SpawnFood(1f));
    }

    public void onFishPress()
    {
        fishBtnCounter++;
        if (fishBtnCounter % 2 == 1)
        {
            collectedFish.gameObject.SetActive(true);
        }
        else
        {
            collectedFish.gameObject.SetActive(false);
        }
        Debug.Log("collected fish!");

    }

    public void onMusicPress()
    {
        Debug.Log("music!");
    }

    public void onSettingsPress()
    {
        Debug.Log("settings");
    }

    public void onLightPress()
    {
        Debug.Log("set the mood!");
    }

    public void onEditPress()
    {
        Debug.Log("editing");
        if (!editing)
        {
            editing = true;
            plant.enabled = true;
            shortD.enabled = true;
            tallD.enabled = true;
        }
        else
        {
            editing = false;
            plant.enabled = false;
            shortD.enabled = false;
            tallD.enabled = false;
        }
    }

    public void onShortDecPress()
    {
        c.changeShortDec();
    }

    public void onTallDecPress()
    {
        c.changeTallDec();
    }

    public void onPlantPress()
    {
        c.changePlant();
    }
}
