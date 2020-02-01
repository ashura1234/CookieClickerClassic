using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyBuilding : MonoBehaviour
{

    int BuildingID(string buttonName)
    {
        for (int i = 0; i < GlobalBuilding.buildingArr.Count; i++)
        {
            if (GlobalBuilding.buildingArr[i].name == buttonName)
            {
                return i;
            }

        }
        return -1;
    }

    public void ButtonClicked()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        int id = BuildingID(name);
        double currentCookie = GlobalCookie.cookieCount;
        int buildingPrice = GlobalBuilding.buildingArr[id].currentPrice;
        if (id != -1 && currentCookie >= buildingPrice)
        {
            GlobalBuilding.buildingArr[id].Buy();
            GlobalCookie.cookieCount -= buildingPrice;
        }
    }
    
}
