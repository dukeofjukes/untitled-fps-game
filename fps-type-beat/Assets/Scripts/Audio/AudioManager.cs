using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public Sound[] sounds;

  public static AudioManager instance; // ensures Singleton

  /*
    Runs before Start().
  */
  private void Awake() {
    // ensure singleton object:
    if (!instance) {
      instance = this;
    } else {
      Destroy(gameObject);
      return;
    }

    DontDestroyOnLoad(gameObject); // audio manager persists through scenes
    
    // populate AudioSource variables:
    foreach (Sound s in sounds) {
      s.source = gameObject.AddComponent<AudioSource>();
      s.source.clip = s.clip;
      s.source.volume = s.volume;
      s.source.pitch = s.pitch;
      s.source.loop = s.loop;
      s.source.playOnAwake = s.playOnAwake;
      s.source.spatialBlend = s.spatialBlend;
    }
  }

  /*
    Plays the AudioSource of a sound object, while handling nullref exception.
  */
  /*
  public void Play(string name) {
    Sound s = Array.Find(sounds, sound => sound.name == name);

    if (s == null) {
      Debug.LogWarning("Sound: " + name + "not found! (possible typo).");
      return;
    }

    s.source.Play();
  }
  */

  /*
    Plays the AudioSource of a sound object at the specified position 
  */
  public AudioSource PlayAtPoint(string name, Vector3 position) {
    Sound s = Array.Find(sounds, sound => sound.name == name);

    if (s == null) {
      Debug.LogWarning("Sound: " + name + "not found! (possible typo).");
      return null;
    }

    /* This code should work while retaining audio source parameters, but doesn't. I suspect "aSource = s.source" assignment to be the culprit.
    GameObject tempGO = new GameObject("TempAudio");
    tempGO.transform.position = position;
    AudioSource aSource = tempGO.AddComponent<AudioSource>();
    aSource = s.source;
    aSource.Play();
    Destroy(tempGO, s.clip.length);
    return aSource;
    */
    AudioSource.PlayClipAtPoint(s.source.clip, position, s.source.volume); // works, but doesn't retain other source parameters
    return s.source;
  }
}
