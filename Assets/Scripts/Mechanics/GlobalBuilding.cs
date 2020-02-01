using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalBuilding : MonoBehaviour
{
    public static List<Building> buildingArr = new List<Building>();
    public GameObject[] buttonArr;
    public GameObject[] displayObjectsArr;

    private void Start()
    {
        buildingArr.Add(new Cursor("Cursor", 15, 0.2f, displayObjectsArr[0]));
        buildingArr.Add(new Building("Grandma", 100, 0.8f, displayObjectsArr[1]));
        buildingArr.Add(new Building("Factory", 500, 4.0f, displayObjectsArr[2]));
        buildingArr.Add(new Building("Mine", 2000, 10.0f, displayObjectsArr[3]));
        buildingArr.Add(new Building("Shipment", 7000, 20.0f, displayObjectsArr[4]));
        buildingArr.Add(new Building("AlchemyLab", 50000, 100.0f, displayObjectsArr[5]));
        buildingArr.Add(new Building("Portal", 1000000, 1333.2f, displayObjectsArr[6]));
        buildingArr.Add(new Building("TimeMachine", 123456789, 24691.2f, displayObjectsArr[7]));
        buildingArr.Add(new EldersPledge("EldersPledge", 666666, 0.0f, displayObjectsArr[8]));
    }

    private void Update()
    {
        DisplayBulidingPanel();
        DisplayBuildingClick();
    }

    void DisplayBulidingPanel()
    {
        for (int i = 0; i < buttonArr.Length; i++)
        {
            GameObject ownedNumber = buttonArr[i].transform.Find("OwnedNumber").gameObject;
            if (buildingArr[i].currentNumber == 0)
            {
                ownedNumber.GetComponent<Text>().text = "";
            }
            else
            {
                ownedNumber.GetComponent<Text>().text = Mathf.CeilToInt(buildingArr[i].currentNumber).ToString("n0");
            }

            GameObject price = buttonArr[i].transform.Find("Price").gameObject;
            price.GetComponent<Text>().text = Mathf.CeilToInt(buildingArr[i].currentPrice).ToString("n0");
            if (GlobalCookie.cookieCount < buildingArr[i].currentPrice)
            {
                price.GetComponent<Text>().color = Color.red;
            }
            else
            {
                price.GetComponent<Text>().color = Color.black;
            }

            GameObject itemName = buttonArr[i].transform.Find("ItemName").gameObject;
            if (GlobalCookie.cookieCount < buildingArr[i].currentPrice)
            {
                itemName.GetComponent<Text>().color = Color.red;
            }
            else
            {
                itemName.GetComponent<Text>().color = Color.black;
            }

            GameObject itemDescription = buttonArr[i].transform.Find("ItemDescription").gameObject;
            if (GlobalCookie.cookieCount < buildingArr[i].currentPrice)
            {
                itemDescription.GetComponent<Text>().color = Color.red;
            }
            else
            {
                itemDescription.GetComponent<Text>().color = Color.black;
            }

            if (GlobalCookie.cookieCount < buildingArr[i].currentPrice)
            {
                buttonArr[i].GetComponent<Image>().color = Color.grey;
            }
            else
            {
                buttonArr[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    void DisplayBuildingClick()
    {
        for (int i = 0; i < buildingArr.Count; i++)
        {
            if(buildingArr[i].currentNumber > 0)
            {
                buildingArr[i].DisplayClick();
            }
            
        }
    }

}


public class Building
{
    public string name;
    public int initPrice;
    public int currentNumber = 0;
    public int currentPrice;
    public float initCPS;
    public float CPS;

    protected bool isPledged = false;
    protected float period = 0;
    protected float nextActionTime = 0;
    protected List<GameObject> displayArr = new List<GameObject>();
    protected List<float> randomSeeds = new List<float>();
    protected GameObject displayObject;

    public Building(string name, int initPrice, float cps, GameObject displayObject)
    {
        this.name = name;
        this.initPrice = initPrice;
        this.currentPrice = initPrice;
        this.initCPS = cps;
        if (this.nextActionTime == 0)
        {
            nextActionTime = Time.time + period;
        }
        this.displayObject = displayObject;
    }
    public virtual void Buy()
    {
        this.currentNumber += 1;
        this.currentPrice = Mathf.CeilToInt(this.initPrice * Mathf.Pow(1.1f, currentNumber));
        this.CPS = this.currentNumber * initCPS;
        this.period = 1.0f;
        randomSeeds.Add(Random.Range(0.0f, 1.0f));
        for (int i = 0; i < randomSeeds.Count; i++)
        {
            randomSeeds[i] = Random.Range(0.0f, 1.0f);
        }

        GameObject buildingBox = new GameObject();
        buildingBox.name = this.name + "Box";
        GameObject buildingDisplay = GameObject.Instantiate(Resources.Load("Prefabs/"+this.name), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        buildingDisplay.transform.SetParent(buildingBox.transform);
        displayArr.Add(buildingBox);
        Display();
    }

    public virtual void Display()
    {
        for (int j = 0; j < currentNumber; j++)
        {
            float x = randomSeeds[j] * 20 + (j%10) * 20;
            float y = randomSeeds[j] * 20 + Mathf.Floor((j / 10) * -24.0f);
            displayArr[j].transform.SetParent(displayObject.transform);
            displayArr[j].transform.localPosition = new Vector3(x, y, 0);
        }
    }

    public virtual void DisplayClick()
    {
        if(Time.time >= nextActionTime)
        {
            /*
            this.period = 5.0f / currentNumber;
            this.displayObject.GetComponentInChildren<Text>().text = "+" + initCPS * 5;
            nextActionTime = Time.time + period;
            displayObject.GetComponentInChildren<Text>().GetComponent<Animator>().SetTrigger("Clicked");
            */
        }
    } 

};

public class Cursor : Building
{
    public Cursor(string name, int initPrice, float cps, GameObject displayObject) : base(name, initPrice, cps, displayObject)
    {
    }
    protected int clickCounter = 0;

    public override void Buy()
    {
        this.currentNumber += 1;
        this.currentPrice = Mathf.CeilToInt(this.initPrice * Mathf.Pow(1.1f, currentNumber));
        this.CPS = countCPS();
        this.period = 5.0f / currentNumber;
        GameObject cursorBox = new GameObject();
        cursorBox.name = "cursorBox";
        GameObject cursor = GameObject.Instantiate(Resources.Load("Prefabs/Cursor"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        cursor.transform.SetParent(cursorBox.transform);
        displayArr.Add(cursorBox);
        Display(); 
    }

    float countCPS()
    {
        if (isPledged)
        {
            return Mathf.Pow(currentNumber, 2) * 0.3f;
        }
        return 0.2f * currentNumber;
    }

    public override void Display()
    {
        float radius = 70.0f;
        float deltaAngle = 2 * Mathf.PI / currentNumber;
        for (int j = 0; j < currentNumber; j++)
        {
            float d_x = -(radius * Mathf.Sin(deltaAngle * j));
            float d_y = -(radius * Mathf.Cos(deltaAngle * j));
            displayArr[j].transform.SetParent(displayObject.transform);
            displayArr[j].transform.localPosition = new Vector3(d_x, d_y, 0);
            displayArr[j].transform.localRotation = Quaternion.Euler(0, 0, deltaAngle * j * (-360 / 2 / Mathf.PI));
        }
    }

    public override void DisplayClick()
    {
        if (clickCounter >= currentNumber)
        {
            clickCounter = 0;
        }
        if (Time.time >= nextActionTime)
        {
            MonoBehaviour.print(clickCounter);
            nextActionTime = Time.time + period;
            displayArr[clickCounter].GetComponentInChildren<Animator>().SetTrigger("Clicked");
            clickCounter += 1;
        }
    }
}

public class GrandMa : Building
{
    public GrandMa(string name, int initPrice, float cps, GameObject displayObject) : base(name, initPrice, cps, displayObject)
    {
    }

    public override void Buy()
    {
        currentNumber += 1;
        currentPrice = Mathf.CeilToInt(this.initPrice * Mathf.Pow(1.1f, currentNumber));
        CPS = countCPS();
        Display();
    }

    float countCPS()
    {
        int factoryNum = GlobalBuilding.buildingArr[2].currentNumber;
        int mineNum = GlobalBuilding.buildingArr[3].currentNumber;
        int shipmentNum = GlobalBuilding.buildingArr[4].currentNumber;
        int alchemyLabNum = GlobalBuilding.buildingArr[5].currentNumber;
        int portalNum = GlobalBuilding.buildingArr[6].currentNumber;
        int timeMachineNum = GlobalBuilding.buildingArr[7].currentNumber;
        int eldersPledgeNum = GlobalBuilding.buildingArr[8].currentNumber;

        return 0.8f + 0.2f * factoryNum + 0.4f * mineNum + 0.6f * shipmentNum + 0.8f * alchemyLabNum
            + 1.0f * portalNum + 1.2f * timeMachineNum + portalNum * 0.1f * eldersPledgeNum;

    }
}

public class EldersPledge : Building
{
    public EldersPledge(string name, int initPrice, float cps, GameObject displayObject) : base(name, initPrice, cps, displayObject)
    {
    }

    public new void Buy()
    {
        currentNumber += 1;
        currentPrice = Mathf.CeilToInt(this.initPrice * Mathf.Pow(1.1f, currentNumber));
        CPS = 0;
    }
}