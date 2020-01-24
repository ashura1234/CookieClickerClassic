using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlobalCookie : MonoBehaviour
{
    public static float cookieCount = 0;
    public GameObject cookieDisplay;
    public GameObject CPSDisplay;
    public GameObject plusOne;
    public GameObject plusOneParent;
    float internalCookie = 0;
    public float CPS = 0;
    int cursorNum;
    float period;
    float nextActionTime;

    private void Update()
    {
        internalCookie = cookieCount;
        cookieDisplay.GetComponent<Text>().text = cookieCount.ToString("n0");

        CPS = 0;
        for (int i = 1; i < GlobalBuilding.buildingArr.Count; i++)
        {
            CPS += GlobalBuilding.buildingArr[i].CPS;
        }
        cookieCount += CPS / 60.0f;

        CPS += GlobalBuilding.buildingArr[0].CPS;
        CPSDisplay.GetComponent<TextMeshProUGUI>().text = "Cookies / Second : " + CPS.ToString("n2");

        cursorNum = GlobalBuilding.buildingArr[0].currentNumber;
        if (period == 0 && cursorNum > 0)
        {
            period = 5.0f / cursorNum;
            nextActionTime = Time.time + period;
        }
        period = 5.0f / cursorNum;
        if (cursorNum > 0 && Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;
            cookieCount += 1;
            CursorAnim();
        }

    }

    public void CursorAnim()
    {
        GameObject plusOneBox = new GameObject();
        plusOneBox.name = "plusOneBox";
        plusOneBox.transform.parent = plusOneParent.transform;
        plusOneBox.transform.position = plusOneParent.transform.position + new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 0);

        GameObject newPlusOne = Instantiate(plusOne);
        newPlusOne.transform.parent = plusOneBox.transform;
        Destroy(plusOneBox, 2.0f);
    }
}
