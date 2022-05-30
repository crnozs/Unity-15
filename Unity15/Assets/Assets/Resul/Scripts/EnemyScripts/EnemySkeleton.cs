using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySkeleton : MonoBehaviour
{
    [SerializeField] float healt = 3f;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f; // 2 atack aras�ndaki ge�ebilecek maksimum s�re (biz belirliyoruz)
    [SerializeField] float attackRange = 1f;
    [SerializeField] float agroRange = 4f;

    GameObject player;
    NavMeshAgent agent;
    Animator anim;
    float timePassed; // 2 attack aras�nda ge�en s�re (zamana ba�l�)
    float newDestinationCD = 0.5f; 
    

    float deathDelay = 3f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player"); // S�rekli karakterimizi takip edece�i i�in.
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        anim.SetFloat("speed", agent.velocity.magnitude / agent.speed); //Skeletonun h�z�n� animatordeki de�ere e�ledik.

        // istedi�imiz s�rede sald�r�s�n� tekrarlas�n diye bu if'i kurduk. (her 3 saniyede 1)
        if (timePassed>=attackCD) 
        {
            if (Vector3.Distance(player.transform.position,transform.position)<=attackRange)
            {
                anim.SetTrigger("attack");
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;

        // Enemy'nin karakterimizi takip etmesini sa�lad�k;
        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position,transform.position)<=agroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
        transform.LookAt(player.transform); // karakterimizi takip ederken ona bakmas�n� da sa�lam�� olduk.
    }

    public void TakeDamage(float damageAmount)
    {
        healt -= damageAmount;
        anim.SetTrigger("damage");
        
        if (healt<=0)
        {
            StartCoroutine(skeletonDeathEnum());
        }
    }

    // �l�m Animasyonu tamamen oynad�ktan bir s�re sonra enemy yok oluyor.
    IEnumerator skeletonDeathEnum() 
    {
        anim.SetTrigger("death");
        

        yield return new WaitForSeconds(deathDelay);
        Destroy(this.gameObject);
    }

    //Enemy'nin agro range'ini ve attack range'ini edit�rden g�rsel olarak ayarlayabilmek i�in bu fonksiyonu kulland�k.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, agroRange);
    }

}