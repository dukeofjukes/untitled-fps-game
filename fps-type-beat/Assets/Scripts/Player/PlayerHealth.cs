using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
  public int maxHealth = 100;
  public int health;
  private AudioManager audioManager;

  void Start() {
    audioManager = FindObjectOfType<AudioManager>();
    health = maxHealth; // may need to change this depending on how level-loading system works, we'll want to remember the health across levels
  }
  
  public void Damage(int damageAmount = 5) {
    health -= damageAmount;
    audioManager.Play("SFX_Player_Damage");

    if (health <= 0) {
      audioManager.Play("SFX_Player_Death");
      // TODO: change game state to game over screen or whatever
    }
  }
}