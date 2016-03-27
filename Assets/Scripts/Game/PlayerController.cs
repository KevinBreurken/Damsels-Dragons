using UnityEngine;
using System.Collections;
using Base.Control;
using Base.Control.Method;
using Base.Game.State;
using Base.Score;
using DG.Tweening;
using Base.Audio;

namespace Base.Game {

    /// <summary>
    /// Controls the playable character.
    /// </summary>
    public class PlayerController : MonoBehaviour {
   
        //----------Graphics Related-------------------
        [Header("Graphics")]
        public SpriteRenderer characterSprite;
        private Animator animator;

        //----------Movement Related-------------------
        [Header("Movement")]
        public bool isControlledByPlayer;
        public float movementSpeedFactor = 1.5f;

        private bool canDoubleJump;
        private bool hasJumpKeyReleased;
        private bool previousGrounded;
        private bool isGrounded;

        //----------Audio Related-------------------
        [Header("Audio")]
        public AudioObjectHolder jumpAudioObject;
        public AudioObjectHolder doubleJumpAudioObject;
        public AudioObjectHolder jumpRockAudioObject;
        public AudioObjectHolder hitGroundAudioObject;
        public AudioObjectHolder pickupCointAudioObject;
        public AudioObjectHolder playerDeadAudioObject;

        //----------Other Related-------------------
        [Header("Other")]
        public LayerMask floorCollisionMask;

        private Rigidbody2D rigidBody;
        private BaseInputMethod inputMethod;
        private CameraController playerCamera;
        private InGameState gameState;

        void Awake () {

            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            jumpAudioObject.CreateAudioObject();
            doubleJumpAudioObject.CreateAudioObject();
            hitGroundAudioObject.CreateAudioObject();
            jumpRockAudioObject.CreateAudioObject();
            pickupCointAudioObject.CreateAudioObject();
            playerDeadAudioObject.CreateAudioObject();

        }

        void Start () {

            InputManager.Instance.onInputChanged += OnInputMethodChanged;
            OnInputMethodChanged(InputManager.Instance.GetCurrentInputMethod());

        }

		void FixedUpdate () {
			
			if(hasJumpKeyReleased){
				
				rigidBody.AddForce(new Vector2(0, -10));

			}

		}

        void Update () {

            if (isControlledByPlayer) {

                if (previousGrounded != isGrounded) {

                    if (isGrounded) {

                        hitGroundAudioObject.GetAudioObject().Play();
                
                    }

                    previousGrounded = isGrounded;

                }

                hasJumpKeyReleased = !inputMethod.GetDownInput();

                float movementInput = inputMethod.GetMovementInput();
                if (movementInput != 0) {

					rigidBody.velocity = new Vector2(movementInput * movementSpeedFactor, rigidBody.velocity.y);
                    animator.SetBool("IsMoving", true);

                    //Flip the sprite.
                    if(movementInput < 0) { characterSprite.flipX = true;
                    } else if (movementInput > 0) { characterSprite.flipX = false; }
                    //---

                } else {

                    animator.SetBool("IsMoving", false);

                }
               
            }
            
            //Detect if character is grounded.
            isGrounded = false;
            for (int i = 0; i < 3; i++) { CastToGround(new Vector3(-0.4f + (0.4f * i), 0, 0)); }

            animator.SetBool("IsGrounded", isGrounded);

            //Add slowdown.
            rigidBody.velocity = new Vector2(rigidBody.velocity.x * 0.9f, rigidBody.velocity.y);

        }

        private void CastToGround (Vector3 _positionOffset) {

            RaycastHit2D hit = Physics2D.Raycast(transform.position + _positionOffset, -Vector2.up, 1, floorCollisionMask.value);
            if (hit.collider != null) {

                isGrounded = true;
                canDoubleJump = true;

            }

        }


        public void SetAtStartPosition () {

            isControlledByPlayer = true;
            transform.position = new Vector2(-8, -3);

        }

        public void SetCameraReference (CameraController _camera) {

            playerCamera = _camera;

        }

        public void SetStateReference (InGameState _gameState) {

            gameState = _gameState;

        }

