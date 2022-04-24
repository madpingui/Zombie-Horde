using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if (target)
        {
            Vector3 goalPos = target.position;
            goalPos.y = transform.position.y;
            goalPos.z = target.position.z - 8;
            transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);
        }
    }
}
