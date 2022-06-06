using UnityEngine;

public class piano : MonoBehaviour
{

    public AudioSource pianosound;

    void Start()
    {
        pianosound = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerAgainstGolemPackage")
        {
            pianosound.Stop();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "PlayerAgainstGolemPackage")
        {
            pianosound.Play();
        }
    }
}