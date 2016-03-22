using UnityEngine;
using System.Collections;
using DG.Tweening;
using Base.Management;

namespace Base.Game {

    
    public class ProjectileMovement : MonoBehaviour {

        public LayerMask hitMask;
        public Ease easeType;
        public GameObject particlePrefab;
        private ParticleSystem instantiatedParticle;

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
        private bool isAtEnd;
        private Collider2D projectileCollider;

        void Awake () {

            projectileCollider = GetComponent<Collider2D>();

            //Check if theres a particle system for this Projectile.
            if(particlePrefab != null) {
                GameObject particleEffect = Instantiate(particlePrefab, transform.position, Quaternion.identity) as GameObject;
                //particleEffect.hideFlags = HideFlags.HideInHierarchy;
                instantiatedParticle = particleEffect.GetComponent<ParticleSystem>();
            }
        }

        public void StartMove () {

            if (instantiatedParticle != null) {

                instantiatedParticle.Stop();

            }
            StopAllCoroutines();
            transform.DOKill();
            isAtEnd = false;
            Vector2 moveToPosition = GetMoveToPosition();
            projectileCollider.enabled = true;
            Tweener tween = transform.DOMove(new Vector3(moveToPosition.x, moveToPosition.y + 1.5f, 0), 0.35f).OnComplete(Bounce).SetEase(Ease.Linear);
            tween.timeScale = speedFactor;

        }

        public void Bounce () {

            if (isAtEnd) {

                CheckForParticleEffect();

            } else {

                Vector2 moveToPosition = GetMoveToPosition();
                moveToPosition.y += 1.5f;

                tweener = transform.DOJump(moveToPosition, 5, 1, 1).OnComplete(Bounce).SetEase(easeType);
                tweener.timeScale = speedFactor;

            }

        }

        private Vector2 GetMoveToPosition () {

            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + new Vector2(-3, 10), -Vector2.up, 20, hitMask.value);
            if (hit.collider != null) {

                if(hit.collider.tag == "ProjectileEndTrigger") {

                    isAtEnd = true;

                }

                return hit.transform.position;

            }
				
            CheckForParticleEffect();

            return new Vector2(0, 0);

        }

        public void HitByPlayer () {

            CheckForParticleEffect();

        }

        public void Unload () {

            transform.DOKill();
            tweener.Kill();
            this.DOKill();
            gameObject.SetActive(false);
            manager.RemoveFromList(this);
            GetComponent<ObjectPoolReturnReference>().ReturnToPool();

        }
		public void CheckForParticleEffect () {

            //Activate particle if possible
            if (instantiatedParticle != null) {

                instantiatedParticle.transform.position = transform.position;
                instantiatedParticle.Play();

            }

            Unload();

        }

    }

}