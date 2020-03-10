using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The Ball
/// </summary>
public class Ball : IntEventInvoker
{
    #region Fields

    //Ball's RigidBody2D component
    Rigidbody2D rb2d;

    //get-ready timer (time before the ball starts moving after spawned)
    Timer getReadyTimer;
    bool getReadyTimerDestroyed = false;

    //Self-distruction timer
    Timer deathTimer;

    //Collider Radius (to check if the ball is out of the screen)
    float colliderRadius;

    //BallSpawner Script (to spawn another ball after death)
    BallSpawner ballSpawner;

    //HUD Script (to change Balls Left value)
    HUD hud;

    //[SerializeField]
    //bool ballsFinished = false;

    #endregion

    #region Methods

    // Start is called before the first frame update
    void Start()
    {
        //place the ball at 1/3 of the screen (remember that 0 is in the middle)
        transform.position = new Vector3(0, -(ScreenUtils.ScreenTop / 3), 0);

        //get the RigidBody2D component
        rb2d = GetComponent<Rigidbody2D>();

        //get the Collider radius
        colliderRadius = gameObject.GetComponent<CircleCollider2D>().radius;

        //get the Ball Spawner Script
        ballSpawner = GameObject.FindWithTag("MainCamera").GetComponent<BallSpawner>();

        //get the HUD Script
        hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();

        //start the Get-ready timer
        getReadyTimer = gameObject.AddComponent<Timer>();
        getReadyTimer.Duration = ConfigurationUtils.BallGetReadyTimerDuration;
        getReadyTimer.Run();
        //add the StartMoving method as a listener for the getReadyTimer's TimerFinished Event
        getReadyTimer.AddTimerFinishedEventListener(StartMoving);

        //start the self-distruction timer
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = ConfigurationUtils.BallDeathTimerDuration;
        deathTimer.Run();
        //add the DestroyBall method as a listener for the deathTimer's TimerFinished Event
        deathTimer.AddTimerFinishedEventListener(DestroyBall);

        //add this ball as invoker for the freezer event
        events.Add(EventName.FreezerEvent, new FreezerEvent());
        EventManager.AddEventInvoker(EventName.FreezerEvent, this);

        //add this ball as an invoker for the speedup event
        events.Add(EventName.SpeedupEvent, new SpeedupEvent());
        EventManager.AddEventInvoker(EventName.SpeedupEvent, this);

        //add this ball as an invoker for the SpawnBall Event
        events.Add(EventName.SpawnBallEvent, new SpawnBallEvent());
        EventManager.AddEventInvoker(EventName.SpawnBallEvent, this);

        //add this ball as an invoker for the HUD's AddPoints event
        events.Add(EventName.AddPointsEvent, new AddPointsEvent());
        EventManager.AddEventInvoker(EventName.AddPointsEvent, this);

        ////add the BallsFinished method as a listener for the BallsFinished Event
        //EventManager.AddEventListener(EventName.BallsFinishedEvent, BallsFinished);

        //add this ball as an invoker for the GameOver Event
        events.Add(EventName.GameOverEvent, new GameOverEvent());
        EventManager.AddEventInvoker(EventName.GameOverEvent, this);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y + colliderRadius < ScreenUtils.ScreenBottom)
        {
            DestroyBall();
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        if (ballSpawner.SpeedingUp)
        {
            rb2d.velocity = newDirection * (ConfigurationUtils.BallInitialForceMagnitude * 2);
        }
        else
            rb2d.velocity = newDirection * ConfigurationUtils.BallInitialForceMagnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            BlockType blockType = collision.gameObject.GetComponent<Block>().BlockType;

            switch (blockType)
            {
                case BlockType.Standard:
                    events[EventName.AddPointsEvent].Invoke((int)ConfigurationUtils.StandardBlocksPoints);
                    AudioManager.PlayAudioClip(AudioClipName.BlockDestroyed);
                    break;
                case BlockType.Bonus:
                    events[EventName.AddPointsEvent].Invoke((int)ConfigurationUtils.BonusBlocksPoints);
                    AudioManager.PlayAudioClip(AudioClipName.BlockDestroyed);
                    break;
                case BlockType.Freezer:
                    events[EventName.AddPointsEvent].Invoke((int)ConfigurationUtils.FreezerBlocksPoints);
                    events[EventName.FreezerEvent].Invoke(0); //0 is a useless parameter that allow us to call all events as UnityEvents<int>
                    AudioManager.PlayAudioClip(AudioClipName.FreezeEffect);
                    break;
                case BlockType.SpeedUp:
                    events[EventName.AddPointsEvent].Invoke((int)ConfigurationUtils.SpeedupBlocksPoints);
                    events[EventName.SpeedupEvent].Invoke(0); //0 is a useless parameter that allow us to call all events as UnityEvents<int>
                    AudioManager.PlayAudioClip(AudioClipName.SpeedupEffect);
                    break;
                default:
                    break;
            }

            Destroy(collision.gameObject);
            
        }
    }

    public void DestroyBall(int useless = 0) //useless is a parameter that allow us to call all listeners as UnityAction<int>
    {
        //destroy the ball and play proper sound
        Destroy(gameObject);
        AudioManager.PlayAudioClip(AudioClipName.BallDestroyed);
        //spawn another ball if they aren't finished
        if(!ballSpawner.BallsFinishedPropriety)
            events[EventName.SpawnBallEvent].Invoke(0);
        //or invoke the GameOver Event if this was the last ball standing
        else if (GameObject.FindGameObjectsWithTag("Ball").Length <= 1) //balls has to be <= 1 becouse this ball is still alive
            events[EventName.GameOverEvent].Invoke((int)GameOverType.Defeat); //casting as int the GameOverType allow us to call all events as UnityEvents<int>

        /*
        //invoke the GameOver event if this was the last ball aviable
        if (ballsFinished && GameObject.FindGameObjectsWithTag("Ball").Length <= 0)
        {
            events[EventName.GameOverEvent].Invoke((int)GameOverType.Defeat); //casting as int the GameOverType allow us to call all events as UnityEvents<int>
        }
        //or spawn another ball
        else
            events[EventName.SpawnBallEvent].Invoke(0);
        */
    }

    public void StartMoving(int useless = 0) //useless is a parameter that allow us to call all listeners as UnityAction<int>
    {
        if (getReadyTimerDestroyed == false)
        {
            //add a force to the ball
            Vector2 direction = new Vector2(0, -1);
            rb2d.AddForce(direction * ConfigurationUtils.BallInitialForceMagnitude, ForceMode2D.Impulse);

            //change the ball layer from JustBornBall to Ball so it can collide with other balls
            gameObject.layer = 9;

            //fake-destroy the timer
            getReadyTimerDestroyed = true;
        }
    }

    //public void BallsFinished(int useless = 0)
    //{
    //    ballsFinished = true;
    //}

    #endregion
}
