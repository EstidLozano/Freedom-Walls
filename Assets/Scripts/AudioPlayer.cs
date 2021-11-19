using UnityEngine;

/*
 * 
 *  // How to use it:
 *  public void TapOnButtonPlay() {
 *      AudioPlayer.playSoundFX("JumpSound");
 *      AudioPlayer.stopMusic("MusicLevel1");
 *      AudioPlayer.playMusic("MusicFinalBoss");
 *  }
 * 
 */

public class AudioPlayer {
    // Name of parent game object containing all the sound effects
    static string soundFxParentName = "SoundFx";

    // Name of parent game object containing all the music songs
    static string musicParentName = "Music";

    /// <summary>
    /// This function plays a sound effect assigned to a game object inside the SoundFX object.
    /// Each sound effect is a game object with an AudioSource attached to.
    /// </summary>
    /// <param name="nameSoundFxToPlay">Name of the sound effect to play</param>
    public static void playSoundFX(string nameSoundFxToPlay)
    {

        // Calculates the full name of the sound effect to play
        string soundFxFullName = soundFxParentName + "/" + nameSoundFxToPlay;

        // Find the game object that contains the required sound fx
        GameObject soundFxObject = GameObject.Find(soundFxFullName);

        // Check if the game object could be found
        if (soundFxObject == null)
        {
            Debug.LogError("AudioPlayer.playSoundFX(): GameObject '" + soundFxFullName + "' could not be found");
            Debug.Break();
            return;
        }

        // Search for the audio source component
        AudioSource audioComponent = soundFxObject.GetComponent<AudioSource>();

        // Check if the component could be found
        if (audioComponent == null)
        {
            Debug.LogError("AudioPlayer.playSoundFX(): Component 'AudioSource' could not be found");
            Debug.Break();
            return;
        }

        // Plays the audio clip attached to the audio source component
        audioComponent.playOnAwake = false;
        audioComponent.loop = false;
        audioComponent.Play();
    }

    /// <summary>
    /// This function plays a music song assigned to a game object inside the Music object.
    /// Each music is a game object with an AudioSource attached to.
    /// </summary>
    /// <param name="nameMusicToPlay">Name of the sound effect to play</param>
    public static void playMusic(string nameMusicToPlay)
    {

        // Calculates the full name of the music to play
        string musicFullName = musicParentName + "/" + nameMusicToPlay;

        // Find the game object that contains the required music
        GameObject musicObject = GameObject.Find(musicFullName);

        // Check if the game object could be found
        if (musicObject == null)
        {
            Debug.LogError("AudioPlayer.playMusic(): GameObject '" + musicFullName + "' could not be found");
            Debug.Break();
            return;
        }

        // Search for the audio source component
        AudioSource audioComponent = musicObject.GetComponent<AudioSource>();

        // Check if the component could be found
        if (audioComponent == null)
        {
            Debug.LogError("AudioPlayer.playMusic(): Component 'AudioSource' could not be found");
            Debug.Break();
            return;
        }

        // Plays the audio clip attached to the audio source component
        audioComponent.loop = true;
        audioComponent.Play();
    }

    /// <summary>
    /// This function stops a music song that is being played, clip assigned to a game object inside the Music object.
    /// Each music is a game object with an AudioSource attached to.
    /// </summary>
    /// <param name="nameMusicToStop">Name of the sound effect to play</param>
    public static void stopMusic(string nameMusicToStop)
    {

        // Calculates the full name of the sound effect to stop
        string musicFullName = musicParentName + "/" + nameMusicToStop;

        // Find the game object that contains the required music
        GameObject musicObject = GameObject.Find(musicFullName);

        // Check if the game object could be found
        if (musicObject == null)
        {
            Debug.LogError("AudioPlayer.stopMusic(): GameObject '" + musicFullName + "' could not be found");
            Debug.Break();
            return;
        }

        // Search for the audio source component
        AudioSource audioComponent = musicObject.GetComponent<AudioSource>();

        // Check if the component could be found
        if (audioComponent == null)
        {
            Debug.LogError("AudioPlayer.stopMusic(): Component 'AudioSource' could not be found");
            Debug.Break();
            return;
        }

        // Plays the audio clip attached to the audio source component
        audioComponent.loop = true;
        audioComponent.Stop();
    }
}

/*
 * 
 * Related:
 * 
 *   AudioPlayer: Script for Unity to play/stop sound fx and music
 *   https://gist.github.com/rsaenzi/119357d38d9c7255adfa8e465d486d0c
 * 
 *   UINavigation: Script for Unity to load/unload UI screens
 *   https://gist.github.com/rsaenzi/95df01c056d352651a9143b13ce305fa
 * 
 *   LevelLoader: Script for Unity to load/unload levels
 *   https://gist.github.com/rsaenzi/33cfff7d20e62c6f229442f7c6a7aa90
 * 
 */