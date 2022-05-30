using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLevelLoader : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
 

    void OnCollisionEnter(Collision other)
    {
        StartSuccessSequence();
    }


    void StartSuccessSequence()
    {
        GetComponent<Character>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);

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
