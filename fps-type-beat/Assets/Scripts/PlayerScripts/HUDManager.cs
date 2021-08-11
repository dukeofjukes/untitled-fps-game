using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
  [Header ("HUD Object References")]
  public Image hitmark;
  public float hitmarkVisibleDuration = 0.3f;
  public float hitmarkFadeAwayDuration = 0.3f;

  private void Start() {
    hitmark.color = new Color(1, 1, 1, 0);
  }

  public IEnumerator showHitmark() {
    hitmark.color = new Color(1, 1, 1, 1);
    //FIXME: figure out how to allow interruptions of the same coroutine, since i want the hitmark to persist on multiple shots

    yield return new WaitForSeconds(hitmarkVisibleDuration);

    for (float i = 1; i >= 0; i -= Time.deltaTime / hitmarkFadeAwayDuration) {
      hitmark.color = new Color(1, 1, 1, i);
      yield return null;
    }
  }
}
