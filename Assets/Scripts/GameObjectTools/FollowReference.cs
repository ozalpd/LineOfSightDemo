using UnityEngine;

/// <summary>
/// Follows the reference transform
/// </summary>
public class FollowReference : MonoBehaviour
{
    public Transform reference;
    [Tooltip("Relative position to the reference.")]
    public Vector3 offset = Vector3.zero;

    [Tooltip("Wait duration after Awake event.")]
    public float latency = 0.5f;
    protected float followTime = 10;

    [Range(0.1f, 20f)]
    public float speed = 5f;
    [Tooltip("Speed is related to distance to the reference. Becomes faster when distance increases. Should be true to soften camera moves.")]
    public bool relevantSpeed = true;
    [Tooltip("Ignore Y axis of the reference position. Should be true for camera.")]
    public bool ignoreY = true;

    [Tooltip("Angular rotation speed. If this value is zero, this parameter has no effect")]
    public float rotationSpeed;

    public bool useLateUpdate = true;

    private float distance;
    private float deltaSpeed;
    private Vector3 newPosition;

    protected virtual void Awake()
    {
        if (reference == null)
        {
            Debug.LogError("No reference transform found. FollowReference needs a reference transform!");
        }

        followTime = Time.time + latency;
    }

    protected virtual void Update()
    {
        if (!useLateUpdate)
            UpdateTransform();
    }

    protected virtual void LateUpdate()
    {
        if (useLateUpdate)
            UpdateTransform();
    }

    protected virtual void UpdateTransform()
    {
        if (rotationSpeed > 0)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, reference.rotation, rotationSpeed * Time.deltaTime);

        newPosition = ignoreY
                    ? new Vector3(reference.position.x, 0, reference.position.z) + offset
                    : reference.position + offset;
        distance = transform.position.DistanceTo(newPosition);

        if (!Mathf.Approximately(distance, 0))
        {
            deltaSpeed = relevantSpeed
                       ? distance * speed * Time.deltaTime
                       : speed * Time.deltaTime;
            transform.position = transform.position.MoveTowards(newPosition, deltaSpeed);
        }
    }
}
