//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building
{
    public string name;
    public int initPrice;
    public int currentNumber = 0;
    public int currentPrice;
    public float initCPS;
    public float CPS;

    public Building() { }
    public Building(string name, int initPrice, float cps)
    {
        this.name = name;
        this.initPrice = initPrice;
        this.currentPrice = initPrice;
        this.initCPS = cps;
    }
    public void Buy()
    {
        this.currentNumber += 1;
        this.currentPrice = Mathf.CeilToInt(this.initPrice * Mathf.Pow(1.1f, currentNumber));
        this.CPS = this.currentNumber * initCPS;
    }

    
};

public class GlobalBuilding : MonoBehaviour
{
    public static List<Building> buildingArr = new List<Building>();
    public GameObject[] buttonArr;

    private void Start()
    {
        buildingArr.Add(new Building("Cursor", 15, 0.2f));
        buildingArr.Add(new Building("Grandma", 100, 0.8f));
        buildingArr.Add(new Building("Factory", 500, 4.0f));
        buildingArr.Add(new Building("Mine", 2000, 10.0f));
        buildingArr.Add(new Building("Shipment", 7000, 20.0f));
        buildingArr.Add(new Building("AlchemyLab", 50000, 100.0f));
        buildingArr.Add(new Building("Portal", 1000000, 1333.2f));
        buildingArr.Add(new Building("Time Machine", 123456789, 24691.2f));
        buildingArr.Add(new Building("ElderPledge", 666666, 0.0f));
    }

    private void Update()
    {
        for(int i = 0; i < buttonArr.Length; i++)
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
            if(GlobalCookie.cookieCount < buildingArr[i].currentPrice)
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


}
