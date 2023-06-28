using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator playerAnim;
    public Animator menuAnim;

    public AudioSource source;
    public AudioClip clip;
    // public GameObject settingsMenu;



    void Start(){
        playerAnim.SetBool("isMainMenu", true);
        source.PlayOneShot(clip);
        Cursor.lockState = CursorLockMode.None;
    }

    public void leaveMainMenu() {
        playerAnim.SetBool("isMainMenu", false);
    }

    public void quitGame() {
        Application.Quit();
    }

    public void enterSettings() {
        menuAnim.SetBool("mainOut", true);
        menuAnim.SetBool("mainIn", false);

        StartCoroutine(settingsInAfterMainOut());
    }
    public void exitSettings() {
        menuAnim.SetBool("settingsOut", true);
        menuAnim.SetBool("settingsIn", false);

        StartCoroutine(mainInAfterSettingsOut());
    }

    private IEnumerator settingsInAfterMainOut()
    {
        yield return new WaitForSeconds(0.8f);

        menuAnim.SetBool("mainOut", false);
        menuAnim.SetBool("settingsIn", true);
    }

    private IEnumerator mainInAfterSettingsOut()
    {
        yield return new WaitForSeconds(0.8f);

        menuAnim.SetBool("settingsOut", false);
        menuAnim.SetBool("mainIn", true);
    }
}
