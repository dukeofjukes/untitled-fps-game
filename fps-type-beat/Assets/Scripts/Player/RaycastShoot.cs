using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME: this entire file has the potential to be refactored to utilize the CharacterManager class
// depends on gun object references and such. especially when it comes to the shooting visual effect in 
// the camera.
public class RaycastShoot : MonoBehaviour {
  public PlayerMovement playerObject;

  public int gunDamage = 5;
  public float fireRate = .25f;
  public float weaponRange = 50f;
  public float hitForce = 100f;
  public Transform gunEnd;

  private Camera fpsCam;
  private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
  //private AudioSource gunAudio;
  private AudioManager audioManager;
  
  private LineRenderer laserLine;
  private float nextFire;

  void Start() {
    //gunAudio = GetComponent<AudioSource>();
    audioManager = FindObjectOfType<AudioManager>();
    laserLine = GetComponent<LineRenderer>();
    fpsCam = GetComponentInParent<Camera>();
  }

  void Update() {
    if (Input.GetButtonDown("Fire1") && Time.time > nextFire) {
      nextFire = Time.time + fireRate;

      StartCoroutine(ShotEffect());

      Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
      RaycastHit hit;

      laserLine.SetPosition(0, gunEnd.position);

      if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange)) {
        laserLine.SetPosition(1, hit.point);

        // handle health damage to shootable objects:
        EnemyHealth target = hit.collider.GetComponentInParent<EnemyHealth>();
        if (target != null) {
          // if the coroutine is currently running (hitmark is visible and fading), stop it:
          if (playerObject.hudManager.hitmarkVisible) {
            StopCoroutine(playerObject.hudManager.showHitmark());
          }
          StartCoroutine(playerObject.hudManager.showHitmark()); // then, start a new coroutine
          target.Damage(gunDamage);
        }
      } else {
        laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
      }
    }
  }

  private IEnumerator ShotEffect() {
    audioManager.PlayAtPoint("SFX_Player_Shoot", transform.position);

    laserLine.enabled = true;
    yield return shotDuration;
    laserLine.enabled = false;
  }
}
