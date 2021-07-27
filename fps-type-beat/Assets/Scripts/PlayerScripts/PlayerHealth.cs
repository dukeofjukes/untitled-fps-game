using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
  public int maxHealth = 100;
  public int health;

  void Start() {
    health = maxHealth; // may need to change this depending on how level-loading system works, we'll want to remember the health across levels
  }
  
  public void Damage(int damageAmount = 5) {
    health -= damageAmount;

    // TODO: gameover state if health <= 0
    /*
    if (health <= 0) {
      // game over
    }
    */
  }
}
