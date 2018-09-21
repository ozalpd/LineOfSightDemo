using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes a 2D sprite look as billboard by making its rotations same with camera.
/// </summary>
public class LookAsBillboard : MonoBehaviour
{

    [Tooltip("A reference to the main camera in the scene.")]
    public Transform mainCamera;

    [Tooltip("Camera rotation is never changed in run time.")]
    public bool fixedCameraRotation;

    public bool ignoreX;
    public bool ignoreY;
    public bool ignoreZ;

    [Range(0f, 1f)]
    public float moveToCameraSpeed = 0f;

    private bool rotate = true;
    private Vector3 nextRotation = Vector3.zero;
    private Vector3 currRotation = Vector3.zero;

    private void Awake()
    {
        if (mainCamera == null)
        {
            if (Camera.main != null)
            {
                mainCamera = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning("Warning: No main camera found. Object needs a Camera tagged \"MainCamera\".");
            }
        }
    }

    private void Update()
    {
        if (moveToCameraSpeed > 0)
            transform.position += (mainCamera.position - transform.position) * moveToCameraSpeed * Time.deltaTime;

        if (ignoreX && ignoreY && ignoreZ)
            return;

        if (rotate && (ignoreX || ignoreY || ignoreZ))
        {
            nextRotation = mainCamera.rotation.eulerAngles;
            currRotation = transform.rotation.eulerAngles;

            if (ignoreX)
                nextRotation.x = currRotation.x;
            if (ignoreY)
                nextRotation.y = currRotation.y;
            if (ignoreZ)
                nextRotation.z = currRotation.z;

            transform.rotation = Quaternion.Euler(nextRotation);
        }
        else if (rotate)
            transform.rotation = mainCamera.rotation;

        rotate = !fixedCameraRotation;
    }
}
