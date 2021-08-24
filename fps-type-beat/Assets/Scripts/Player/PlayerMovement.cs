using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Intended for use with first person movement system. 
*/
public class PlayerMovement : CharacterManager {
  public HUDManager hudManager;

  void Update() {
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
      
      StartCoroutine(flipPlayer());
    }
  }

  public IEnumerator flipPlayer() {
    //TODO: rotate 180 degrees, gravity shouldn't have to switch because its local to the Player GameObject
    //while (not on surface) {
      //keep flipping
      yield return null;
    //}
  }
}
