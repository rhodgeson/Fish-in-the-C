using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{

    public GameObject collectedFish;
    GameObject fishNote, dayLightsParent, nightLightsParent, dayLights, nightLights;
    Button fishNoteBtn;
    int fishBtnCounter, fishNoteBtnCounter, lightBtnCounter;
    private FoodSpawn fs;
    private Collections coll;
    Button plant, shortD, tallD, sand;
    private bool editing = false;
    void Start()
    {
        plant = GameObject.Find("Plant").GetComponent<Button>();
        shortD = GameObject.Find("ShortDecor").GetComponent<Button>();
        tallD = GameObject.Find("TallDecor").GetComponent<Button>();
        sand = GameObject.Find("Sand").GetComponent<Button>();
        coll = GameObject.Find("GameController").GetComponent<Collections>();
        fs = GameObject.Find("FoodSpawner").GetComponent<FoodSpawn>();
        fishNote = GameObject.Find("FishUpdateNote");
        fishNoteBtn = fishNote.GetComponentInChildren<Button>();
        //dayLightsParent = GameObject.Find("Daylight");
        // dayLights = dayLightsParent.transform.Find("Lights").gameObject;
        // nightLightsParent = GameObject.Find("Nightlight");
        // nightLights = nightLightsParent.transform.Find("Lights").gameObject;
        plant.enabled = false;
        shortD.enabled = false;
        tallD.enabled = false;
        sand.enabled = false;

    }

    public void onFoodPress() //Leslyanne
    {
        FoodSpawn fs = GameObject.Find("FoodSpawner").GetComponent<FoodSpawn>();
        if (!fs.feeding)
        {
            fs.feeding = true;
        }
        else
        {
            fs.feeding = false;
        }
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
        dayLightsParent = GameObject.Find("Daylight");
        dayLights = dayLightsParent.transform.Find("Lights").gameObject;

        nightLightsParent = GameObject.Find("Nightlight");
        nightLights = nightLightsParent.transform.Find("Lights").gameObject;

        lightBtnCounter++;
        if (lightBtnCounter % 2 == 1)
        {
            nightLights.gameObject.SetActive(false);
            dayLights.gameObject.SetActive(true);

        }
        else
        {
            dayLights.gameObject.SetActive(false);
            nightLights.gameObject.SetActive(true);
        }

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
            sand.enabled = true;
        }
        else
        {
            editing = false;
            plant.enabled = false;
            shortD.enabled = false;
            tallD.enabled = false;
            sand.enabled = false;
        }
    }

    public void onShortDecPress()
    {
        Collections coll = GameObject.Find("GameController").GetComponent<Collections>();
        coll.changeShortDec();
    }

    public void onTallDecPress()
    {
        Collections coll = GameObject.Find("GameController").GetComponent<Collections>();
        coll.changeTallDec();
    }

    public void onPlantPress()
    {
        Collections coll = GameObject.Find("GameController").GetComponent<Collections>();
        coll.changePlant();
    }

    public void onSandPress()
    {
        Collections coll = GameObject.Find("GameController").GetComponent<Collections>();
        coll.changeSand();
    }

    public void onOkayBtnPressed()
    {
        //fishNoteBtnCounter++;        

        fishNote.gameObject.SetActive(false);

    }
}
