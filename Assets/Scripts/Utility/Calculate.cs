using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Math {

    /// <summary>
    /// Contains functions used for calculations.
    /// </summary>
    public class Calculate : MonoBehaviour {

        public static float GetHighestFromList (List<float> list) {

            float highest = 0;
            //get the highest value.
            for (int i = 0; i < list.Count; i++) {

                if (list[i] > highest) {
                    //Set the highest value.
                    highest = list[i];

                }

            }

            return highest;

        }

    }

}