using UnityEngine;
using System.Collections;

namespace Base.Game {
	
    /// <summary>
    /// The pickup thats the playable character can pickup.
    /// </summary>
	public class CoinPickup : MonoBehaviour {

        /// <summary>
        /// The GameObject that holds the graphic of the coin.
        /// </summary>
        public GameObject coinRenderer;

        /// <summary>
        /// The particle effect that is played when the player picks up the coin.
        /// </summary>
		public ParticleSystem particleEffect;

        private Collider2D coinCollider;

        void Awake () {
			
			coinCollider = GetComponent<Collider2D>();
		
		}

        /// <summary>
        /// Picks up the coin, and disables the object.
        /// </summary>
		public void Pickup () {
			
			coinCollider.enabled = false;
			coinRenderer.SetActive(false);
            Score.ScoreManager.Instance.AddScore(30);
			particleEffect.Stop();
			particleEffect.Play();

		}

        /// <summary>
        /// Resets this CoinPickup.
        /// </summary>
		public void Reset () {
			
			coinCollider.enabled = true;
			coinRenderer.SetActive(true);

		}

	}

}
