using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{
    public static void LoadMenu(MenuName menuName)
    {
        switch (menuName)
        {
            case MenuName.MainMenu:
                SceneManager.LoadScene("mainMenu");
                break;
            case MenuName.PauseMenu:
                Object.Instantiate(Resources.Load(@"prefabs\PauseMenu"));
                break;
            default:
                break;
        }
    }
}
