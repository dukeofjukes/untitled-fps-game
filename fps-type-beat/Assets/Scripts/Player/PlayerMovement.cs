using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Intended for use with first person movement system. 
*/
public class PlayerMovement : CharacterManager {
  public HUDManager hudManager;
  public float rotationRate = 1f; // in degrees per FixedUpdate call

  void FixedUpdate() {
    GetInputMovement();
    ApplyMovement(); // inherited from CharacterManager
  }

  /*
    Assign x, y, z with user input.
  */
  private void GetInputMovement() {
    // get player wasd input:
    this.xMovement = Input.GetAxis("Horizontal");
    this.zMovement = Input.GetAxis("Vertical");

    // apply jump velocity:
    // FIXME: apply flipping velocity here:
    if (Input.GetButtonDown("Jump") && isGrounded) {
      //velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

      // start flipping coroutine, 
      gravityDirection *= -1; // flip the gravity direction in CharacterManager parent class
      StartCoroutine(flipPlayer());
    }
  }

  public IEnumerator flipPlayer() {
    // Quaternion originalRotation = transform.rotation;
    Vector3 targetAngle = transform.eulerAngles + 180f * Vector3.forward;

    do {
      // transform.Rotate(0f, 0f, rotationRate);
      // if (Mathf.Abs(transform.eulerAngles.z - targetAngle.z) < 2)
        // break;

      // transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngle, rotationRate * Time.deltaTime);
      float newZ = Mathf.Lerp(transform.eulerAngles.z, targetAngle.z, rotationRate * Time.deltaTime);
      transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, newZ);
      yield return null;
    } while (transform.eulerAngles.z != targetAngle.z/*Mathf.Abs(transform.eulerAngles.z - targetAngle.z) < 2f*/);

    //transform.eulerAngles = targetAngle;
    // transform.SetPositionAndRotation(transform.position, originalRotation);
  }
}
