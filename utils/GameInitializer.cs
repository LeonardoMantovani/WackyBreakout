using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Run the ScreenUtils.Initialize() method at the very beginning of the game
/// </summary>
public class GameInitializer : MonoBehaviour
{
    // Awake is called before Start()
    private void Awake()
    {
        ScreenUtils.Initialize();
        ConfigurationUtils.Initialize();
        EventManager.Initialize();
    }
}
