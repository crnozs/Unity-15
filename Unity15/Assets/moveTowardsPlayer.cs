using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveTowardsPlayer : MonoBehaviour
{
	public float speed = 1.0f;

	private GameObject player;

	// Use this for initialization
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		transform.position += (player.transform.position - transform.position).normalized * speed * Time.deltaTime;
	}

 

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			StartCoroutine(screamSound());
		}
	}

	IEnumerator screamSound()
    {
		// takip eden nesnenin scream sesi.
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
