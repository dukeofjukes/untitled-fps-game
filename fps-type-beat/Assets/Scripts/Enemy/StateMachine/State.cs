using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour {
  public EnemyManager enemyManager;
  protected bool isShot;

  public abstract State RunCurrentState();
  public abstract void getShot();
}
