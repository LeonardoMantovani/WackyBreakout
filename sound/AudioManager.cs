using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    #region Fields

    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();

    #endregion

    #region Propieties

    public static bool Initialized
    {
        get { return initialized; }
    }

    #endregion

    #region Methods

    public static void Initialize(AudioSource audioSource)
    {
        AudioManager.audioSource = audioSource;

        audioClips.Add(AudioClipName.BallDestroyed, Resources.Load<AudioClip>(@"audioClips\BallDestroyed"));
        audioClips.Add(AudioClipName.BlockDestroyed, Resources.Load<AudioClip>(@"audioClips\BlockDestroyed"));
        audioClips.Add(AudioClipName.FreezeEffect, Resources.Load<AudioClip>(@"audioClips\FreezeEffect"));
        audioClips.Add(AudioClipName.FreezeEffectFinished, Resources.Load<AudioClip>(@"audioClips\FreezeEffectFinished"));
        audioClips.Add(AudioClipName.SpeedupEffect, Resources.Load<AudioClip>(@"audioClips\SpeedupEffect"));
        audioClips.Add(AudioClipName.SpeedupEffectFinished, Resources.Load<AudioClip>(@"audioClips\SpeedupEffectFinished"));
        audioClips.Add(AudioClipName.GameOverVictory, Resources.Load<AudioClip>(@"audioClips\GameOverVictory"));
        audioClips.Add(AudioClipName.GameOverDefeat, Resources.Load<AudioClip>(@"audioClips\GameOverDefeat"));

        initialized = true;
    }

    public static void PlayAudioClip(AudioClipName clipName)
    {
        audioSource.PlayOneShot(audioClips[clipName]);
    }

    #endregion
}
