using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class PortalLevelLoader : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] int sceneIndex;

    [SerializeField] bool scene1;
    [SerializeField] bool scene2;

    public int quest = 0;

    bool pass;

    private void Start()
    {
        quest = PlayerPrefs.GetInt("Quest");
    }

    private void Update()
    {
        if(scene1)
        {
            if(Collect.Collect1 && Collect.Collect2)
            {
                pass = true;
            }
        }

        if(scene2)
        {
            if (Collect.Collect3 && Collect.Collect4)
            {
                pass = true;
            }
        }
        if(!scene1 && !scene2)
        {
            pass = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (pass)
        { other.gameObject.GetComponent<Character>().enabled = false;
            StartCoroutine(Delay());
            
            
            if (scene1 || scene2)
            {
                quest++;
                PlayerPrefs.SetInt("Quest", quest); 
            }
        }
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
