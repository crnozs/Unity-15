using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class PortalLevelLoader : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] int sceneIndex;
 
   
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Character>().enabled = false;
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(levelLoadDelay);
        SceneManager.LoadScene(sceneIndex);
    }

      

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    

}
