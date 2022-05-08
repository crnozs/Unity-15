using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour // KARAKTERÝMÝZ SÝLAHLANDIÐI ZAMAN STRAFE MOVEMENT'E GEÇECEK
{

    int kickIndex;
    bool canAttack = true;
    bool isStrafe = false;
    Animator anim;

    public GameObject kilicEl;
    public GameObject kilicSirt;
    public GameObject trails;
    public bool isRage = false;
   
    
    void Start()
    {
        anim = GetComponent<Animator>();
        trailClose();
       
    }
 
   
    void Update()
    {
        anim.SetBool("iS", isStrafe);

        if (Input.GetKeyDown(KeyCode.F))
        {
            isStrafe = !isStrafe; 
        }
        
        if (Input.GetKeyDown(KeyCode.E)&&isStrafe==true&&canAttack==true) //Tekme atmak
        {
            kickIndex = Random.Range(0, 2);
            anim.SetInteger("kickIndex", kickIndex);
            anim.SetTrigger("kick");
        }
        if (Input.GetKeyDown(KeyCode.R)&&isRage==true)
        {
            anim.SetTrigger("rage");
        }

        

        if (Input.GetKeyDown(KeyCode.Mouse0) && isStrafe==true && canAttack==true) //Kýlýçla vurmak
        {
           
            anim.SetTrigger("attack");

            




        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && isStrafe == true && canAttack == true) // Block Yapmak
        {
            
        }

        // Controller scriptindeki deðiþkenleri kontrol ediyoruz
        if (isStrafe==true)
        {
            GetComponent<Controller>().hareketTipi = Controller.MovementType.Strafe;
            GetComponent<IKLook>().azalt();
            canAttack = true;
        }
        if (isStrafe == false)
        {
            GetComponent<Controller>().hareketTipi = Controller.MovementType.Directional;
            GetComponent<IKLook>().arttýr();
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
