using UnityEngine;
using System.Collections;
using DG.Tweening;
using Base.Management;

namespace Base.Game {

    
    public class ProjectileMovement : MonoBehaviour {

        public LayerMask hitMask;
        public Ease easeType;
		private float speedFactor;
		public float SpeedFactor {

			get {

				return speedFactor;

			}

			set {

				speedFactor = value;
				if(tweener != null) {
					tweener.timeScale = speedFactor;
				}

			}

		}

		public ProjectileManager manager;
		public Sequence tweener;
        public void StartMove () {

            Vector2 moveToPosition = GetMoveToPosition();
			transform.DOMoveX(moveToPosition.x,0.35f);
			transform.DOMoveY(moveToPosition.y + 1.5f, 0.35f).OnComplete(Bounce);

        }

        public void Bounce () {

            Vector2 moveToPosition = GetMoveToPosition();
            moveToPosition.y += 1.5f;

			tweener = transform.DOJump(moveToPosition, 5, 1, 1).OnComplete(Bounce).SetEase(easeType);
			tweener.timeScale = speedFactor;

        }

        private Vector2 GetMoveToPosition () {

            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + new Vector2(-3, 10), -Vector2.up, 20, hitMask.value);
            if (hit.collider != null) {

                return hit.transform.position;

            }

			ReturnToPool();
            return new Vector2(0, 0);

        }

		private void ReturnToPool () {
			
			manager.RemoveFromList(this);
			GetComponent<ObjectPoolReturnReference>().ReturnToPool();

		}

    }

}