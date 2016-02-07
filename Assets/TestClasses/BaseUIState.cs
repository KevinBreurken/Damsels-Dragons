using UnityEngine;
using System.Collections;

public class BaseUIState : MonoBehaviour {

    public delegate void StateSwitch (string _state);
    public event StateSwitch OnStateSwitch;

    public delegate void ClickEvent ();
    public event ClickEvent onCLick;

    void Awake () {

        this.gameObject.SetActive(false);

    }

    public virtual IEnumerator Exit () {

        yield return null;
        this.gameObject.SetActive(false);

    }

    public virtual void Enter () {

        this.gameObject.SetActive(true);

    }

    public void SwitchState (string _state) {

        OnStateSwitch(_state);

    }

    public void Click () {

        onCLick();

    }

}
