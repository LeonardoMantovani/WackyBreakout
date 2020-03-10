using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioSource : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //if there isn't another audio source
        if (!AudioManager.Initialized)
        {
            //add an audio source to the game object
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();

            //initialize the AudioManager script
            AudioManager.Initialize(audioSource);

            //keep this game object after a scene change
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject); //becouse it's a duplicate
    }
}
