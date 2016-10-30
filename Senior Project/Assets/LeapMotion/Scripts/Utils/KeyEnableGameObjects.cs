using UnityEngine;
using System.Collections;
using System.Collections.Generic;

<<<<<<< HEAD
namespace Leap.Unity{
  public class KeyEnableGameObjects : MonoBehaviour {
    public List<GameObject> targets;
    [Header("Controls")]
    public KeyCode unlockHold = KeyCode.RightShift;
    public KeyCode toggle = KeyCode.T;
  
  	// Update is called once per frame
  	void Update () {
      if (unlockHold != KeyCode.None &&
          !Input.GetKey (unlockHold)) {
        return;
      }
  	  if (Input.GetKeyDown (toggle)) {
        foreach (GameObject target in targets) {
          target.SetActive(!target.activeSelf);
        }
      }
  	}
  }
}
=======
public class KeyEnableGameObjects : MonoBehaviour {
  public List<GameObject> targets;
  [Header("Controls")]
  public KeyCode unlockHold = KeyCode.RightShift;
  public KeyCode toggle = KeyCode.T;

	// Update is called once per frame
	void Update () {
    if (unlockHold != KeyCode.None &&
        !Input.GetKey (unlockHold)) {
      return;
    }
	  if (Input.GetKeyDown (toggle)) {
      foreach (GameObject target in targets) {
        target.SetActive(!target.activeSelf);
      }
    }
	}
}
>>>>>>> 4a3e1d6546375caea4e12609c9be6e8d42cb59fb
