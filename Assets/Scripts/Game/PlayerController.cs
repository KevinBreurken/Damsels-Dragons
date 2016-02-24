using UnityEngine;
using System.Collections;
using Base.Control;
using Base.Control.Method;

namespace Base.Game {
	
	public class PlayerController : MonoBehaviour {

		public float movementSpeedFactor = 1.5f;
        public float jumpStrength;
        public LayerMask floorCollisionMask;
        public float maxSpeed;

        private Rigidbody2D rigidBody;
        private BaseInputMethod inputMethod;
        private bool isGrounded;

		void Awake () {

            rigidBody = GetComponent<Rigidbody2D>();

		}

        void Start () {

            InputManager.Instance.onInputChanged += OnInputMethodChanged;
            OnInputMethodChanged(InputManager.Instance.GetCurrentInputMethod());

        }
        
		void FixedUpdate () {

           //Apply horizontal movement.
           //rigidBody.AddForce(((Vector2.right * inputMethod.GetMovementInput()) *movementSpeedFactor));
          

        }

        void Update () {

            
			Vector3 vel = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
			rigidBody.velocity = new Vector2(vel.x + inputMethod.GetMovementInput(),rigidBody.velocity.y);

			//Detect if character is grounded.
			isGrounded = false;
			for (int i = 0; i < 3; i++) {
				
				CastToGround(new Vector3(-0.4f + (0.4f * i),0,0));

			}

			//Add slowdown.
			rigidBody.velocity = new Vector2(rigidBody.velocity.x * 0.9f,rigidBody.velocity.y);

        }

		private void CastToGround (Vector3 _positionOffset) {


			Debug.DrawRay(transform.position + _positionOffset, -Vector2.up);
			RaycastHit2D hit = Physics2D.Raycast(transform.position + _positionOffset, -Vector2.up,1, floorCollisionMask.value);
			if (hit.collider != null) {

				isGrounded = true;

			}

		}

        public void SetAtStartPosition () {

            transform.position = new Vector2(-8, -3);

        }

        public void OnJumpPressed () {

            if (isGrounded) {

                rigidBody.AddForce((Vector2.up * jumpStrength), ForceMode2D.Impulse);

            }
            Debug.Log("Jump Pressed");

        }

		/// <summary>
		/// Called when the input method is changed.
		/// </summary>
		public void OnInputMethodChanged (BaseInputMethod _newMethod) {

            //Remove the event listener if there was any.
            if(inputMethod != null) {

                inputMethod.onJumpPressed -= OnJumpPressed;

            }

			inputMethod = _newMethod;
            inputMethod.onJumpPressed += OnJumpPressed;

            Debug.Log("[Input] Input method is changed to : " + _newMethod.GetType().ToString() + "On The Player Controller");

		}

	}

}