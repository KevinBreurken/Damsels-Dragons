using UnityEngine;
using System.Collections;
using DG.Tweening;
using Base.Management;

namespace Base.Game {

    /// <summary>
    /// Handles projectile movement.
    /// </summary>
    public class ProjectileMovement : MonoBehaviour {

        /// <summary>
        /// The layer(s) the projectile bounces on.
        /// </summary>
        public LayerMask hitMask;

        /// <summary>
        /// The easing type used in the tween movement.
        /// </summary>
        public Ease easeType;

        /// <summary>
        /// The prefab for the used particle effect.
        /// </summary>
        public GameObject particlePrefab;
        private ParticleSystem instantiatedParticle;


        private float speedFactor;
        /// <summary>
        /// The factor the projectile moves with.
        /// Used for pre-baking movement.
        /// </summary>
		public float SpeedFactor {

			get {

				return speedFactor;

			}

			set {

				speedFactor = value;

				if(tweemSequence != null) {

					tweemSequence.timeScale = speedFactor;

				}

			}

		}

        /// <summary>
        /// Reference to the manager this projectile uses.
        /// </summary>
		public ProjectileManager manager;

        /// <summary>
        /// the tween sequence used for movement..
        /// </summary>
		public Sequence tweemSequence;

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

        /// <summary>
        /// Spawns and fires the projectile.
        /// </summary>
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

        /// <summary>
        /// Bounces the projectile to its next position.
        /// </summary>
        public void Bounce () {

            if (!gameObject.activeSelf)
                return;

            if (isAtEnd) {

                CheckForParticleEffect();

            } else {

                Vector2 moveToPosition = GetMoveToPosition();
                moveToPosition.y += 1.5f;

                tweemSequence = transform.DOJump(moveToPosition, 5, 1, 1).OnComplete(Bounce).SetEase(easeType);
                tweemSequence.timeScale = speedFactor;

            }

        }

        private Vector2 GetMoveToPosition () {
            
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + new Vector2(-4, 10), -Vector2.up, 20, hitMask.value);
            if (hit.collider != null) {

                if(hit.collider.tag == "ProjectileEndTrigger") {

                    isAtEnd = true;

                }

                return hit.transform.position;

            }
				
            CheckForParticleEffect();
            isAtEnd = true;
            return new Vector2(0, 0);

        }

        /// <summary>
        /// Called when the player jumped on this projectile.
        /// </summary>
        public void HitByPlayer () {

            CheckForParticleEffect();

        }

        /// <summary>
        /// Called when the game is loaded.
        /// </summary>
        public void OnGameLoad () {

            if (instantiatedParticle != null) {

                instantiatedParticle.transform.position = new Vector3(0,-1000,0);
                instantiatedParticle.Stop();

            }

        }

        /// <summary>
        /// Called when the game switches out of the game state.
        /// </summary>
        public void Unload () {

            gameObject.SetActive(false);
            manager.RemoveFromList(this);
            GetComponent<ObjectPoolReturnReference>().ReturnToPool();

            transform.position = new Vector3(-100, -100, 0);
           
            transform.DOKill();
            tweemSequence.Kill();
            this.DOKill();

        }

        /// <summary>
        /// Checks if this projectile has a particle effect.
        /// </summary>
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