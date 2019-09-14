using UnityEngine;

/// <summary>
/// FM: Class designed to controll movement over simple path
/// With large variety of methods to help customize destiny for this logics
/// </summary>
//[System.Serializable]
public class FBasics_PathData
{
    // Static Cache Data
    public Vector3[] PathPoints { get; private set; }

    private float AllLengthLoop;
    private float AllLength;

    public float[] Distances { get; private set; }
    public float[] LoopedDistances { get; private set; }

    public float[] PointProgressLoop { get; private set; }
    public float[] PointProgress { get; private set; }

    public Vector3[] PointDirection { get; private set; }

    // Dynamic Variables
    public bool LoopPath = false;

    private float progress = 0f;

    private int currentPoint = 0;
    private int nextIndex = 0;
    private int previousIndex = 0;
    private float pointProgress = 0f;

    #region Static and generating methods

    /// <summary>
    /// Creating path data class responsible for all path motion
    /// Starting point for path motion, we calculating all needed data for optimal movement over the path made from points in world space
    /// </summary>
    public static FBasics_PathData GeneratePathCache(Vector3[] pathPoints, bool looped = false)
    {
        FBasics_PathData data = new FBasics_PathData
        {
            PathPoints = pathPoints
        };

        data.LoopPath = looped;

        float[] dists = new float[pathPoints.Length];
        float[] pointDistance = new float[pathPoints.Length + 1];
        Vector3[] dirs = new Vector3[pathPoints.Length];

        pointDistance[0] = 0f;

        // Calculating distances between path points and whole path distance
        for (int i = 0; i < pathPoints.Length; i++)
        {
            int a = i;
            int b = i + 1;
            if (b > pathPoints.Length - 1) b = 0;

            dists[i] = Vector3.Distance(pathPoints[a], pathPoints[b]);
            dirs[i] = (pathPoints[b] - pathPoints[a]).normalized;

            data.AllLengthLoop += dists[i];
            if (i != pathPoints.Length - 1) data.AllLength += dists[i];
            pointDistance[i + 1] = data.AllLengthLoop;
        }

        // Calculating progress of path in percentage measure
        float[] pointProgressLoop = new float[pointDistance.Length];
        for (int i = 0; i < pointProgressLoop.Length; i++)
        {
            pointProgressLoop[i] = Mathf.InverseLerp(0f, data.AllLengthLoop, pointDistance[i]);
        }

        float[] pointProgress = new float[pathPoints.Length];
        for (int i = 0; i < pointProgress.Length; i++)
        {
            pointProgress[i] = Mathf.InverseLerp(0f, data.AllLength, pointDistance[i]);
        }

        // Cache the data
        data.LoopedDistances = pointDistance;
        data.PointProgressLoop = pointProgressLoop;
        data.PointProgress = pointProgress;
        data.Distances = dists;
        data.PointDirection = dirs;

        return data;
    }

    /// <summary>
    /// Converts distance to percentage progress value
    /// </summary>
    public float DistanceToProgress(float distance)
    {
        return distance / GetPathLength();
    }

    // Converts progress percentage value to distance value
    public float ProgressToDistance(float progress)
    {
        return GetPathLength() * progress;
    }


    /// <summary> 
    /// Calculating distance between two points ignoring height (Y Axis) 
    /// </summary>
    public static float TopDownDistance(Vector3 startPos, Vector3 endPos)
    {
        return Mathf.Sqrt(Mathf.Pow(endPos.x - startPos.x, 2) + Mathf.Pow(endPos.z - startPos.z, 2));
    }


    #endregion

    #region Public get data methods

    /// <summary>
    /// Setting percentage progress over the path
    /// </summary>
    /// <param name="value"> Value from 0 to 1</param>
    public void SetProgress(float value)
    {
        SetTotalProgressValue(value);
    }

    /// <summary>
    /// Setting path progress at the point where it would on the path if it travel provided distance in units
    /// </summary>
    public void SetProgressByDistance(float distance)
    {
        SetTotalProgressValue(GetProgressAtDistance(distance));
    }

    /// <summary>
    /// Returning current path's progress value
    /// </summary>
    public float GetProgressValue()
    {
        return progress;
    }

