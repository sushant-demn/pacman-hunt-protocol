using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 0f, -10f); // -10 on Z is required for 2D cameras!

    public Camera cam;

    [Header("Camera Bounds")]
    public bool useBounds = true; // A toggle just in case you want to turn it off later
    public Vector2 minCameraPos;  // The bottom-left limit
    public Vector2 maxCameraPos;  // The top-right limit

    // We use LateUpdate for cameras so it moves AFTER the player has finished moving this frame
    void LateUpdate()
    {
        if (target == null) return;

        // Where the camera wants to be
        Vector3 desiredPosition = target.position + offset;

        if (useBounds)
        {
            // Mathf.Clamp forces a number to stay between a minimum and maximum value
            float clampedX = Mathf.Clamp(desiredPosition.x, minCameraPos.x, maxCameraPos.x);
            float clampedY = Mathf.Clamp(desiredPosition.y, minCameraPos.y, maxCameraPos.y);

            // Rebuild the desired position with our newly clamped numbers
            desiredPosition = new Vector3(clampedX, clampedY, desiredPosition.z);
        }

        // Smoothly glide towards that position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    public void setCameraFullSize()
    {
        cam.orthographicSize = 16;
    }
}