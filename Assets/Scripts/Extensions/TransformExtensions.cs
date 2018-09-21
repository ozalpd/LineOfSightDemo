using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    /// <summary>
    /// Direction to another position or vector.
    /// </summary>
    /// <returns>The direction.</returns>
    /// <param name="thisPosition">This position.</param>
    /// <param name="destination">Other position or vector.</param>
    /// <param name="ignoreY">If set to <c>true</c> ignores y part of the vectors.</param>
    public static Vector3 Direction(this Transform transform, Vector3 destination, bool ignoreY = false)
    {
        return transform.position.Direction(destination, ignoreY);
    }

    /// <summary>
    /// Distance from the position to another position.
    /// </summary>
    /// <returns>The distance between the position to another vector</returns>
    /// <param name="thisPosition">This position</param>
    /// <param name="destination">Other position</param>
    /// <param name="ignoreY">If set to <c>true</c> ignores y part of the positins.</param>
    public static float DistanceTo(this Transform transform, Vector3 destination, bool ignoreY = false)
    {
        return transform.position.DistanceTo(destination, ignoreY);
    }

    public static float Dot(this Transform transform, Vector3 other)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = other - transform.position;

        return Vector3.Dot(forward, toOther);
    }

    public static bool IsBehind(this Transform transform, Vector3 other)
    {
        return transform.Dot(other) < 0;
    }

    /// <summary>
    /// Moves a transform current in a straight line towards a target point.
    /// </summary>
    public static Vector3 MoveTowards(this Transform transform, Vector3 target, float maxDistanceDelta)
    {
        return Vector3.MoveTowards(transform.position, target, maxDistanceDelta);
    }

}