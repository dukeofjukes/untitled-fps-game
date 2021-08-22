using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
  public int maxHealth = 100;
  public int health;
  public EnemyManager enemyManager;
  private AudioManager audioManager;

  public Renderer modelRenderer;
  private Material material;
  private float originalR, originalG, originalB;

  void Start() {
    audioManager = FindObjectOfType<AudioManager>();
    health = maxHealth; // may need to change this depending on how level-loading system works, we'll want to remember the health across levels
    enemyManager = gameObject.GetComponent<EnemyManager>();
    material = modelRenderer.material;
    originalR = material.color.r;
    originalG = material.color.g;
    originalB = material.color.b;
  }
  
  public void Damage(int damageAmount = 5) {
    health -= damageAmount;
    float colorDamageMultiplier = damageAmount / (float)maxHealth;
    float damagedR = material.color.r - (colorDamageMultiplier * originalR);
    float damagedG = material.color.g - (colorDamageMultiplier * originalG);
    float damagedB = material.color.b - (colorDamageMultiplier * originalB);
    material.color = new Color(damagedR, damagedG, damagedB, material.color.a);

    // wake up enemy, set to AttackState:
    if (enemyManager.currentState is PatrolState) {
      enemyManager.currentState.getShot();
    }

    if (health <= 0) {
      audioManager.Play("SFX_Enemy_Death");
      Destroy(gameObject);
    }
  }
}