    /// <summary>
    /// Returning direction over path at provided progress
    /// </summary>
    internal Vector3 GetDirectionAtProgress(float progress)
    {
        return PointDirection[GetIndexInProgress(progress)];
    }

    ///// <summary>
    ///// Returning percentage (0 to 1) value of actual progress on the path
    ///// </summary>
    //internal float GetActualTotalProgressValue()
    //{
    //    if (LoopPath)
    //        return Mathf.Lerp(PointProgressLoop[currentPoint], PointProgressLoop[currentPoint + 1], pointProgress);
    //    else
    //    {
    //        if (currentPoint == PathPoints.Length - 1) return 1f;
    //        return Mathf.Lerp(PointProgress[currentPoint], PointProgress[currentPoint + 1], pointProgress);
    //    }
    //}


    /// <summary> 
    /// Returning position of current progress on the path 
    /// </summary>
    internal Vector3 GetCurrentProgressPosition()
    {
        return GetProgressPositionIn(currentPoint, nextIndex, pointProgress);
    }

    /// <summary>
    /// Returns total progress value which is at provided distance over length of the path
    /// </summary>
    public float GetProgressAtDistance(float progressDistance)
    {
        return Mathf.InverseLerp(0f, GetPathLength(), progressDistance);
    }

    /// <summary>
    /// Returning position of current progress over path with additional distance over path so you can get predicted position for next frames 
    /// </summary>
    internal Vector3 GetCurrentProgressPositionForwarded(float additionalDistance)
    {
        float wholeDivider = AllLength;
        float precentOfWhole = Distances[currentPoint] / AllLength;
        float targetProgress = pointProgress + (additionalDistance / precentOfWhole) / wholeDivider;

        if (targetProgress > 1f)
        {
            float deunify = ((targetProgress - 1f) * precentOfWhole) * wholeDivider;

            int nextPoint = nextIndex;
            int farNextPoint = nextIndex + 1;

            if (farNextPoint > PathPoints.Length - 1)
            {
                if (LoopPath) farNextPoint = 0; else farNextPoint = nextIndex;
            }

            precentOfWhole = Distances[nextPoint] / AllLength;
            targetProgress = (deunify / precentOfWhole) / wholeDivider;

            return GetProgressPositionIn(nextPoint, farNextPoint, targetProgress);
        }
        else if (targetProgress < 0f)
        {
            float deunify = ((-targetProgress) * precentOfWhole) * wholeDivider;

            int prePoint = currentPoint;
            int farPrePoint = previousIndex;

            precentOfWhole = Distances[prePoint] / AllLength;
            targetProgress = (deunify / precentOfWhole) / wholeDivider;

            return GetProgressPositionIn(prePoint, farPrePoint, targetProgress);
        }
        else
        {
            return GetProgressPositionIn(currentPoint, nextIndex, targetProgress);
        }
    }


    /// <summary> 
    /// Returning direction value of current point on the path 
    /// </summary>
    public Vector3 GetCurrentProgressDirection()
    {
        return PointDirection[currentPoint];
    }


    /// <summary> 
    /// Returning direction value of current point on the path with transitioning to next direction when distance to point starts to be lower than provided value
    /// </summary>
    public Vector3 GetCurrentProgressDirection(float smoothingDistance = 0.5f)
    {
        if (smoothingDistance <= 0f) return GetCurrentProgressDirection();

        Vector3 currentPosition = GetCurrentProgressPosition();

        float distance = Vector3.Distance(currentPosition, PathPoints[nextIndex]);

        return Vector3.Slerp(PointDirection[nextIndex], PointDirection[currentPoint], distance / smoothingDistance);
    }


    /// <summary>
    /// Returning position of nearest point of path from provided position in world space
    /// </summary>
    public Vector3 GetNearestPositionToPathFrom(Vector3 from)
    {
        float nearestDist = 1000000f;
        int nearest = 0;

        for (int i = 0; i < PathPoints.Length - 1; i++)
        {
            float dist = TopDownDistance(from, PathPoints[i]);

            if (dist < nearestDist)
            {
                nearest = i;
                nearestDist = dist;
            }
        }

        Vector3 point = GetNearestPositionToLine(PathPoints[nearest], PathPoints[nearest + 1], from);

        return point;
    }


