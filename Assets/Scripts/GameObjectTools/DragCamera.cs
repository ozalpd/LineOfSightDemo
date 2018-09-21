using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour
{
    [Tooltip("Camera that to be dragged by game object. Script will try to set Main Camera tagged one, if it is not set.")]
    public Camera draggedCam;
    private Vector3 offset;

    private void Awake()
    {
        if (draggedCam == null)
            draggedCam = Camera.main;

        if (draggedCam == null)
        {
            Debug.LogError("No main camera found!");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        offset = draggedCam.transform.position - transform.position;
    }

    private void LateUpdate()
    {
        draggedCam.transform.position = transform.position + offset;
    }
}
