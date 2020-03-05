using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : IntEventInvoker
{
    #region Fields

    //Score Text
    [SerializeField]
    Text scoreText;
    const string ScorePrefix = "Score:\n";
    int score = 0;

    //Balls Left Text
    [SerializeField]
    Text ballLeftText;
    const string BallLeftPrefix = "Ball Left:\n";
    int ballLeft;

    #endregion

    #region Proprities

    public int Score
    {
        get { return score; }
    }

    #endregion

    #region Methods

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = ScorePrefix + score.ToString();

        ballLeft = (int)ConfigurationUtils.BallsPerGame;
        ballLeftText.text = BallLeftPrefix + ballLeft.ToString();

        //add the AddPointsEvent listener
        EventManager.AddEventListener(EventName.AddPointsEvent, IncrementScore);

        //add the ReduceBallsLeftEvent listener
        EventManager.AddEventListener(EventName.ReduceBallsLeftEvent, BallSpawned);

        //add this script as an invoker for the GameOver Event
        events.Add(EventName.GameOverEvent, new GameOverEvent());
        EventManager.AddEventInvoker(EventName.GameOverEvent, this);
    }

    // Update is called once per frame
    void Update()
    {
        //Call the pause button method if the user press the pause key on the keyboard
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPauseClick();
        }
    }

    public void BallSpawned(int useless = 0) ////0 is a useless parameter that allow us to call all listeners as UnityAction<int>
    {
        ballLeft--;
        if (ballLeft >= 0)
        {
            ballLeftText.text = BallLeftPrefix + ballLeft.ToString();
        }
        else
        {
            events[EventName.GameOverEvent].Invoke((int)GameOverType.Defeat); //casting as int the GameOverType allow us to call all events as UnityEvents<int>
        }
    }

    public void IncrementScore(int points)
    {
        score += points;

        scoreText.text = ScorePrefix + score.ToString();
    }

    public void OnPauseClick()
    {
        MenuManager.LoadMenu(MenuName.PauseMenu);
    }

    #endregion
}