    /// <summary>
    /// (WARNING: Not fully finished) 
    /// Returning progress to nearest point of path from provided position in world space
    /// </summary>
    public float GetNearestProgressToPathFrom(Vector3 from)
    {
        int nearest = 0;
        float nearestDist = 1000000f;

        for (int i = 0; i < PathPoints.Length - 1; i++)
        {
            float dist = Vector3.Distance(from, PathPoints[i]);

            if (dist < nearestDist)
            {
                nearest = i;
                nearestDist = dist;
            }
        }

        Vector3 point1 = GetNearestPositionToLine(PathPoints[nearest], PathPoints[nearest + 1], from);
        Vector3 point2 = GetNearestPositionToLine(PathPoints[(nearest == 0? PathPoints.Length-1 : nearest-1)], PathPoints[nearest], from);

        Debug.DrawLine(from, point1, Color.green);
        Debug.DrawLine(from, point2, Color.yellow);

        if (Vector3.Distance(point1, from) > Vector3.Distance(point2, from))
        {
            nearest -= 1;
            if (nearest < 0) nearest = PathPoints.Length - 1;
        }

        int next = nearest + 1;

        if (LoopPath)
        {
            if (next > PathPoints.Length - 1) next = 0;
        }
        else
        {
            if (next > PathPoints.Length - 1) next = PathPoints.Length - 1;
        }

        float distToEnd = Vector3.Distance(from, PathPoints[next]);
        float distFromStartToEnd = Vector3.Distance(PathPoints[nearest], PathPoints[next]);

        float progressBetween = Mathf.InverseLerp(0f, distFromStartToEnd, distToEnd);

        return GetTotalProgressFromBetween(nearest, next, progressBetween);
    }


    /// <summary>
    /// Returning total path length in linear manner (don't forget about looped paths)
    /// </summary>
    public float GetPathLength()
    {
        if (LoopPath) return AllLengthLoop; else return AllLength;
    }


    #endregion

    #region Metody do obliczeń wewnętrznych poruszania się po ścieżce

    /// <summary>
    /// When target path point is changing we need to do some new calculations to keep everything running properly 
    /// </summary>
    private void SwitchNextPathPoint(int direction = 1)
    {
        currentPoint += direction;

        if (LoopPath)
        {
            previousIndex = currentPoint - 1;

            if (currentPoint > PathPoints.Length - 1)
            {
                currentPoint = 0;
            }
            else
            {
                if (currentPoint < 0)
                {
                    currentPoint = PathPoints.Length - 1;
                }
            }

            pointProgress -= 1f * direction;
            nextIndex = currentPoint + 1;

            if (nextIndex > PathPoints.Length - 1) nextIndex = 0;
            if (previousIndex < 0) previousIndex = PathPoints.Length - 1;
        }
        else
        {
            if (currentPoint < 0) currentPoint = 0;

            previousIndex = currentPoint - 1;
            nextIndex = currentPoint + 1;

            pointProgress -= 1f * direction;

            if (nextIndex > PathPoints.Length - 1) nextIndex = PathPoints.Length - 1;
            if (previousIndex < 0) previousIndex = 0;
        }
    }


    /// <summary>
    /// Setting current target path progress point
    /// </summary>
    private void SetCurrentPointIndex(int i)
    {
        currentPoint = i;
        previousIndex = i - 1;
        nextIndex = i + 1;

        if (LoopPath)
        {
            if (nextIndex > PathPoints.Length - 1) nextIndex = 0;
            if (previousIndex < 0) previousIndex = PathPoints.Length - 1;
        }
        else
        {
            if (nextIndex > PathPoints.Length - 1) nextIndex = PathPoints.Length - 1;
            if (previousIndex < 0) previousIndex = 0;
        }
    }

    /// <summary> 
    /// Returning array with point progress values for looped and not looped manner 
    /// </summary>
    private float[] GetTargetProgressArray()
    {
        if (LoopPath) return PointProgressLoop; else return PointProgress;
    }

    /// <summary>
    /// Returning index of point in which is contained provided progress value
    /// </summary>
    private int GetIndexInProgress(float progress)
    {
        int i = 0;

        float[] targetArray = GetTargetProgressArray();

        for (i = 0; i < targetArray.Length - 1; i++)
        {
            if (targetArray[i] <= progress && targetArray[i + 1] >= progress)
            {
                break;
            }
        }

        return i;
    }

