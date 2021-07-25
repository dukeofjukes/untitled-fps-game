using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  public float moveSpeed = 50f;
  public float destroyBulletInterval = 7f;
  private float destroyBulletTimer;
  private Vector3 shootDir;

  void Start() {
    destroyBulletTimer = destroyBulletInterval;
  }

  public void Setup(Vector3 shootDir) {
    this.shootDir = shootDir;
  }

  void Update() {
    destroyBulletTimer -= Time.deltaTime; // get time since instantiation

    // destroy this bullet if its existed too long or it hits something:
    if (destroyBulletTimer <= 0 /*or collision*/) {
      Destroy(gameObject);
      return;
    }

    transform.position += shootDir * moveSpeed * Time.deltaTime; // move bullet
  }
}
