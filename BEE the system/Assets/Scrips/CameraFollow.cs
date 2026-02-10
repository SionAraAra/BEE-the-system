using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    [Header("Follow")]
    public float smoothTime = 0.25f;
    private Vector3 velocity;

    [Header("Bounds")]
    public Vector2 minBounds; // bottom-left of level
    public Vector2 maxBounds; // top-right of level

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPos = target.position + offset;

        // smooth follow
        Vector3 smoothedPos = Vector3.SmoothDamp(
            transform.position,
            desiredPos,
            ref velocity,
            smoothTime
        );

        // camera half size (important!)
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // clamp so edges never cross bounds
        smoothedPos.x = Mathf.Clamp(
            smoothedPos.x,
            minBounds.x + camWidth,
            maxBounds.x - camWidth
        );

        smoothedPos.y = Mathf.Clamp(
            smoothedPos.y,
            minBounds.y + camHeight,
            maxBounds.y - camHeight
        );

        transform.position = new Vector3(
            smoothedPos.x,
            smoothedPos.y,
            offset.z
        );
    }
}