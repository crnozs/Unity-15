using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour // KARAKTERÝMÝZ SÝLAHLANDIÐI ZAMAN STRAFE MOVEMENT'E GEÇECEK
{

    int attackIndex;
    int kickIndex;
    bool canAttack = true;
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
        
        if (Input.GetKeyDown(KeyCode.E)&&isStrafe==true&&canAttack==true) //Tekme atmak
        {
            kickIndex = Random.Range(0, 2);
            anim.SetInteger("kickIndex", kickIndex);
            anim.SetTrigger("kick");
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isStrafe==true && canAttack==true) //Kýlýçla vurmak
        {
            attackIndex = Random.Range(0, 3);
            anim.SetInteger("attackIndex", attackIndex);



            anim.SetTrigger("attack");
            /*
            if (attackIndex==0)
            {
                anim.SetInteger("attacIndex", 1);
                attackIndex = 2;
                
            }
            if (attackIndex == 1)
            {
                anim.SetInteger("attackIndex", 2);
                attackIndex = 2;

            }

            if (attackIndex == 2)
            {
                anim.SetInteger("attacIndex", 3);
                attackIndex = 1;
            }
            */
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
}
