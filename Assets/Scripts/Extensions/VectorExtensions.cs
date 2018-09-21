using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    /// <summary>
    ///   <para>Returns the angle in degrees between from and to.</para>
    /// </summary>
    /// <param name="thisPosition">This position</param>
    /// <param name="to">The vector to which the angular difference is measured.</param>
    /// <returns>
    ///   <para>The angle in degrees between the two vectors.</para>
    /// </returns>
    public static float Angle(Vector3 thisPosition, Vector3 to)
    {
        return Vector3.Angle(thisPosition, to);
    }

    /// <summary>
    /// Returns a copy of vector with its magnitude clamped to maxLength.
    /// </summary>
    /// <param name="original"></param>
    /// <param name="maxLength"></param>
    public static Vector3 ClampMagnitude(this Vector3 original, float maxLength)
    {
        return Vector3.ClampMagnitude(original, maxLength);
    }

    /// <summary>
    /// Direction to another position or vector.
    /// </summary>
    /// <returns>The direction.</returns>
    /// <param name="thisPosition">This position.</param>
    /// <param name="destination">Other position or vector.</param>
    /// <param name="ignoreY">If set to <c>true</c> ignores y part of the vectors.</param>
    public static Vector3 Direction(this Vector3 thisPosition, Vector3 destination, bool ignoreY = false)
    {
        if (ignoreY)
        {
            return new Vector3(destination.x - thisPosition.x, 0, destination.z - thisPosition.z);
        }
        return destination - thisPosition;
    }

    /// <summary>
    /// Distance from the position to another position.
    /// </summary>
    /// <returns>The distance between the position to another vector</returns>
    /// <param name="thisPosition">This position</param>
    /// <param name="destination">Other position</param>
    /// <param name="ignoreY">If set to <c>true</c> ignores y part of the positins.</param>
    public static float DistanceTo(this Vector3 thisPosition, Vector3 destination, bool ignoreY = false)
    {
        return ignoreY
            ? Vector3.Distance(thisPosition, destination.With(y: thisPosition.y /*need to use same y*/))
            : Vector3.Distance(thisPosition, destination);
    }

    /// <summary>
    /// Flattens original Vector3 by setting its y to zero or given flat value
    /// </summary>
    /// <param name="original"></param>
    /// <param name="flatY">Flat value of y axis</param>
    /// <returns></returns>
    public static Vector3 Flat(this Vector3 original, float flatY = 0)
    {
        return original.With(y: flatY);
    }

    /// <summary>
    /// Linearly interpolates from this vector to other vector by t.
    /// </summary>
    /// <param name="thisVector"></param>
    /// <param name="other"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Vector3 Lerp(this Vector3 thisVector, Vector3 other, float t)
    {
        return Vector3.Lerp(thisVector, other, t);
    }

    /// <summary>
    /// Moves a point current in a straight line towards a target point.
    /// </summary>
    public static Vector3 MoveTowards(this Vector3 thisPosition, Vector3 target, float maxDistanceDelta)
    {
        return Vector3.MoveTowards(thisPosition, target, maxDistanceDelta);
    }

    /// <summary>
    /// Reurns a new Vector3 with a given any of x, y, z values
    /// </summary>
    /// <param name="original"></param>
    /// <param name="x">If this is null uses original x value</param>
    /// <param name="y">If this is null uses original y value</param>
    /// <param name="z">If this is null uses original z value</param>
    /// <returns></returns>
    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
    }
}
