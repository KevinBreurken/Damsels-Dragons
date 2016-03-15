using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Base.Game;
using Base.Audio;
using UnityEngine.UI;

namespace Base.UI.State {
	
	/// <summary>
	/// Not yet finished. acts as dummy.
	/// </summary>
	public class ResultUIState : BaseUIState {

		public UIButton backButton;
		public UIButton retryButton;
		public UIObject textObject;
		public UIObject scoreObject;

		void Awake () {

			backButton.onClicked += BackButton_onClicked;
			retryButton.onClicked += RetryButton_onClicked;

		}

		private void BackButton_onClicked () {

			UIStateSelector.Instance.SetState("MenuUIState");

		}
			
		private void RetryButton_onClicked () {

			UIStateSelector.Instance.SetState("GameUIState");

		} 

		public override void Enter () {

			base.Enter();

			scoreObject.gameObject.GetComponentInChildren<Text>().text = "" + Score.ScoreManager.Instance.GetScore();

			AudioObject song = MusicManager.Instance.GetSongByName("MainMenuMusic");
			song.FadeVolume(0, 1, 5);
			StartCoroutine(MusicManager.Instance.TryToPlaySong(song));

			backButton.Show();
			GameStateSelector.Instance.SetState("OffGameState");
			StartCoroutine(ShowAnimation());

		}

		IEnumerator ShowAnimation () {

			textObject.Show();
			scoreObject.Show();
			backButton.Show();
			retryButton.Show();
			yield return new WaitForSeconds(1);

		}

		public override IEnumerator Exit () {

			StartCoroutine(backButton.Hide());
			StartCoroutine(retryButton.Hide());

			yield return StartCoroutine(Effect.EffectManager.Instance.FadeEffect.Fade(1));
			base.Exit();

		}

	}

}
