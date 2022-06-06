using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySkeleton : MonoBehaviour
{
    [SerializeField] float healt = 3f;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f; // 2 atack arasýndaki geçebilecek maksimum süre (biz belirliyoruz)
    [SerializeField] float attackRange = 1f;
    [SerializeField] float agroRange = 4f;

    GameObject player;
    NavMeshAgent agent;
    Animator anim;
    float timePassed; // 2 attack arasýnda geçen süre (zamana baðlý)
    float newDestinationCD = 0.5f;
    int count;

    acilSusamAcil acilLan;

    float deathDelay = 3f;

    private void Start()
    {
        acilLan = Object.FindObjectOfType<acilSusamAcil>();
        count= 0;
        player = GameObject.FindWithTag("Player"); // Sürekli karakterimizi takip edeceði için.
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        anim.SetFloat("speed", agent.velocity.magnitude / agent.speed); //Skeletonun hýzýný animatordeki deðere eþledik.

        // istediðimiz sürede saldýrýsýný tekrarlasýn diye bu if'i kurduk. (her 3 saniyede 1)
        if (timePassed>=attackCD) 
        {
            if (Vector3.Distance(player.transform.position,transform.position)<=attackRange)
            {
                anim.SetTrigger("attack");
                timePassed = 0;

                // Ýskeler kýlýç savurma sesi
            }
        }
        timePassed += Time.deltaTime;

        // Enemy'nin karakterimizi takip etmesini saðladýk;
        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position,transform.position)<=agroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);

            // Ýskeler yürüme sesi
        }
        newDestinationCD -= Time.deltaTime;
        transform.LookAt(player.transform); // karakterimizi takip ederken ona bakmasýný da saðlamýþ olduk.

        if (count==5)
        {
            acilLan.anim.SetTrigger("open");
        }
    }

    public void TakeDamage(float damageAmount)
    {
        // Ýskelet hasar alma sesi

        healt -= damageAmount;
        anim.SetTrigger("damage");
        
        if (healt<=0)
        {
            StartCoroutine(skeletonDeathEnum());
        }
    }

    // Ölüm Animasyonu tamamen oynadýktan bir süre sonra enemy yok oluyor.
    IEnumerator skeletonDeathEnum() 
    {
        anim.SetTrigger("death");
        count += 1;
        

        yield return new WaitForSeconds(deathDelay);
        Destroy(this.gameObject);
    }

    //Enemy'nin agro range'ini ve attack range'ini editörden görsel olarak ayarlayabilmek için bu fonksiyonu kullandýk.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, agroRange);
    }

}
