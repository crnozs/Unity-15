using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject healtBar;
    public GameObject staminaBar;
    public GameObject rageBar;


    void Update()
    {
        if(rageBar.gameObject.GetComponent<Image>().fillAmount == 100f)
        {
            GetComponent<WeaponController>().isRage = true;
        }
        else
        {
            GetComponent<WeaponController>().isRage = false;
        }
        
    }
}
