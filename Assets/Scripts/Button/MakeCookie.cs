using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCookie : MonoBehaviour
{
    public GameObject plusOne;
    public GameObject plusOneParent;

    public void ButtonClicked()
    {
        GlobalCookie.cookieCount += 1;
        GameObject plusOneBox = new GameObject();
        plusOneBox.name = "plusOneBox";
        plusOneBox.transform.parent = plusOneParent.transform;
        plusOneBox.transform.position = plusOneParent.transform.position + new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 0);

        GameObject newPlusOne = Instantiate(plusOne);
        newPlusOne.transform.parent = plusOneBox.transform;
        Destroy(plusOneBox, 2.0f);
    }
}
