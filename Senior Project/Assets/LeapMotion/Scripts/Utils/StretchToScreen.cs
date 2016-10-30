/******************************************************************************\
<<<<<<< HEAD
* Copyright (C) Leap Motion, Inc. 2011-2016.                                   *
=======
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
>>>>>>> 4a3e1d6546375caea4e12609c9be6e8d42cb59fb
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections;

<<<<<<< HEAD
namespace Leap.Unity{
  public class StretchToScreen : MonoBehaviour {
  
    void Awake() {
      GetComponent<GUITexture>().pixelInset = new Rect(0.0f, 0.0f, Screen.width, Screen.height);
    }
  }
}
=======
public class StretchToScreen : MonoBehaviour {

  void Awake() {
    GetComponent<GUITexture>().pixelInset = new Rect(0.0f, 0.0f, Screen.width, Screen.height);
  }
}

>>>>>>> 4a3e1d6546375caea4e12609c9be6e8d42cb59fb
