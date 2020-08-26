using UnityEngine.SceneManagement;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<AudioManager>().EndSound("GameOver");
    }
    public void Quit() 
    {
        Application.Quit();
    }
}
