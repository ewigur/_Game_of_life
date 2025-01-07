using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameOfLife
{
    
    public class InGameButtons : MonoBehaviour
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

    }
}
