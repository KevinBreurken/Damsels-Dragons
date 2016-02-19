using UnityEngine;
using System.Collections;

namespace Base.UI.State {

    /// <summary>
    /// Not yet finished. acts as dummy.
    /// </summary>
    public class MenuUIState : BaseUIState {

        public UIButton startButton;
        public UIButton optionsButton;
        public UIButton creditsButton;
        public UIButton quitButton;


        public override void Enter () {

            base.Enter();

            startButton.Show();
            optionsButton.Show();
            creditsButton.Show();
            quitButton.Show();

        }

        // Update is called once per frame
        void Update () {


        }

    }

}