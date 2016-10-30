using UnityEngine;
using System.Collections;
using System.Collections.Generic;

<<<<<<< HEAD
namespace Leap.Unity{
  public class KeyEnableBehaviors : MonoBehaviour {
    public List<Behaviour> targets;
    [Header("Controls")]
    public KeyCode unlockHold = KeyCode.None;
    public KeyCode toggle = KeyCode.Space;
    
    // Update is called once per frame
    void Update () {
      if (unlockHold != KeyCode.None &&
          !Input.GetKey (unlockHold)) {
        return;
      }
      if (Input.GetKeyDown (toggle)) {
        foreach (MonoBehaviour target in targets) {
          target.enabled = !target.enabled;
        }
=======
public class KeyEnableBehaviors : MonoBehaviour {
  public List<Behaviour> targets;
  [Header("Controls")]
  public KeyCode unlockHold = KeyCode.None;
  public KeyCode toggle = KeyCode.Space;
  
  // Update is called once per frame
  void Update () {
    if (unlockHold != KeyCode.None &&
        !Input.GetKey (unlockHold)) {
      return;
    }
    if (Input.GetKeyDown (toggle)) {
      foreach (MonoBehaviour target in targets) {
        target.enabled = !target.enabled;
>>>>>>> 4a3e1d6546375caea4e12609c9be6e8d42cb59fb
      }
    }
  }
}
