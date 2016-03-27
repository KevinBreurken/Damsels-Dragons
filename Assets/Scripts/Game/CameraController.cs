using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Base.Game {

    public class CameraController : MonoBehaviour {
        
        public delegate void CameraEvent ();

        /// <summary>
        /// Called when the Camera is scrolling.
        /// </summary>
        public event CameraEvent onCameraScrolled;

        [Header("Target")]
        public bool followTarget;
        public Transform target;

        [Header("Positioning")]
        public Vector3 offset;
        public float cameraFollowDistance;

        [HideInInspector]
        public Camera gameViewCamera;

        private GameObject cameraLookPoint;
        private float jumpOffset;
        private float verticalPostion;
        private Vector3 velocity = Vector3.zero;

        // Use this for initialization
        void Awake () {

            gameViewCamera = GetComponent<Camera>();
            cameraLookPoint = new GameObject();
            cameraLookPoint.hideFlags = HideFlags.HideInHierarchy;
            verticalPostion = transform.position.y;

        }

        /// <summary>
        /// Refocuses the target.
        /// </summary>
        public void RefocusTarget () {

            cameraLookPoint.transform.position = gameViewCamera.ViewportToWorldPoint(new Vector3(0.25f, 0.5f, 0));
            followTarget = true;

        }

        // Movement is done in fixedUpdate to prevent stuttering.
        void FixedUpdate () {

            if (followTarget) {

                Vector3 point = gameViewCamera.WorldToViewportPoint(cameraLookPoint.transform.position);
                Vector3 delta = cameraLookPoint.transform.position - gameViewCamera.ViewportToWorldPoint(new Vector3(0.25f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
                Vector3 destination = transform.position + delta + offset;

                if(target.transform.position.y > 4) {
                    if(jumpOffset < 2)
                    jumpOffset += 0.1f;
                } else {
                    if(jumpOffset > 0)
                    jumpOffset -= 0.1f;
                }

                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, 0.15f);
                transform.position = new Vector3(transform.position.x, offset.y + jumpOffset, 0);

                if (target.transform.position.x > cameraLookPoint.transform.position.x) {

                    cameraLookPoint.transform.position = new Vector3(target.transform.position.x, target.transform.position.y);

                }

            }

            if (onCameraScrolled != null) {

                onCameraScrolled();

            }

        }

        /// <summary>
        /// Places the camera at the maps starting position.
        /// </summary>
        public void SetAtStartPosition () {

            transform.position = new Vector2(-3.201591f, 0.65f);

            Vector3 point = gameViewCamera.WorldToViewportPoint(cameraLookPoint.transform.position);
            Vector3 delta = cameraLookPoint.transform.position - gameViewCamera.ViewportToWorldPoint(new Vector3(0.25f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta + offset;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, 0.15f);
            transform.position = new Vector3(transform.position.x, offset.y, 0);

            cameraLookPoint.transform.position = new Vector3(target.transform.position.x, target.transform.position.y);

        }

        /// <summary>
        /// Locks the camera to a given chunk.
        /// </summary>
        /// <param name="_chunk">The chunk that will be focused.</param>
		public void FixateChunk (ChunkData _chunk){

            Vector3 point = gameViewCamera.WorldToViewportPoint(_chunk.transform.position);
            Vector3 delta = _chunk.transform.position - gameViewCamera.ViewportToWorldPoint(new Vector3(0.25f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position;

            transform.DOMoveX(_chunk.transform.position.x + 25, 2);
            transform.DOMoveY(verticalPostion + offset.y, 2);

        }

    }

}