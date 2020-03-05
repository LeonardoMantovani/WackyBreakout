using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ConfigurationData
{
    //the name of the ConfigurationData file
    const string ConfigurationDataFileName = "ConfigurationData.csv";

    #region Ball Fields

    float ballInitialForceMagnitude = 15;
    float ballDeathTimerDuration = 10;
    float ballGetReadyTimerDuration = 2;
    float randomBallSpawnerMin = 5;
    float randomBallSpawnerMax = 10;
    float ballsPerGame = 10;

    #endregion

    #region Ball Proprieties

    public float BallInitialForceMagnitude
    {
        get { return ballInitialForceMagnitude; }
    }

    public float BallDeathTimerDuration
    {
        get { return ballDeathTimerDuration; }
    }

    public float BallGetReadyTimerDuration
    {
        get { return ballGetReadyTimerDuration; }
    }

    public float RandomBallSpawnerMin
    {
        get { return randomBallSpawnerMin; }
    }

    public float RandomBallSpawnerMax
    {
        get { return randomBallSpawnerMax; }
    }

    public float BallsPerGame
    {
        get { return ballsPerGame; }
    }

    #endregion

    #region Paddle Fields

    float playerMoveUnitsPerSecond = 25;
    float paddleBounceAngleHalfRange = 60;

    #endregion

    #region Paddle Proprieties

    public float PlayerMoveUnitsPerSecond
    {
        get { return playerMoveUnitsPerSecond; }
    }
    public float PaddleBounceAngleHalfRange
    {
        get { return paddleBounceAngleHalfRange; }
    }

    #endregion

    #region Block Fields

    float standardBlocksPercentage = 70;
    float bonusBlocksPercentage = 20;
    float freezerBlocksPercentage = 5;
    float speedupBlocksPercentage = 5;

    float standardBlocksPoints = 1;
    float bonusBlocksPoints = 2;
    float freezerBlocksPoints = 5;
    float speedupBlocksPoints = 5;

    #endregion

    #region Block Proprieties

    public float StandardBlocksPercentage
    {
        get { return standardBlocksPercentage; }
    }
    public float BonusBlocksPercentage
    {
        get { return bonusBlocksPercentage; }
    }
    public float FreezerBlocksPercentage
    {
        get { return freezerBlocksPercentage; }
    }
    public float SpeedupBlocksPercentage
    {
        get { return speedupBlocksPercentage; }
    }

    public float StandardBlocksPoints
    {
        get { return standardBlocksPoints; }
    }
    public float BonusBlocksPoints
    {
        get { return bonusBlocksPoints; }
    }
    public float FreezerBlocksPoints
    {
        get { return freezerBlocksPoints; }
    }
    public float SpeedupBlocksPoints
    {
        get { return speedupBlocksPoints; }
    }

    #endregion

    #region Events Fields

    float freezerTimerDuration = 2;
    float speedupTimerDuration = 2;

    #endregion

    #region Events Proprieties

    public float FreezerTimerDuration
    {
        get { return freezerTimerDuration; }
    }

    public float SpeedupTimerDuration
    {
        get { return speedupTimerDuration; }
    }

    #endregion

    #region Constructors

    public ConfigurationData()
    {
        StreamReader input = null;
        try
        {
            //open coonfiguration file
            input = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));

            //save names (written in the first line of the file) and values (written in the second line of the file)
            string names = input.ReadLine();
            string values = input.ReadLine();

            //set fields with values from the file
            SetConfigurationDataFields(values);
        }
        catch (Exception e)
        {
        }
        finally
        {
            if (input != null)
                input.Close();
        }
    }

    #endregion

    #region Methods

    void SetConfigurationDataFields(string rawValues) 
    {
        string[] values = rawValues.Split(';');

        ballInitialForceMagnitude = float.Parse(values[0]);
        ballDeathTimerDuration = float.Parse(values[3]);
        ballGetReadyTimerDuration = float.Parse(values[4]);
        randomBallSpawnerMin = float.Parse(values[5]);
        randomBallSpawnerMax = float.Parse(values[6]);
        ballsPerGame = float.Parse(values[7]);

        playerMoveUnitsPerSecond = float.Parse(values[1]);
        paddleBounceAngleHalfRange = float.Parse(values[2]);

        standardBlocksPercentage = float.Parse(values[8]);
        bonusBlocksPercentage = float.Parse(values[9]);
        freezerBlocksPercentage = float.Parse(values[10]);
        speedupBlocksPercentage = float.Parse(values[11]);

        standardBlocksPoints = float.Parse(values[12]);
        bonusBlocksPoints = float.Parse(values[13]);
        freezerBlocksPoints = float.Parse(values[14]);
        speedupBlocksPoints = float.Parse(values[15]);

        freezerTimerDuration = float.Parse(values[16]);
        speedupTimerDuration = float.Parse(values[17]);
    }

    #endregion
}
