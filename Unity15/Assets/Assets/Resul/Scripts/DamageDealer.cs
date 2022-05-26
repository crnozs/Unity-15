using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage; // k�l�c�m�z daha �nce 

    [SerializeField] float weaponLength; // K�l�c�m�z�n i�erisine damage at�p atmad���m�z� kontrol eden bir raycast tanml�yoruz.
    [SerializeField] float weaponDamage; // K�l�c�m�z enemy'nin collider'ine temas etti�inde ne kadar hasar verecek onu belirliyoruz.
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

            int layerMask = 1 << 9; // Layer mask olu�turuyoruz ki damage vermek istemedi�imiz nesneleri g�z ard� edebilelim.
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                // Vurdu�umuz nesnenin i�erisinde EnemySkeleton componenti var m� ve daha �nceden damage at�lmam�� m� di�e kontrol ediyoruz.
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

    // Raycast'imizin boyunu edit�rden G�RSEL olarak ayarlayabilmemizi sa�l�yor.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; 
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength); //K�l�c�n ucundan tutamac�na do�ru bir �izgi ile kontrol ediyoruz.
    }
}
