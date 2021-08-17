using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
  public int maxHealth = 100;
  public int health;
  public AudioSource hitAudio;
  //public AudioSource deathAudio;

  void Start() {
    health = maxHealth; // may need to change this depending on how level-loading system works, we'll want to remember the health across levels
  }
  
  public void Damage(int damageAmount = 5) {
    health -= damageAmount;
    hitAudio.Play();

    if (health <= 0) {
      FindObjectOfType<AudioManager>().Play("PlayerDeath");
      // TODO: change game state to game over screen or whatever
    }
  }
}