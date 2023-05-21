using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameUI;

    public AimStateManager aimStateManager;
    public PointsManager pointsManager;

    public bool isPaused = false;
    public bool isPausedNoMenu = true;
    GameObject[] ammoUIObjects;

    public bool noMenu;

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
                PauseGame(false);
            }
            else
            {
                unPauseGame();
            }
        }
    }

    public void PauseGame(bool noMenu)
    {
        Time.timeScale = 0;
        InputManger.instance.canInput = false;
        gameUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        isPausedNoMenu = true;

        if(noMenu == false) 
        {
            pauseMenu.SetActive(true);
            isPaused = true;
        }
    }

    public void unPauseGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        isPausedNoMenu = false;

        pauseMenu.SetActive(false);
        gameUI.SetActive(true);

        InputManger.instance.canInput = true;

        Cursor.lockState = CursorLockMode.Locked;
    }

    //not really needed because there is now only one ammotext now

    // public void AmmoUIPause(){
    //     foreach (GameObject i in ammoUIObjects)
    //     {
    //         i.SetActive(false);
    //     }
        
    // }
    //  public void AmmoUIUnPause(){
    //     foreach (GameObject i in ammoUIObjects)
    //     {
    //         i.SetActive(true);
    //     }
    // }
}
