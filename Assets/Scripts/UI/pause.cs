using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused = false;

    public AimStateManager aimStateManager;
    public PointsManager pointsManager;
    public GameObject gameUI;


    void Start() 
    {
    }


    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pointsManager.inShop) 
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
        gameUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        
        Debug.Log("paused");

        aimStateManager.enabled = false;
    }
    
    public void unPauseGame() {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);

        aimStateManager.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        
        Debug.Log("unpaused");
    }

    public void pauseGameNoMenu() {
        Time.timeScale = 0;
        isPaused = true;
        gameUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        
        Debug.Log("paused");

        aimStateManager.enabled = false;
    }
    
    public void unPauseGameNoMenu() {
        Time.timeScale = 1;
        isPaused = false;
        gameUI.SetActive(true);

        aimStateManager.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        
        Debug.Log("unpaused");
    }
}
