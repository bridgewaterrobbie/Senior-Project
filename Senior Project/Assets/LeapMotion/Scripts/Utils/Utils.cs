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
using Leap;

<<<<<<< HEAD
namespace Leap.Unity {
=======
namespace Leap {
>>>>>>> 4a3e1d6546375caea4e12609c9be6e8d42cb59fb

  public static class Utils {

    public static void IgnoreCollisions(GameObject first, GameObject second, bool ignore = true) {
      if (first == null || second == null)
        return;

      Collider[] first_colliders = first.GetComponentsInChildren<Collider>();
      Collider[] second_colliders = second.GetComponentsInChildren<Collider>();

      for (int i = 0; i < first_colliders.Length; ++i) {
        for (int j = 0; j < second_colliders.Length; ++j) {
          if (first_colliders[i] != second_colliders[j] &&
              first_colliders[i].enabled && second_colliders[j].enabled) {
            Physics.IgnoreCollision(first_colliders[i], second_colliders[j], ignore);
          }
        }
      }
    }

  }
}
