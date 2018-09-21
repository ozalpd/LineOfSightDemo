using UnityEngine;
/// <summary>
/// Saves a transfom's position and rotation into local variables, then restores at any time method RestoreTransform called.
/// </summary>
public class SaveTransform : MonoBehaviour
{
    [Tooltip("If this is checked, saves transfom's position and rotation in the Start method.")]
    public bool autoSave = true;
    private Vector3 _position;
    private Quaternion _rotation;

    /// <summary>
    /// Is here any saved data.
    /// </summary>
    public bool IsSaved { get { return _isSaved; } }

    private bool _isSaved;

    /// <summary>
    /// Saves a transfom's position and rotation
    /// </summary>
    public void Save()
    {
        _position = transform.position;
        _rotation = transform.rotation;
        _isSaved = true;
    }

    /// <summary>
    /// Restores transfom's position and rotation from last saved values.
    /// </summary>
    public void RestoreTransform()
    {
        transform.position = _position;
        transform.rotation = _rotation;
    }

    private void Start()
    {
        if (autoSave)
            Save();
    }
}