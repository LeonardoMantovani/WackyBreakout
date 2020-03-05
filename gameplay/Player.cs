using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The player paddle
/// </summary>
public class Player : MonoBehaviour
{
    #region Fields

    //KeepInScreen support
    float halfColliderX;

    //Player's RigidBody2D component
    Rigidbody2D rb2d;

    //Freezer Effect Support
    Timer freezeTimer; //(to freeze the paddle when hit a freezer block)
    bool freezed = false; //(to move the paddle only if it isn't freezed)

    ////SpeedUp Effect Support
    //Timer speedupTimer; //(to speed up all the balls in the scene when hit a speedup block)
    //Vector2 standardVelocity; //(The velocity to double and restore after the timer is finished)
    //bool speedupFlag = false; //(to speedup all the balls only if needed)

    #endregion

    #region Methods

    // Start is called before the first frame update
    void Start()
    {
        //get half collider's X (we don't need Y because the Player moves only horizontallu)
        halfColliderX = GetComponent<BoxCollider2D>().size.x / 2;

        //get the rigidbody2d component
        rb2d = GetComponent<Rigidbody2D>();

        //setting up the freeze timer 
        freezeTimer = gameObject.AddComponent<Timer>();
        freezeTimer.Duration = ConfigurationUtils.FreezerTimerDuration;
        //add the Unfreeze method as a listener of the freezeTimer's TimerFinished Event
        freezeTimer.AddTimerFinishedEventListener(Unfreeze);

        //add the FreezeUp method as a listener of the Freezer Event
        EventManager.AddEventListener(EventName.FreezerEvent, FreezeUp);

        ////setting up the speedup timer
        //speedupTimer = gameObject.AddComponent<Timer>();
        //speedupTimer.Duration = 2; //TODO: change with a ConfigurationUtils value

        ////add the SpeedupBalls method as a listener of the SpeedUp Event
        //EventManager.AddSpeedupEventListener(SpeedupBalls);
    }

    // Update is called once per frame
    void Update()
    {
        ////unfreeze the paddle if the freezeTimer has finished
        //if (freezed && freezeTimer.Finished)
        //{
        //    freezed = false;
        //}

        //if (speedupFlag == true && speedupTimer.Finished == false)
        //{
        //    GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        //    if (!speedupTimer.Finished)
        //    {
        //        foreach (GameObject ball in balls)
        //        {
        //            ball.GetComponent<Rigidbody2D>().velocity = standardVelocity * 2;
        //        }
        //    }
        //    else if (speedupTimer.Finished)
        //    {
        //        foreach (GameObject ball in balls)
        //        {
        //            ball.GetComponent<Rigidbody2D>().velocity = standardVelocity;
        //        }

        //        speedupFlag = false;
        //    }
        //}
    }

    // Fixed Update is called 50 times per second (used for physics)
    private void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0 && !freezed)
        {
            Vector2 newPosition = rb2d.position;

            newPosition.x += moveInput * ConfigurationUtils.PlayerMoveUnitsPerSecond * Time.fixedDeltaTime;

            newPosition.x = CheckX(newPosition.x);

            rb2d.MovePosition(newPosition);
        }
    }

    /// <summary>
    /// check if the new X will be inside the game window and fix it if it doesn't
    /// </summary>
    /// <param name="possibleNewX">The calculated X where we want to move the player</param>
    /// <returns>An X inside the screen where we can move the player</returns>
    float CheckX(float possibleNewX)
    {
        float newX;

        if (possibleNewX - halfColliderX < ScreenUtils.ScreenLeft)
            newX = ScreenUtils.ScreenLeft + halfColliderX;
        else if (possibleNewX + halfColliderX > ScreenUtils.ScreenRight)
            newX = ScreenUtils.ScreenRight - halfColliderX;
        else
            newX = possibleNewX;

        return newX;
    }

    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            // calculate new ball direction
            float ballOffsetFromPaddleCenter = transform.position.x - coll.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter / halfColliderX;
            float angleOffset = normalizedBallOffset * ConfigurationUtils.PaddleBounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // tell ball to set direction to new direction
            Ball ballScript = coll.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }

    public void FreezeUp(int useless = 0) //useless is a parameter that allow us to call all listeners as UnityAction<int>
    {
        if (!freezed)
        {
            freezed = true;
            freezeTimer.Run();
        }
        else
            freezeTimer.AddSeconds(ConfigurationUtils.FreezerTimerDuration);
    }

    //unfreeze the paddle if the freezeTimer has finished
    public void Unfreeze(int useless = 0) //useless is a parameter that allow us to call all listeners as UnityAction<int>
    {
        freezed = false;
    }

    #endregion
}