    /// <summary> 
    /// Returning main progress on path from progress between two points 
    /// </summary>
    private float GetPointProgress(int i, float progress)
    {
        float[] targetArray = GetTargetProgressArray();
        return Mathf.InverseLerp(targetArray[i], targetArray[i + 1], progress);
    }

    ///// <summary> 
    ///// Returning progress value between two points by provided global progress 
    ///// </summary>
    //private float GetPointProgressByGlobalProgress(int i, float globalProgress)
    //{
    //    float[] targetArray = GetTargetProgressArray();
    //    float pointsProgress = 
    //    return Mathf.Lerp(targetArray[i], targetArray[i + 1], pointsProgress);
    //}




    /// <summary>
    /// Returning position for progress between two points on the path
    /// </summary>
    private Vector3 GetProgressPositionIn(int firstIndex, int secondIndex, float progress)
    {
        if (PathPoints.Length == 1) secondIndex = firstIndex;

        Vector3 aPoint = PathPoints[firstIndex];
        Vector3 bPoint = PathPoints[secondIndex];

        return Vector3.Lerp(aPoint, bPoint, progress);
    }

    /// <summary>
    /// Returning nearest point to path from provided position in world space
    /// </summary>
    private Vector3 GetNearestPositionToLine(Vector3 lineStart, Vector3 lineEnd, Vector3 from)
    {
        Vector3 dirVector1 = from - lineStart;
        Vector3 dirVector2 = (lineEnd - lineStart).normalized;

        float distance = Vector3.Distance(lineStart, lineEnd);
        float dot = Vector3.Dot(dirVector2, dirVector1);

        if (dot <= 0) return lineStart;

        if (dot >= distance) return lineEnd;

        Vector3 dotVector = dirVector2 * dot;

        Vector3 closestPoint = lineStart + dotVector;

        return closestPoint;
    }

    /// <summary>
    /// Returning main progress of path from progress between two points on the path
    /// </summary>
    private float GetTotalProgressFromBetween(int start, int next, float progress)
    {
        if (LoopPath)
            return Mathf.Lerp(PointProgressLoop[start], PointProgressLoop[next], progress);
        else
        {
            if (start == PathPoints.Length - 1) return 1f;
            return Mathf.Lerp(PointProgress[start], PointProgress[next], progress);
        }
    }

    #endregion

    #region Publiczne metody przydatne w kontrolerach


    /// <summary>
    /// Setting main progress value to provided value
    /// </summary>
    private void SetTotalProgressValue(float progress)
    {
        if (LoopPath) if (progress > 1f) progress = 1f - progress;

        progress = Mathf.Clamp(progress, 0f, 1f);

        int i = GetIndexInProgress(progress);

        float pointProgress = GetPointProgress(i, progress);

        SetCurrentPointIndex(i);

        this.pointProgress = pointProgress;

        this.progress = progress;
    }

    /// <summary>
    /// Returning position of path by provided progress percentage value
    /// </summary>
    internal Vector3 GetPositionAtProgress(float totalProgress)
    {
        totalProgress = Mathf.Clamp(totalProgress, 0f, 1f);

        int i = GetIndexInProgress(totalProgress);
        int n = i + 1;

        if (n > PathPoints.Length - 1)
        {
            if (LoopPath) n = 0;
            else
                n = PathPoints.Length - 1;
        }

        float pointProgress = GetPointProgress(i, totalProgress);

        return GetProgressPositionIn(i, n, pointProgress);
    }

    #endregion

    #region Deep private methods which are helping to calculate the path

    /// <summary>
    /// Checking if we should switch between path point indexes for new section, in a different way for the looped path and other for not looped path
    /// </summary>
    private void CheckToSwitchPoint()
    {
        if (pointProgress >= 1f)
        {
            if (LoopPath)
            {
                SwitchNextPathPoint();
            }
            else
            {
                if (currentPoint != PathPoints.Length - 2) SwitchNextPathPoint(); else pointProgress = 1f;
            }
        }
        else
        {
            if (pointProgress < 0f)
            {
                if (LoopPath)
                {
                    SwitchNextPathPoint(-1);
                }
                else
                {
                    if (currentPoint != 0) SwitchNextPathPoint(-1); else pointProgress = 0f;
                }
            }
        }
    }


    #endregion
}
