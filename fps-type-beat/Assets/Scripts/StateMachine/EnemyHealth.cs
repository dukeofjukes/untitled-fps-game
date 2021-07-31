using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
  public int maxHealth = 100;
  public int health;
  public EnemyManager enemyManager;
  //private AudioSource deathAudio;

  void Start() {
    //deathAudio = GetComponent<AudioSource>();
    health = maxHealth; // may need to change this depending on how level-loading system works, we'll want to remember the health across levels
    enemyManager = gameObject.GetComponent<EnemyManager>();
  }
  
  public void Damage(int damageAmount = 5) {
    health -= damageAmount;

    // wake up enemy, set to AttackState:
    if (enemyManager.currentState is PatrolState) {
      enemyManager.currentState.getShot();
    }

    if (health <= 0) {
      //deathAudio.Play();
      Destroy(gameObject);
    }
  }
}
