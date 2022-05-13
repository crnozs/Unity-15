using UnityEngine;



public class Interactible : MonoBehaviour 
{
    public float radius=3f;

    private void OnDrawGizmosSelected() // Editörden kontrol etmemizi saðlayacak olan fonksiyon.
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius); // Nesneyle etkileþime geçilebilecek alan.
    }
}
/*
 * Bu Scripti etkileþime geçilebilecek nesnelere component olarak atacak ve etrafýndaki sarý renkli
 * sýnýrlar içerisinde (radius) kameradan gönderilen raycast'in temas edeceði alaný ayarlayýp 
 * karakterimizi nesnelerle etkileþime sokabileceðiz. kýlýcý alabilmek yada goleme saldýrabilmek gibi.
 */
