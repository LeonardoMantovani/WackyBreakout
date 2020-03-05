using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //pause the time of the game
        Time.timeScale = 0;
    }

    public void OnResumeClick()
    {
        DestroyPauseMenu();
    }

    public void OnQuitClick()
    {
        DestroyPauseMenu();
        
        MenuManager.LoadMenu(MenuName.MainMenu);
    }

    void DestroyPauseMenu()
    {
        //resume the time of the game
        Time.timeScale = 1;

        //destroy the pause menu
        Destroy(gameObject);
    }
}
