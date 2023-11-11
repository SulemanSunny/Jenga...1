using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    public float restartDelay = 1f;

    public GameObject completeLevelUI;

    public void EndGame()
    {
        SceneManager.LoadScene(3);
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

   public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    void OnTriggerEnter()
    {
       CompleteLevel();
    }
   
}
