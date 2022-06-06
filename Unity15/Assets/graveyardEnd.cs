using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graveyardEnd : MonoBehaviour
{
    int count;
    [SerializeField]
    GameObject door;
    GameObject angel;
    [Range(-2f,2f)]
    float randomDistance;
    void Start()
    {
        angel = GameObject.FindGameObjectWithTag("angel");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("angel"))
        {
            count += 1;
            angel.transform.position = new Vector3
                (gameObject.transform.position.x - randomDistance, 
                gameObject.transform.position.y - randomDistance, 
                gameObject.transform.position.z - randomDistance);

            angel.GetComponent<moveTowardsPlayer>().speed = 0f;

            if (count==3)
            {
                door.GetComponent<Animator>().SetTrigger("open");
            }
        }
    }
}
