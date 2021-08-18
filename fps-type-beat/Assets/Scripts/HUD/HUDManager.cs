using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
  [Header ("HUD Object References")]
  public Image hitmark;
  public HealthBar healthBar;
  public float hitmarkVisibleDuration = 0.3f;
  public float hitmarkFadeAwayDuration = 0.3f;
  public bool hitmarkVisible = false;

  public PlayerHealth playerHealth;

  private void Start() {
    hitmark.color = new Color(1, 1, 1, 0);
    healthBar.setMaxHealth(playerHealth.maxHealth);
  }

  private void Update() {
    healthBar.setHealth(playerHealth.health);
  }

  public IEnumerator showHitmark() {
    hitmark.color = new Color(1, 1, 1, 1);
    hitmarkVisible = true;

    yield return new WaitForSeconds(hitmarkVisibleDuration);

    for (float i = 1; i >= 0; i -= Time.deltaTime / hitmarkFadeAwayDuration) {
      hitmark.color = new Color(1, 1, 1, i);
      yield return null;
    }
    hitmarkVisible = false;
  }
}
