using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime = 0.1f;

    private Vector3 currentVelocity;

    private void Awake()
    {
        AssignTarget();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            AssignTarget();
            if (target == null)
                return;
        }

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    private void AssignTarget()
    {
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
                offset = transform.position - target.position;
                Debug.Log("CameraFollow assigned Player target at runtime.");
            }
        }
    }
}
