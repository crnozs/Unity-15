using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour // KARAKTERÝMÝZ SÝLAHLANDIÐI ZAMAN STRAFE MOVEMENT'E GEÇECEK
{

    bool isStrafe = false;
    Animator anim;

    public GameObject kilicEl;
    public GameObject kilicSirt;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

   
    void Update()
    {
        anim.SetBool("iS", isStrafe);

        if (Input.GetKeyDown(KeyCode.F))
        {
            isStrafe = !isStrafe;
        }

        // Controller scriptindeki deðiþkenleri kontrol ediyoruz
        if (isStrafe==true)
        {
            GetComponent<Controller>().hareketTipi = Controller.MovementType.Strafe; 
        }
        if (isStrafe == false)
        {
            GetComponent<Controller>().hareketTipi = Controller.MovementType.Directional;
        }


    }

    void equip()
    {
        kilicEl.SetActive(true);
        kilicSirt.SetActive(false);
    }
    void unequip()
    {
        kilicSirt.SetActive(true);
        kilicEl.SetActive(false);
    }
}
