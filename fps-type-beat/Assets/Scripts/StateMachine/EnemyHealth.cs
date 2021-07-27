using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
  public int maxHealth = 100;
  public int health;
  //private AudioSource deathAudio;

  void Start() {
    //deathAudio = GetComponent<AudioSource>();
    health = maxHealth; // may need to change this depending on how level-loading system works, we'll want to remember the health across levels
  }
  
  public void Damage(int damageAmount = 5) {
    health -= damageAmount;

    if (health <= 0) {
      //deathAudio.Play();
      Destroy(gameObject);
    }
  }
}
