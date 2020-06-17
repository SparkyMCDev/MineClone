using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject AboutMenu;


    //Simple Main Menu Stuff

    private void Awake()
    {
        SettingsMenu.SetActive(false);
        AboutMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void QuitGameButton()
    {
        Debug.Log("User Quit the game");
        Application.Quit();
    }




    public void BackButton()
    {
        SettingsMenu.SetActive(false);
        AboutMenu.SetActive(false);
        MainMenu.SetActive(true);
    }


    public void AboutButton()
    {
        AboutMenu.SetActive(true);
        MainMenu.SetActive(false);
    }




    //Settings Menu

    public void SettingsButton()
    {
        SettingsMenu.SetActive(true);
        MainMenu.SetActive(false);
    }






    //Singleplayer Menu

    public void SinglePlayerButton()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
