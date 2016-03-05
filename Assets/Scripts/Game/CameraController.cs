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

        public bool followTarget;
        public Transform target;
        public Camera gameViewCamera;

        private Vector3 velocity = Vector3.zero;
        public float dampTime = 0.15f;
        public Vector3 offset;
        private float jumpOffset;
        public float cameraFollowDistance;
        private GameObject cameraLookPoint;

        private float verticalPostion;

        // Use this for initialization
        void Awake () {

            gameViewCamera = GetComponent<Camera>();
            cameraLookPoint = new GameObject();
            cameraLookPoint.hideFlags = HideFlags.HideInHierarchy;
            verticalPostion = transform.position.y;

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

                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
                transform.position = new Vector3(transform.position.x, offset.y + jumpOffset, 0);

                if (target.transform.position.x > cameraLookPoint.transform.position.x) {

                    cameraLookPoint.transform.position = new Vector3(target.transform.position.x, target.transform.position.y);

                }

                if (onCameraScrolled != null) {

                    onCameraScrolled();

                }

            }

        }

        public void SetAtStartPosition () {

            transform.position = new Vector2(0, 0.65f);

            Vector3 point = gameViewCamera.WorldToViewportPoint(cameraLookPoint.transform.position);
            Vector3 delta = cameraLookPoint.transform.position - gameViewCamera.ViewportToWorldPoint(new Vector3(0.25f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta + offset;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            transform.position = new Vector3(transform.position.x, offset.y, 0);

            cameraLookPoint.transform.position = new Vector3(target.transform.position.x, target.transform.position.y);

        }

		public void FixateChunk (ChunkData _chunk){

            Vector3 point = gameViewCamera.WorldToViewportPoint(_chunk.transform.position);
            Vector3 delta = _chunk.transform.position - gameViewCamera.ViewportToWorldPoint(new Vector3(0.25f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position;

            transform.DOMoveX(_chunk.transform.position.x + 11, 2);

        }

    }

}