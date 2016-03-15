using UnityEngine;
using System.Collections;
using Base.Game;
using Base.Game.State;
using Base.Audio;

namespace Base.UI.State {

    /// <summary>
    /// Not yet finished. acts as dummy.
    /// </summary>
    public class GameUIState : BaseUIState {

        public BaseGameState nextGameState;

        public override void Enter () {
            base.Enter();
            Game.GameStateSelector.Instance.SetState("InGameState");

            AudioObject song = MusicManager.Instance.GetSongByName("GameMusic");
            song.FadeVolume(0,1, 5);
            StartCoroutine(MusicManager.Instance.TryToPlaySong(song));

        }

        public override IEnumerator Exit () {

            Effect.EffectManager.Instance.FadeEffect.onFadeFinished += FadeEffect_onFadeFinished;
			Score.ScoreManager.Instance.FinaliseScore();
            AudioObject song = MusicManager.Instance.GetSongByName("GameMusic");
            song.FadeVolume(1,0, 5);
            StartCoroutine(MusicManager.Instance.StopSong(song));

            yield return StartCoroutine(Effect.EffectManager.Instance.FadeEffect.Fade(1));
            GameStateSelector.Instance.SetState(nextGameState);

        }

        private void FadeEffect_onFadeFinished () {
			
            Effect.EffectManager.Instance.FadeEffect.onFadeFinished -= FadeEffect_onFadeFinished;
            Time.timeScale = 1;

        }

    }

}