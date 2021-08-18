using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager {
  private AudioManager audioManager;

  [Header ("Enemy-specific Component References")]
  public State currentState; // holds starting state, displays current state during runtime
  public Transform target; // the player's transform

  [Header ("State Detection Triggers")]
  public float viewRadius = 20f; // detection radius
  public float viewAngle = 70f; // detection field-of-view angle (in degrees)
  public float attackRadius = 50f; // distance player has to escape to end attack state
  public float stopPursuitRadius = 10f; // "personal space" distance around the player, enemy will stop pursuit at this radius
  [HideInInspector]
  private Vector3 targetHeading; // base value used to calculate distance and direction (see below)
  public float targetDistance; // tracks the current distance from target
  public Vector3 targetDirection; // points from this enemy towards the target
  public LayerMask viewMask;

  [Header ("Attack State Variables")]
  public float timerStrafeInterval = 1f;
  public float timerJumpInterval = 2f;
  public float fireRateInterval = 1f;
  public GameObject bulletPrefab;
  public Transform POV;
  public Transform gunEnd;

  private void Start() {
    audioManager = FindObjectOfType<AudioManager>();
  }

  /*
    Perform global actions and refresh variables that aren't dependent on a state.
  */
  private void Update() {
    targetHeading = target.position - transform.position; // get a vector that points towards target
    targetDistance = targetHeading.magnitude; // refresh distance
    targetDirection = targetHeading / targetDistance; // refresh normalized direction

    HandleStateMachine();
    ApplyMovement(); // inherited from CharacterManager
  }

  /*
    Run the current state, and if a next state is returned, switch to it.
  */
  private void HandleStateMachine() {
    State nextState = currentState?.RunCurrentState(); // if variable is not null, run current state

    if (nextState != null) {
      SwitchToNextState(nextState);
    }
  }

  /*
    Switch to a given state. Called in HandleStateMachine().
  */
  private void SwitchToNextState(State nextState) {
    currentState = nextState;
  }
}
