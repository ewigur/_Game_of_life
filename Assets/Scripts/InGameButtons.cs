using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameOfLife
{
    
    public class InGameButtons : MonoBehaviour
    {
        public GameObject Button;
        public GameObject controlsPanel;

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ControlsButton()
        {
            if(controlsPanel != null)
                controlsPanel.SetActive(true);
        }

        public void BackButton()
        {
            if(controlsPanel != null)
                controlsPanel.SetActive(false);
            
        }

        public void QuitToMenu()
        {
            SceneManager.LoadScene("GameMenu");
        }

    }
}
