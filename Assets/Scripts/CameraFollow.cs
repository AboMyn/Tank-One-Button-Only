using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;              // Tank
    public Vector3 offset = new Vector3(0, 8, -10);
    public float smoothSpeed = 8f;

    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.15f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        if (shakeDuration > 0)
        {
            smoothPos += Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime;
        }

        transform.position = smoothPos;
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}