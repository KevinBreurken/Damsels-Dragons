using UnityEngine;
using System.Collections;

namespace Base.Game {
	
	public class CoinPickup : MonoBehaviour {

		private SpriteRenderer spriteRenderer;
		private Collider2D collider;

		void Awake () {
			
			collider = GetComponent<Collider2D>();
			spriteRenderer = GetComponent<SpriteRenderer>();

		}


		public void Pickup () {
			
			collider.enabled = false;
			spriteRenderer.enabled = false;
            Score.ScoreManager.Instance.AddScore(100);

		}

		public void Reset () {
			
			collider.enabled = true;
			spriteRenderer.enabled = true;

		}

	}

}
