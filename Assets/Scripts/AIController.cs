using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    public Transform target;

    public float rotationSpeed = 2f;
    public float speed = 2f;
    public float shootDist = 5f;

    [Header("Sight Settings of AI")]
    [Tooltip("Visible Distance")]
    [Range(5f, 100f)]
    public float visibleDist = 20f;
    [Tooltip("Far Sight Angle")]
    [Range(5f, 360f)]
    public float farSightAngle = 60f;

    [Tooltip("Near Sight begining distance. When target gets closer than this distance sight angle increases.")]
    [Range(1f, 10f)]
    public float nearSightDist = 8f;
    [Tooltip("Near Sight Angle. This is maximum sight angle if distance to target will be zero.")]
    [Range(5f, 360f)]
    public float nearSightAngle = 180f;

    string state = "IDLE"; //TODO:Make this an enum

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        direction = target.position.With(y: transform.position.y) - transform.position;
        angle = Vector3.Angle(direction, transform.forward);

        if (direction.magnitude < 0.1)
        {
            sightAngle = nearSightAngle * 0.5f;
        }
        else if (direction.magnitude < nearSightDist)
        {
            sightAngle = Mathf.Lerp(nearSightAngle, farSightAngle, direction.magnitude / nearSightDist) * 0.5f;
        }
        else
        {
            sightAngle = farSightAngle * 0.5f;
        }

        if (direction.magnitude < visibleDist && angle < sightAngle)
        {
            //direction.y = transform.position.y;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(direction),
                                                  rotationSpeed * Time.deltaTime);

            if (direction.magnitude > shootDist)
            {
                if (state != "RUNNING")
                    anim.SetTrigger("isRunning");
                state = "RUNNING";
            }
            else
            {
                if (state != "SHOOTING")
                    anim.SetTrigger("isShooting");
                state = "SHOOTING";
            }
        }
        else
        {
            if (state != "IDLE")
                anim.SetTrigger("isIdle");
            state = "IDLE";
        }

        if (state == "RUNNING")
            transform.Translate(0, 0, Time.deltaTime * speed);
    }
    Vector3 direction;
    float sightAngle;
    float angle;
}
