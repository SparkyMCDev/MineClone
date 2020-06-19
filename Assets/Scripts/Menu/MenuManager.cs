using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject AboutMenu;
    public GameObject SinglePlayer;
    public GameObject PlaySelectedWorld;
    public GameObject DeleteWorld;
    private string SelectedWorld = null;
    private bool WorldSelected = false;


    //Simple Main Menu Stuff

    private void Awake()
    {
        SettingsMenu.SetActive(false);
        AboutMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    private void Update()
    {
        if(SelectedWorld != null && WorldSelected == true) { PlaySelectedWorld.GetComponent<Button>().interactable = true; DeleteWorld.GetComponent<Button>().interactable = true; }
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

    //About Menu
    public void AboutButton()
    {
        AboutMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void ClickEvent(string Logo)
    {
        if(Logo == "Github") { Application.OpenURL("https://github.com/SparkyMCDev/MineClone"); }
        else { Application.OpenURL("https://discord.gg/nPCa36t");  }
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
        SinglePlayer.SetActive(true);
        MainMenu.SetActive(false);
        //SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        //SceneManager.UnloadSceneAsync("MainMenu");

    }

    public void DeleteWorldButton()
    {
        if(SelectedWorld != null)
        {
            //Delete World
        }
    }

}
