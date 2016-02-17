using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Base.UI {

    [RequireComponent(typeof(Button),typeof(Image))]
    public class UIButton : UIObject {

        // Use this for initialization
        void Start () {

        }

        // Update is called once per frame
        void Update () {

            #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.O)) {
                Debug.Log("asd");
                Show();
            }
            if (Input.GetKeyDown(KeyCode.P)) {
                Debug.Log("asd");
                Hide();
            }
#endif
        }
    }

}
