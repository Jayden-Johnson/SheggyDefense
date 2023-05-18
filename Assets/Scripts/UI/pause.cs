using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameUI;

    public AimStateManager aimStateManager;
    public PointsManager pointsManager;

    private bool isPaused = false;
    GameObject[] ammoUIObjects;

    private void Start()
    {
        ammoUIObjects = GameObject.FindGameObjectsWithTag("AmmoUI");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pointsManager.inShop)
        {
            if (isPaused == false)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    private void PauseGame()
    {
        AmmoUIPause();
        Time.timeScale = 0;
        isPaused = true;
        pauseMenu.SetActive(true);
        gameUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;

        aimStateManager.enabled = false;
    }

    private void UnpauseGame()
    {
        AmmoUIUnPause();
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);

        aimStateManager.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;

        
    }
    public void pauseGameNoMenu() {
        WeaponManager.instance.enabled = false;
        AmmoUIPause();
        Time.timeScale = 0;
        isPaused = true;
        gameUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        
        Debug.Log("paused");

        aimStateManager.enabled = false;
    }
    
    public void unPauseGameNoMenu() {
        WeaponManager.instance.enabled = true;
        AmmoUIUnPause();
        Time.timeScale = 1;
        isPaused = false;
        gameUI.SetActive(true);

        aimStateManager.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        
        Debug.Log("unpaused");
    }
    public void AmmoUIPause(){
        foreach (GameObject i in ammoUIObjects)
        {
            i.SetActive(false);
        }
        
    }
     public void AmmoUIUnPause(){
        foreach (GameObject i in ammoUIObjects)
        {
            i.SetActive(true);
        }
        
    }
    

}
