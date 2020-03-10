using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventName
{
    //Gameplay Events
    FreezerEvent,
    SpeedupEvent,
    SpawnBallEvent,
    BlockDestroyedEvent,
    GameOverEvent,
    BallsFinishedEvent,

    //HUD Events
    AddPointsEvent,
    ReduceBallsLeftEvent,
}
