using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    [HideInInspector] public int pointBalance;
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI pointText2;
    public GameObject shopPopUp;
    public AimStateManager aimStateManager;
    public GameObject shopMenu;
    public GameObject insufficientFunds;
    public bool inShop = false;
    public pause pause;
    public bool asdff;
    public Button button;

    public WeaponClassManager weaponClassManager;


    void Update() 
    {
        pointText.text = "$" + pointBalance.ToString();
        pointText2.text = "Balance: $" + pointBalance.ToString();

        if (Input.GetKeyDown(KeyCode.E) && aimStateManager.lookingAtShop) {
            if(!inShop)
            {
                if(!pause.isPaused) 
                {
                    enterShop();
                }
            } 
            else 
            {
                exitShop();
            }   
        }
    }

    void enterShop() {
        shopMenu.SetActive(true);
        inShop = true;
        pause.PauseGame(true);
        insufficientFunds.SetActive(false);
    }

    void exitShop() {
        shopMenu.SetActive(false);
        inShop = false;
        pause.unPauseGame();
    }

    public void buyItem1() {
        if (pointBalance >= 20) {
            pointBalance -= 20;
            weaponClassManager.CreateNewWeapon();
            weaponClassManager.AddNewWeapon();
            button.interactable = false;
        } else {
            insufficientFunds.SetActive(true);
        }
    }
    public void buyItem2() {
        if (pointBalance >= 10) {
            pointBalance -= 10;
            PlayerHealth.instance.playerHealth = 100;
        } else {
            insufficientFunds.SetActive(true);
        }
    }

    public void give30Points() {
        pointBalance += 30;
    }
}
