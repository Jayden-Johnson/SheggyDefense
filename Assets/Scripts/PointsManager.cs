using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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


    void Update() 
    {
        pointText.text = "Points: " + pointBalance.ToString();
        pointText2.text = "Balance: " + pointBalance.ToString();

        if (Input.GetKeyDown(KeyCode.E) && aimStateManager.lookingAtShop) {
            if(!inShop)
            {
                enterShop();
            } else {
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
        if (pointBalance >= 1) {
            pointBalance -= 1;
        } else {
            insufficientFunds.SetActive(true);
        }
    }
}
