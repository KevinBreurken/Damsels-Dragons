using UnityEngine;
using System.Collections;

namespace Base.UI.States {

    public class MenuUIState : BaseUIState {

        public override void Enter () {

            base.Enter();

        }

        // Update is called once per frame
        void Update () {

            if (Input.GetKeyDown(KeyCode.Space)) {
                StartCoroutine(UIStateSelector.Instance.SetUIState("GameUIState"));
            }

        }

    }

}