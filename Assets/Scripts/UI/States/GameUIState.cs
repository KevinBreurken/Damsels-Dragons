using UnityEngine;
using System.Collections;

namespace Base.UI.States {

    public class GameUIState : BaseUIState {

        // Use this for initialization
        void Start () {

        }

        // Update is called once per frame
        void Update () {

            if (Input.GetKeyDown(KeyCode.Space)) {
                StartCoroutine(UIStateSelector.Instance.SetUIState("MenuUIState"));
            }

        }
    }

}