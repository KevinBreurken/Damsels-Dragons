using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public bool followTarget;
    public Transform target;
    public Camera gameViewCamera;

    private Vector3 velocity = Vector3.zero;
    public float dampTime = 0.15f;
    public Vector3 offset;
    public float cameraFollowDistance;
    private GameObject cameraLookPoint;

    private float verticalPostion;

    // Use this for initialization
    void Awake () {

        gameViewCamera = GetComponent<Camera>();
        cameraLookPoint = new GameObject();
        verticalPostion = transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {

        if (followTarget) {

            Vector3 point = gameViewCamera.WorldToViewportPoint(cameraLookPoint.transform.position);
            Vector3 delta = cameraLookPoint.transform.position - gameViewCamera.ViewportToWorldPoint(new Vector3(0.25f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta + offset;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            transform.position = new Vector3(transform.position.x, offset.y, 0);


            if (target.transform.position.x > cameraLookPoint.transform.position.x) {

                cameraLookPoint.transform.position = new Vector3(target.transform.position.x, target.transform.position.y);

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

}
