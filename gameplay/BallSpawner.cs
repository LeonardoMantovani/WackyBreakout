using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : IntEventInvoker
{
    //Timer for the Random Spawning of balls
    Timer spawnTimer;

    //Ball to spawn
    GameObject ballPrefab;
    CircleCollider2D ballCollider;

    //Spawning area's corners
    Vector3 lowerLeftCorner;
    Vector3 upperRightCorner;
    float colliderRadius;

    //flag in order to avoid balls overlapping on spawning
    bool ballInQueue = false;
    
    //timer before retry spawning a ball
    Timer waitingTimer;

    //HUD Script (to update Number of Balls Left)
    HUD hud;

    //SpeedUp Balls support
    bool speedingUp = false; //(a flag for the speedUp effect)
    Timer speedupTimer;

    /// <summary>
    /// A propriety which returns the speedingUp flag
    /// </summary>
    public bool SpeedingUp
    {
        get { return speedingUp; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //get the ball prefab from the resource folder
        ballPrefab = Resources.Load<GameObject>(@"prefabs\Ball");

        //get the half of the ball's collider diagonal
        ballCollider = ballPrefab.GetComponent<CircleCollider2D>();
        colliderRadius = ballCollider.radius;

        //get the spawning area's corners
        lowerLeftCorner = new Vector3(-colliderRadius, -(ScreenUtils.ScreenTop / 3) - colliderRadius, 0);
        upperRightCorner = new Vector3(colliderRadius, -(ScreenUtils.ScreenTop / 3) + colliderRadius, 0);

        //get the HUD Script
        hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();

        //add the waitingTimer to the Game Object
        waitingTimer = gameObject.AddComponent<Timer>();
        waitingTimer.Duration = 1.5f;
        //add the SpawnBall method as a listener of the waitingTimer's TimerFinished Event
        waitingTimer.AddTimerFinishedEventListener(SpawnBall);

        //start the timer for the next random-spawn
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = Random.Range(ConfigurationUtils.RandomBallSpawnerMin, ConfigurationUtils.RandomBallSpawnerMax);
        spawnTimer.Run();
        //add the SpawnTimerFinished method as a listener of the spawnTimer's TimerFinishedEvent
        spawnTimer.AddTimerFinishedEventListener(SpawnTimerFinished);

        //set the speedup Timer
        speedupTimer = gameObject.AddComponent<Timer>();
        speedupTimer.Duration = ConfigurationUtils.SpeedupTimerDuration;
        //add the StopSpeedingUp method as a listener of the speedupTimer's TimerFinished Event
        speedupTimer.AddTimerFinishedEventListener(StopSpeedingUp);

        //add the SpeedupBalls method as a listener of the SpeedUp Event
        EventManager.AddEventListener(EventName.SpeedupEvent, SpeedupBalls);

        //add this as an invoker for the SpawnBall Event
        events.Add(EventName.SpawnBallEvent, new SpawnBallEvent());
        EventManager.AddEventInvoker(EventName.SpawnBallEvent, this);

        //add the SpawnBall method as a listener of the SpawnBall Event
        EventManager.AddEventListener(EventName.SpawnBallEvent, SpawnBall);

        //add this as an invoker for the HUD's ReduceBallsLeft event
        events.Add(EventName.ReduceBallsLeftEvent, new ReduceBallsLeftEvent());
        EventManager.AddEventInvoker(EventName.ReduceBallsLeftEvent, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBall(int useless = 0) //useless is a parameter that allow us to call all listeners as UnityAction<int>
    {
        if (Physics2D.OverlapArea(lowerLeftCorner, upperRightCorner) == null)
        {
            GameObject newBall = Instantiate<GameObject>(ballPrefab);
            if (speedingUp)
            {
                newBall.GetComponent<Rigidbody2D>().velocity *= 2;
            }

            events[EventName.ReduceBallsLeftEvent].Invoke(0); //0 is a useless parameter that allow us to call all events as UnityEvents<int>

            ballInQueue = false;
        }
        else
        {
            ballInQueue = true;
            waitingTimer.Run();
        }
    }

    // the listener for the Speedup Event
    public void SpeedupBalls(int useless = 0) //useless is a parameter that allow us to call all listeners as UnityAction<int>
    {
        if (!speedingUp)
        {
            //turn on the flag (to double the speed of future balls)
            speedingUp = true;

            //double the speed for existing balls
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject ball in balls)
            {
                ball.GetComponent<Rigidbody2D>().velocity *= 2;
            }

            //run the effect timer
            speedupTimer.Run();
        }
        else
            speedupTimer.AddSeconds(ConfigurationUtils.SpeedupTimerDuration);
    }

    public void SpawnTimerFinished(int useless = 0) //useless is a parameter that allow us to call all listeners as UnityAction<int> 
    {
        events[EventName.SpawnBallEvent].Invoke(0);
        spawnTimer.Duration = Random.Range(ConfigurationUtils.RandomBallSpawnerMin, ConfigurationUtils.RandomBallSpawnerMax);
        spawnTimer.Run();
    }

    public void StopSpeedingUp(int useless = 0) //useless is a parameter that allow us to call all listeners as UnityAction<int>
    {
        //slow down all the existing balls
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<Rigidbody2D>().velocity *= 0.5f;
        }

        //turn off the flag (to stop the speed-doubling after spawning)
        speedingUp = false;
    }

}
