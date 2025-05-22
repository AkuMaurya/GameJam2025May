using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;          // Assign the Player transform in the Inspector
    public Vector3 offset = new Vector3(0, 5, -7);  // Offset from the player
    public float smoothSpeed = 5f;    // Camera follow speed
    public bool lookAtPlayer = true;  // Enable camera rotation

    void LateUpdate()
    {
        if (target == null) return;

        // Smooth follow
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Optional: rotate to always look at the player
        if (lookAtPlayer)
        {
            Vector3 lookDirection = target.position - transform.position;
            Quaternion desiredRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed * Time.deltaTime);
        }
    }
}
