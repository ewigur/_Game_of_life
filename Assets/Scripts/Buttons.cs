using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
    public GameObject Button;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SpawnScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
