using UnityEngine;
using System.Collections;

namespace Base.Game {
	
	public class CoinPickup : MonoBehaviour {

		public GameObject coinRenderer;
		private Collider2D collider;
		public ParticleSystem system;

		void Awake () {
			
			collider = GetComponent<Collider2D>();
		
		}


		public void Pickup () {
			
			collider.enabled = false;
			coinRenderer.SetActive(false);
            Score.ScoreManager.Instance.AddScore(100);
			system.Stop();
			system.Play();

		}

		public void Reset () {
			
			collider.enabled = true;
			coinRenderer.SetActive(true);

		}

	}

}
