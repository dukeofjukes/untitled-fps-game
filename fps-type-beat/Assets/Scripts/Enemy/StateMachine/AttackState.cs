using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State {
  public PatrolState patrolState;

  private float strafeTimer;
  private float jumpTimer;

  // shooting stuff:
  private float fireTimer;
  private WaitForSeconds shotDuration = new WaitForSeconds(.07f);

  private AudioManager audioManager;

  private void Start() {
    audioManager = FindObjectOfType<AudioManager>();
  }

  public override State RunCurrentState() {
    SetRandomMovement();

    // get the location of the target (not accounting for up/down look, since only the gun moves that direction)
    Vector3 bodyLookAtTarget = new Vector3(enemyManager.target.position.x,
                                       enemyManager.transform.position.y, // lock y axis
                                       enemyManager.target.position.z);
    enemyManager.transform.LookAt(bodyLookAtTarget); // look towards the target

    // get the y-axis position of the target:
    Vector3 gunLookAtTarget = new Vector3(enemyManager.target.position.x,
                                          enemyManager.target.position.y, // only get y axis
                                          enemyManager.target.position.z);
    enemyManager.POV.LookAt(gunLookAtTarget); // point gun up/down towards target
    
    HandleShooting(enemyManager.POV.forward);

    if (!isShot) {
      // handle state switching:
      if (enemyManager.targetDistance > enemyManager.attackRadius) {
        return patrolState; // switch states
      }
    }
    
    return this;
  }

  public override void getShot() {
    isShot = true;
  }

  /*
    Assign x, y, z with random movement.
  */
  private void SetRandomMovement() {
    // set timers:
    strafeTimer -= Time.deltaTime;
    jumpTimer -= Time.deltaTime;

    // randomize movement:
    if (enemyManager.targetDistance > enemyManager.stopPursuitRadius) { // outside of stopPursuitRadius, follow
      enemyManager.zMovement = (Random.value > 0.5) ? 0 : 1;
    } else { // within stopPursuitRadius "personal space", back up
      enemyManager.zMovement = (Random.value > 0.5) ? 0 : -1;
    }

    if (strafeTimer <= 0 && enemyManager.isGrounded) { // only switch strafe directions when grounded
      enemyManager.xMovement = (Random.value > 0.5) ? -1 : 1;
      strafeTimer = enemyManager.timerStrafeInterval;
    }

    if (jumpTimer <= 0) {
      // randomize jumping:
      if (Random.value > 0.5 && enemyManager.isGrounded) {
        enemyManager.velocity.y = Mathf.Sqrt(enemyManager.jumpHeight * -2f * enemyManager.gravity);
      }
      jumpTimer = enemyManager.timerJumpInterval;
    }
  }

  /*
    Handles shooting timers, effects, and any associated logic in the event of a landed shot.
  */
  private void HandleShooting(Vector3 shootDir) {
    // so, enemy should already be facing the target player in the attack state.
    // that means, we should apply some kind of projectile shoot (not raycast), that takes time to reach the player

    // set timer:
    fireTimer -= Time.deltaTime;
    
    if (fireTimer <= 0) {
      fireTimer = enemyManager.fireRateInterval; // reset timer

      StartCoroutine(ShotEffect()); // play sound effect

      // create bullet:
      GameObject bulletGameObject = Instantiate(enemyManager.bulletPrefab, enemyManager.gunEnd.position, enemyManager.gunEnd.rotation);
      bulletGameObject.GetComponent<Bullet>().Setup(shootDir);
    }
  }

  private IEnumerator ShotEffect() {
    audioManager.PlayAtPoint("SFX_Enemy_Shoot", transform.position);

    yield return shotDuration;
  }
}
