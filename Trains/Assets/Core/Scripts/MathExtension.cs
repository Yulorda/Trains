using UnityEngine;

public static class MathExtension
{
    public static Vector2 GetPointOnLine(Vector3 lineA, Vector3 lineB, Vector3 point, out float pointOnLine)
    {
        var line = lineB - lineA;
        Vector3 pointToStart = point - lineA;
        pointOnLine = GetPointOnLine(line, pointToStart);
        return lineA + line * pointOnLine;
    }

    /// <summary>
    /// [0,1]
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public static float GetPointOnLine(Vector3 line, Vector3 pointToStart)
    {
        return Mathf.Clamp(Vector3.Dot(pointToStart, line.normalized) / line.magnitude, 0, 1);
    }


    /// <summary>
    /// [0,1]
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public static float GetPointOnLine(Vector3 lineA, Vector3 lineB, Vector3 point)
    {
        var line = lineB - lineA;
        Vector3 pointToStart = point - lineA;
        return Mathf.Clamp(Vector3.Dot(pointToStart, line.normalized) / line.magnitude, 0, 1);
    }

    public static Vector3 GetPointOnLine(Vector3 lineA, Vector3 lineB, float pointOnLine)
    {
        return lineA + (lineB - lineA) * pointOnLine;
    }
}