        /// <summary>
        /// Called when the input method is changed.
        /// </summary>
        public void OnInputMethodChanged (BaseInputMethod _newMethod) {

            //Remove the event listener if there was any.
            if (inputMethod != null) {

                inputMethod.onJumpPressed -= OnJumpPressed;

            }

            inputMethod = _newMethod;
            inputMethod.onJumpPressed += OnJumpPressed;

            Debug.Log("[Input] Input method is changed to : " + _newMethod.GetType().ToString() + "On The Player Controller");

        }

        public void OnLevelComplete () {

            isControlledByPlayer = false;
            transform.DOJump(playerCamera.gameViewCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.13f, 0.5f)), 8, 1, 2);
            StartCoroutine(DelayAfterLevelComplete());

        }

        private IEnumerator DelayAfterLevelComplete () {

            yield return new WaitForSeconds(3);
            isControlledByPlayer = true;
            playerCamera.RefocusTarget();

        }

        public void Die () {

            Time.timeScale = 0;
			isControlledByPlayer = false;
            transform.DOJump(transform.position + new Vector3(-1, -9, 0), 5, 1, 2).SetUpdate(true).OnComplete(gameState.LeaveGame);
            playerDeadAudioObject.GetAudioObject().Play();

        }

        //--------------Jumping-------------------------------

        public void OnJumpPressed () {

            if (isControlledByPlayer) {

                if (isGrounded) {

					rigidBody.velocity = Vector2.zero;
					rigidBody.AddForce(new Vector2(0, 500));
                    animator.SetTrigger("IsJumping");
                    jumpAudioObject.GetAudioObject().Play();

                } else {

                    if (canDoubleJump) {

						rigidBody.velocity = Vector2.zero;
						rigidBody.AddForce(new Vector2(0, 500));
                        canDoubleJump = false;
                        animator.SetTrigger("IsJumping");
                        doubleJumpAudioObject.GetAudioObject().Play();

                    }

                }

            }

        }

        private void OnJumpedOnRock () {

            transform.DOMoveY(transform.position.y + 5, 0.35f).OnComplete(OnRockJumpComplete);
            canDoubleJump = true;
            ScoreManager.Instance.AddScore(15);
            jumpRockAudioObject.GetAudioObject().Play();

        }

        private void OnNormalJumpComplete () {

            //rigidBody.AddForce(new Vector2(0, -100));
            //rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);

        }

        private void OnRockJumpComplete () {

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);

        }

        //-------------end Jumping-----------------------------


        void OnTriggerEnter2D (Collider2D _col) {

            if (isControlledByPlayer) {

                if (_col.gameObject.layer == LayerMask.NameToLayer("Triggers")) {

                    if (_col.tag == "EndLevelTrigger") {

                        EndLevelChunk endChunk = _col.transform.parent.GetComponent<EndLevelChunk>();

                        if (!endChunk.isFinished) {

                            playerCamera.followTarget = false;
                            playerCamera.FixateChunk(endChunk);

                        }

                    }

                    if (_col.tag == "DragonTrigger") {

                        _col.GetComponent<EndLevelTrigger>().SendEndMessage();


                    }

                }

				if (_col.gameObject.layer == LayerMask.NameToLayer("Pickup")) {

					if (_col.gameObject.tag == "Coin") {

                        _col.gameObject.GetComponent<CoinPickup>().Pickup();
                        pickupCointAudioObject.GetAudioObject().Play();

					}

				}


                if (_col.gameObject.layer == LayerMask.NameToLayer("Moving")) {

                    if (_col.gameObject.tag == "FireProjectile") {

                        Die();

                    }

                    if (_col.gameObject.tag == "StoneProjectile") {

                        //Compare height
                        float playerheight = transform.position.y;
                        float projectileheight = _col.gameObject.transform.position.y;

                        if (projectileheight <= playerheight) {

                            //Jumped on rock.
                            _col.GetComponent<Collider2D>().enabled = false;
                            _col.GetComponent<ProjectileMovement>().HitByPlayer();
                            OnJumpedOnRock();

                        } else {

                            //Hit by rock.
                            Die();

                        }

                    }

                }

            }

        }

    }

}