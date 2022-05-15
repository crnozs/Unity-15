using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour // KARAKTER�M�Z S�LAHLANDI�I ZAMAN STRAFE MOVEMENT'E GE�ECEK
{

    
    bool canAttack = true;
    bool isStrafe = false;
    Animator anim;

    public GameObject kilicEl;
    public GameObject kilicSirt;
    public GameObject trails;
   
    
    void Start()
    {
        anim = GetComponent<Animator>();
        trailClose();
       
    }
 
   
    void Update()
    {
        anim.SetBool("iS", isStrafe);
        if (kilicSirt!=null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isStrafe = !isStrafe;
            }
        }
        
        

        if (Input.GetKeyDown(KeyCode.Mouse0) && isStrafe==true && canAttack==true) //K�l��la vurmak
        {
           
            anim.SetTrigger("attack");



        }
        /*if (Input.GetKeyDown(KeyCode.Mouse1) && isStrafe == true && canAttack == true) // Block Yapmak
        {
            
        }*/

        // Controller scriptindeki de�i�kenleri kontrol ediyoruz
        if (isStrafe==true)
        {
            GetComponent<Controller>().hareketTipi = Controller.MovementType.Strafe;
            GetComponent<IKLook>().azalt();
            canAttack = true;
        }
        if (isStrafe == false)
        {
            GetComponent<Controller>().hareketTipi = Controller.MovementType.Directional;
            GetComponent<IKLook>().artt�r();
            canAttack = false;
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

    void trailOpen()
    {
        for (int i = 0; i < trails.transform.childCount; i++)
        {
            trails.transform.GetChild(i).gameObject.GetComponent<TrailRenderer>().emitting = true;
        }

    }
    void trailClose() 
    {
        for (int i = 0; i < trails.transform.childCount; i++)
        {
            trails.transform.GetChild(i).gameObject.GetComponent<TrailRenderer>().emitting = false;
        }

    }
}
