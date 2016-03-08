using UnityEngine;
using System.Collections;
using Base.Control;
using Base.Control.Method;
using Base.Game.State;
using Base.Score;
using DG.Tweening;

namespace Base.Game {

    public class PlayerController : MonoBehaviour {

        public float movementSpeedFactor = 1.5f;
        public float jumpStrength;
        public LayerMask floorCollisionMask;
        public float maxSpeed;

        private Rigidbody2D rigidBody;
        private BaseInputMethod inputMethod;
        private bool isGrounded;
        private CameraController playerCamera;
        private InGameState gameState;
        private bool canDoubleJump;
        private bool isControlledByPlayer;
        private Animator animator;

        void Awake () {

            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();

        }

        void Start () {

            InputManager.Instance.onInputChanged += OnInputMethodChanged;
            OnInputMethodChanged(InputManager.Instance.GetCurrentInputMethod());

        }

        void Update () {

            Vector3 vel = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
            if (isControlledByPlayer) {

                if(inputMethod.GetMovementInput() != 0) {

                    rigidBody.velocity = new Vector2(vel.x + inputMethod.GetMovementInput(), rigidBody.velocity.y);
                    animator.SetBool("IsMoving", true);

                } else {

                    animator.SetBool("IsMoving", false);

                }
               
            }
            
            //Detect if character is grounded.
            isGrounded = false;
            for (int i = 0; i < 3; i++) {

                CastToGround(new Vector3(-0.4f + (0.4f * i), 0, 0));

            }
            animator.SetBool("IsGrounded", isGrounded);
            //Add slowdown.
            rigidBody.velocity = new Vector2(rigidBody.velocity.x * 0.9f, rigidBody.velocity.y);

        }

        private void CastToGround (Vector3 _positionOffset) {

            Debug.DrawRay(transform.position + _positionOffset, -Vector2.up);
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
            playerCamera.ResumeFollowingTarget();

        }

        public void Die () {

            Time.timeScale = 0;
            transform.DOJump(transform.position + new Vector3(-1, -9, 0), 5, 1, 2).SetUpdate(true).OnComplete(gameState.LeaveGame);

        }

        //--------------Jumping-------------------------------

        public void OnJumpPressed () {

            if (isControlledByPlayer) {

                if (isGrounded) {

                    transform.DOMoveY(transform.position.y + 3, 0.25f).OnComplete(OnNormalJumpComplete);
                    animator.SetTrigger("IsJumping");

                } else {

                    if (canDoubleJump) {

                        transform.DOMoveY(transform.position.y + 3, 0.25f).OnComplete(OnNormalJumpComplete);
                        canDoubleJump = false;
                        animator.SetTrigger("IsJumping");

                    }

                }

            }

        }

        private void OnJumpedOnRock () {

            transform.DOMoveY(transform.position.y + 5, 0.35f).OnComplete(OnRockJumpComplete);
            canDoubleJump = true;
            ScoreManager.Instance.AddScore(100);

        }

        private void OnNormalJumpComplete () {

            rigidBody.AddForce(new Vector2(0, -100));
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);

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