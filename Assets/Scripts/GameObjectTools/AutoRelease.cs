using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Releases (disables) enabled game object after a duration. If duration is zero tries to detect lenght of audio or animation clip on awake.
/// </summary>
public class AutoRelease : MonoBehaviour
{
    [Tooltip("Keep value zero to let this script to detect animation lenght")]
    public float duration;

    void Awake()
    {
        if (!(duration > 0))
        {
            Animator animator = GetComponent<Animator>();
            var clipInfo = animator != null ? animator.GetCurrentAnimatorClipInfo(0) : null;
            float animLen = clipInfo != null && clipInfo.Length > 0 ? clipInfo[0].clip.length : 0;

            AudioSource audioSource = GetComponent<AudioSource>();
            float audioLen = audioSource != null ? audioSource.clip.length : 0;

            duration = Mathf.Max(audioLen, animLen);

            if (!(duration > 0))
                duration = 1;
        }
    }

    void OnEnable()
    {
        StartCoroutine(Release());
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}