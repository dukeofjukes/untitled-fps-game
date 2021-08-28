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
      StartCoroutine(flipPlayer());
    }
  }

  public IEnumerator flipPlayer() {
    Quaternion originalRotation = transform.rotation;
    gravityDirection *= -1; // flip the gravity direction in CharacterManager parent class
    do {
      transform.Rotate(0f, 0f, rotationRate);
      yield return null;
    } while (transform.rotation.z % 180 != 0);
    transform.SetPositionAndRotation(transform.position, originalRotation);
  }
}
