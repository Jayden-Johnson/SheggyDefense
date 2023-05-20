using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameUI;

    public AimStateManager aimStateManager;
    public PointsManager pointsManager;

    private bool isPaused = false;
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
        isPaused = true;
        WeaponManager.instance.enabled = false;
        aimStateManager.enabled = false;

        gameUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;

        if(noMenu == false) 
        {
            pauseMenu.SetActive(true);
        }
    }

    public void unPauseGame()
    {
        Time.timeScale = 1;
        isPaused = false;

        pauseMenu.SetActive(false);
        gameUI.SetActive(true);

        WeaponManager.instance.enabled = true;
        aimStateManager.enabled = true;

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
