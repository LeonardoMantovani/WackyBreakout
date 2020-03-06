using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: add a way to return to the Main Menu
    }

    public void OnHomeButtonClick()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.LoadMenu(MenuName.MainMenu);
    }
}
