using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage; // kýlýcýmýz daha önce 

    [SerializeField] float weaponLength; // Kýlýcýmýzýn içerisine damage atýp atmadýðýmýzý kontrol eden bir raycast tanmlýyoruz.
    [SerializeField] float weaponDamage; // Kýlýcýmýz enemy'nin collider'ine temas ettiðinde ne kadar hasar verecek onu belirliyoruz.
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    void Update()
    {
        if (canDealDamage)
        {
            RaycastHit hit;

            int layerMask = 1 << 9; // Layer mask oluþturuyoruz ki damage vermek istemediðimiz nesneleri göz ardý edebilelim.
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                // Vurduðumuz nesnenin içerisinde EnemySkeleton componenti var mý ve daha önceden damage atýlmamýþ mý diðe kontrol ediyoruz.
                if (hit.transform.TryGetComponent(out EnemySkeleton enemySkeleton) && !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    enemySkeleton.TakeDamage(weaponDamage);
                    hasDealtDamage.Add(hit.transform.gameObject);
                    print("SKELETON TAKE DAMAGE");
                }
                
            }
        }
    }
    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    // Raycast'imizin boyunu editörden GÖRSEL olarak ayarlayabilmemizi saðlýyor.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; 
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength); //Kýlýcýn ucundan tutamacýna doðru bir çizgi ile kontrol ediyoruz.
    }
}
