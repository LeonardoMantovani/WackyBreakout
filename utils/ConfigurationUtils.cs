using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ConfigurationUtils
{
    static ConfigurationData configurationData;
    
    #region Methods

    public static void Initialize()
    {
        configurationData = new ConfigurationData();
    }
    
    #endregion
    
    #region Ball Proprieties

    /// <summary>
    /// The Initial foce magnitude for the ball
    /// </summary>
    public static float BallInitialForceMagnitude
    {
        get { return configurationData.BallInitialForceMagnitude; }
    }

    /// <summary>
    /// The time before a ball destroy itself
    /// </summary>
    public static float BallDeathTimerDuration
    {
        get { return configurationData.BallDeathTimerDuration; }
    }

    /// <summary>
    /// The time before a ball starts moving
    /// </summary>
    public static float BallGetReadyTimerDuration
    {
        get { return configurationData.BallGetReadyTimerDuration; }
    }

    /// <summary>
    /// The minimum value for the Random BallSpawner Timer
    /// </summary>
    public static float RandomBallSpawnerMin
    {
        get { return configurationData.RandomBallSpawnerMin; }
    }

    /// <summary>
    /// The maximum value for the Random BallSpawner Timer
    /// </summary>
    public static float RandomBallSpawnerMax
    {
        get { return configurationData.RandomBallSpawnerMax; }
    }

    /// <summary>
    /// The number of balls the player have for every game
    /// </summary>
    public static float BallsPerGame
    {
        get { return configurationData.BallsPerGame; }
    }

    #endregion

    #region Player Proprieties

    /// <summary>
    /// Units a player move in a second
    /// </summary>
    public static float PlayerMoveUnitsPerSecond
    {
        get { return configurationData.PlayerMoveUnitsPerSecond; }
    }

    /// <summary>
    /// The angle for the fake-convex paddle coverted in radians
    /// </summary>
    public static float PaddleBounceAngleHalfRange
    {
        get { return configurationData.PaddleBounceAngleHalfRange * Mathf.Deg2Rad; }
    }

    #endregion

    #region Blocks Proprieties

    /// <summary>
    /// The percentage for the spawning of Standard Blocks
    /// </summary>
    public static float StandardBlocksPercentage
    {
        get { return configurationData.StandardBlocksPercentage; }
    }

    /// <summary>
    /// The percentage for the spawning of Bonus Blocks
    /// </summary>
    public static float BonusBlocksPercentage
    {
        get { return configurationData.BonusBlocksPercentage; }
    }

    /// <summary>
    /// The percentage for the spawning of Freezer Blocks
    /// </summary>
    public static float FreezerBlocksPercentage
    {
        get { return configurationData.FreezerBlocksPercentage; }
    }

    /// <summary>
    /// The percentage for the spawning of SpeedUp Blocks
    /// </summary>
    public static float SpeedUpBlocksPercentage
    {
        get { return configurationData.SpeedupBlocksPercentage; }
    }

    /// <summary>
    /// The points added to the score after destroying a Standard Block
    /// </summary>
    public static float StandardBlocksPoints
    {
        get { return configurationData.StandardBlocksPoints; }
    }

    /// <summary>
    /// The points added to the score after destroying a Bonus Block
    /// </summary>
    public static float BonusBlocksPoints
    {
        get { return configurationData.BonusBlocksPoints; }
    }

    /// <summary>
    /// The points added to the score after destroying a Freezer Block
    /// </summary>
    public static float FreezerBlocksPoints
    {
        get { return configurationData.FreezerBlocksPoints; }
    }

    /// <summary>
    /// The points added to the score after destroying a SpeedUp Block
    /// </summary>
    public static float SpeedupBlocksPoints
    {
        get { return configurationData.SpeedupBlocksPoints; }
    }

    #endregion

    #region Events Proprieties

    /// <summary>
    /// The duration of the Freezer PickUp Effect
    /// </summary>
    public static float FreezerTimerDuration
    {
        get { return configurationData.FreezerTimerDuration; }
    }

    /// <summary>
    /// The duration of the SpeedUp PickUp Effect
    /// </summary>
    public static float SpeedupTimerDuration
    {
        get { return configurationData.SpeedupTimerDuration; }
    }

    #endregion
}
