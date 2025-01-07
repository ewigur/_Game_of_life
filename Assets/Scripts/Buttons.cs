using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class Buttons : MonoBehaviour
{
    [SerializeField] public GameObject promptPanel;
    [SerializeField] public GameObject Menu;
    public GameObject Button;

    private void Start() 
    {
        promptPanel.SetActive(false);        
    }
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

    public void Controls()
    {
        Menu.SetActive(false);
        promptPanel.SetActive(true);        
    }

    public void Back()
    {
        Menu.SetActive(true);
        promptPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
