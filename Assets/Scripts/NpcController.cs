using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
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
    [Range(1f, 30f)]
    public float nearSightDist = 8f;
    [Tooltip("Near Sight Angle. This is maximum sight angle if distance to target will be zero.")]
    [Range(5f, 360f)]
    public float nearSightAngle = 180f;

    public NpcState State { get; private set; }

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        State = NpcState.Idle;
    }

    private void Update()
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
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(direction),
                                                  rotationSpeed * Time.deltaTime);

            if (direction.magnitude > shootDist)
            {
                if (State != NpcState.Chasing)
                {
                    anim.SetTrigger("isRunning");
                    State = NpcState.Chasing;
                }
            }
            else
            {
                if (State != NpcState.Attacking)
                {
                    anim.SetTrigger("isShooting");
                    State = NpcState.Attacking;
                }
            }
        }
        else
        {
            if (State != NpcState.Idle)
            {
                anim.SetTrigger("isIdle");
                State = NpcState.Idle;
            }
        }

        if (State == NpcState.Chasing)
            transform.Translate(0, 0, Time.deltaTime * speed);
    }

    private Vector3 direction;
    private float sightAngle;
    private float angle;
}
