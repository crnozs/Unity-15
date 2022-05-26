using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;
    [SerializeField] GameObject trails;


    GameObject currentWeaponInHand;
    GameObject currentWeaponInSheath;
    GameObject weaponTrails;
    void Start()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        
    }



    public void DrawWeapon() // Kýlýcý çektiðimizde gerçekleþecek olan event.
    {
        currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
        weaponTrails = Instantiate(trails, weaponHolder.transform);
        Destroy(currentWeaponInSheath);
    }

    public void SheathWeapon() // Kýlýcý geri koyduðumuzda gerçekleþecek olan event.
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        Destroy(currentWeaponInHand);
        Destroy(weaponTrails);
    }


    public void StartDealDamage() // DamageDealer scriptindeki fonksiyona event ile ulaþtýk.
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }
    public void EndDealDamage() // DamageDEaler scriptindeki fonksiyona event ile ulaþtýk. Fonksiyonlarýn hepsini
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }
}