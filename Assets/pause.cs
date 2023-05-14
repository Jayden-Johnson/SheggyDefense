using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class pause : MonoBehaviour
{
    public GameObject pauseMenu;

    public static bool isPaused = false;

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(isPaused == false)
            {
                pauseGame();
            } 
            else 
            {
                unPauseGame(); 
            }
        }
    }

    public void pauseGame() {
        Time.timeScale = 0;
        isPaused = true;
        pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        
        Debug.Log("paused");
    }
    
    public void unPauseGame() {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Locked;
        
        Debug.Log("unpaused");
    }
}
