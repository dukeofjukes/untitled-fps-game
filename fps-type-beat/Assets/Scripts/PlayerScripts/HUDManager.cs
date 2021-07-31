using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
  [Header ("HUD Object References")]
  public Image hitmark;
  public float hitmarkVisibleDuration = 1f;

  private void Start() {
    hitmark.enabled = false;
  }

  public IEnumerator showHitmark() {
    hitmark.enabled = true;
    yield return hitmarkVisibleDuration;
    hitmark.enabled = false;
  }
}
