using UnityEngine;



public class Interactible : MonoBehaviour 
{
    public float radius=3f;

    private void OnDrawGizmosSelected() // Editörden kontrol etmemizi sağlayacak olan fonksiyon.
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius); // Nesneyle etkileşime geçilebilecek alan.
    }
}
/*
 * Bu Scripti etkileşime geçilebilecek nesnelere component olarak atacak ve etrafındaki sarı renkli
 * sınırlar içerisinde (radius) kameradan gönderilen raycast'in temas edeceği alanı ayarlayıp 
 * karakterimizi nesnelerle etkileşime sokabileceğiz. kılıcı alabilmek yada goleme saldırabilmek gibi.
 */
