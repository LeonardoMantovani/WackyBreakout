using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The Game manager
/// </summary>
public class WackyBreakout : IntEventInvoker
{
    // The Victory GameOver Message
    const string VictoryMessage = "YOU WIN!\nYOUR FINAL SCORE IS: ";

    // The Defeat GameOver Message
    const string DefeatMessage = "YOU LOSE!\nBUT DON'T GIVE UP, TRY AGAIN!";
    
    /// <summary>
    /// Awake is called before Start(),
    /// Initialize all static methods of the game
    /// </summary>
    private void Awake()
    {
        ScreenUtils.Initialize();
        ConfigurationUtils.Initialize();
        EventManager.Initialize();
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        //add the CheckForGameOver method as a listener of the BlockDestroyed Event
        EventManager.AddEventListener(EventName.BlockDestroyedEvent, CheckForGameOver);

        //add this script as an invoker of the GameOver Event
        events.Add(EventName.GameOverEvent, new GameOverEvent());
        EventManager.AddEventInvoker(EventName.GameOverEvent, this);

        //add the SpawnGameOver method as a listener of the GameOver Event
        EventManager.AddEventListener(EventName.GameOverEvent, SpawnGameOver);
    }

    public void CheckForGameOver(int useless = 0)
    {
        // if there aren't other blocks in the scene
        if (GameObject.FindGameObjectsWithTag("Block").Length <= 0)
        {
            events[EventName.GameOverEvent].Invoke((int)GameOverType.Victory); //casting as int the GameOverType allow us to call all events as UnityEvents<int>
        }
    }

    public void SpawnGameOver(int gameOverType)
    {
        //load the Game Over Screen
        MenuManager.LoadMenu(MenuName.GameOverScreen);

        //Get the VictoryText
        Text victoryText = GameObject.FindWithTag("VictoryText").GetComponent<Text>();

        if (gameOverType == 0)
        {
            HUD hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
            
            victoryText.text = VictoryMessage + hud.Score.ToString();
        }
        else
            victoryText.text = DefeatMessage;
    }
}
