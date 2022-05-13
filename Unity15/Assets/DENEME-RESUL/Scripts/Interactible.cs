using UnityEngine;



public class Interactible : MonoBehaviour 
{
    public float radius=3f;

    private void OnDrawGizmosSelected() // Edit�rden kontrol etmemizi sa�layacak olan fonksiyon.
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius); // Nesneyle etkile�ime ge�ilebilecek alan.
    }
}
/*
 * Bu Scripti etkile�ime ge�ilebilecek nesnelere component olarak atacak ve etraf�ndaki sar� renkli
 * s�n�rlar i�erisinde (radius) kameradan g�nderilen raycast'in temas edece�i alan� ayarlay�p 
 * karakterimizi nesnelerle etkile�ime sokabilece�iz. k�l�c� alabilmek yada goleme sald�rabilmek gibi.
 */
