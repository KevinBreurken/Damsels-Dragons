using UnityEngine;
using System.Collections;
using Base.UI;
using Base.UI.States;
using DG.Tweening;

namespace Base.UI.States {

	public class SplashScreenState : BaseUIState {

		public CanvasGroup canvasGroup;
		public int waitTime;

		void Awake(){
			
			canvasGroup = GetComponent<CanvasGroup>();
			canvasGroup.alpha = 0;
		}

		public override void Enter ()
		{
			base.Enter ();
			canvasGroup.DOFade(1,1);
			StartCoroutine("WaitForFade");
		}

		IEnumerator WaitForFade() {
			
			yield return new WaitForSeconds(waitTime);
			Debug.Log("as");
			canvasGroup.DOFade(0,2);

			yield return new WaitForSeconds(2.5f);

			UIStateSelector.Instance.SetUIState("MenuUIState");

		}
			
	}

}