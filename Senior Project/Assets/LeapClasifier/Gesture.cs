
using System;
using UnityEngine;

public class Gesture
{
    public Point[] Points = null;
    public string Name = "";
    public const int SAMPLING_RESOLUTION = 32;

    public Gesture(Point[] points, string gestureName = "")
    {
        this.Name = gestureName;


        Points = points;
        Points = removeInfin(Points);

        foreach(Point p in Points)
        {
            if (float.IsNaN(p.x)) { 
                Debug.Log("The NaN is there before I do anything WTF!!!");
               break;
            }
            if (float.IsInfinity(p.x))
            {
                Debug.Log("The Infin is there before I do anything WTF!!!");
                break;
            }
        }

        // normalizes the array of points with respect to scale, origin, and number of points


        Points = TranslateTo(Points, Centroid(Points));

        foreach (Point p in this.Points)
        {
            if (float.IsNaN(p.x))
            {
                Debug.Log("The NaN is after Translate");
                break;
            }
            if (float.IsInfinity(p.x))
            {
                Debug.Log("The Infin is after Translate");
                break;
            }
        }
        Points = Resample(Points, SAMPLING_RESOLUTION);

        foreach (Point p in this.Points)
        {
            if (float.IsNaN(p.x)) { 
                Debug.Log("The NaN is after resample");
            break;
            }
            if (float.IsInfinity(p.x))
            {
                Debug.Log("The Infin is after Resample");
                break;
            }
        }
        
        Points = Scale(Points);

        foreach (Point p in this.Points)
        {
            if (float.IsNaN(p.x))
            {
                Debug.Log("The NaN is after Scale");
                //  Debug.Log(points);
                break;
            }
            if (float.IsInfinity(p.x))
            {
                Debug.Log("The Infin is after Scale");
                break;
            }

        }
    }

    public Point[] removeInfin(Point[] points)
    {
        Point lastPoint = new Point(0, 0, 0, 0);
        for(int i = 0; i<points.Length;i++)
        {
            if (float.IsInfinity(points[i].x))
            {
                points[i] = lastPoint;
                Debug.Log("Got one infinity");
            }
            lastPoint = points[i];
        }
        return points;
    }

    public float pointDistance(Point a, Point b)
    {
        return (float)Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2) + Math.Pow(a.z - b.z, 2));
    }
    /// <summary>
    /// Performs scale normalization with shape preservation into [0..1]x[0..1]
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public Point[] Scale(Point[] points)
    {
        float minx = float.MaxValue, miny = float.MaxValue, maxx = float.MinValue, maxy = float.MinValue, minz = float.MaxValue, maxz = float.MinValue;

        for (int i = 0; i < points.Length; i++)
        {
            if (minx > points[i].x) minx = points[i].x;
            if (miny > points[i].y) miny = points[i].y;
            if (minz > points[i].z) minz = points[i].z;
            if (maxx < points[i].x) maxx = points[i].x;
            if (maxy < points[i].y) maxy = points[i].y;
            if (maxz < points[i].z) maxz = points[i].z;
        }

        Point[] newPoints = new Point[points.Length];
        float scale = Math.Max(Math.Max(maxx - minx, maxy - miny), maxz - minz);
        for (int i = 0; i < points.Length; i++)
            newPoints[i] = new Point((points[i].x - minx) / scale, (points[i].y - miny) / scale, (points[i].z - minz) / scale, points[i].stroke);
        return newPoints;
    }

    /// <summary>
    /// Translates the array of points by p
    /// </summary>
    /// <param name="points"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public Point[] TranslateTo(Point[] points, Point p)
    {
        Point[] newPoints = new Point[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            newPoints[i] = new Point(points[i].x - p.x, points[i].y - p.y, points[i].z - p.z, points[i].stroke);
            if (float.IsNaN(newPoints[i].x))
                Debug.Log("Translate to is getting a NaN, handed point is: " + points[i].ToString());
        }
        return newPoints;
    }

    /// <summary>
    /// Computes the centroid for an array of points
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public Point Centroid(Point[] points)
    {
        float cx = 0, cy = 0, cz = 0;
        for (int i = 0; i < points.Length; i++)
        {
            cx += points[i].x;
            cy += points[i].y;
            cz += points[i].z;
        }
        Point r = new Point(cx / points.Length, cy / points.Length, cz / points.Length, 0);
        if (float.IsNaN(r.x) || float.IsInfinity(r.x))
            Debug.Log("During Centroid we encountered an issue. Points length is: " + points.Length + " and the input point cx was" + cx);

        return new Point(cx / points.Length, cy / points.Length, cz / points.Length, 0);
    }


    /// <summary>
    /// Computes the path length for an array of points
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public float PathLength(Point[] points)
    {
        float length = 0;
        for (int i = 1; i < points.Length; i++)
            if (points[i].stroke == points[i - 1].stroke)
                length += pointDistance(points[i - 1], points[i]);
        return length;
    }


    /// <summary>
    /// Resamples the array of points into n equally-distanced points
    /// </summary>
    /// <param name="points"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public Point[] Resample(Point[] points, int n)
    {
        Point[] newPoints = new Point[n];
        newPoints[0] = new Point(points[0].x, points[0].y, points[0].z, points[0].stroke);
        int numPoints = 1;

        float I = PathLength(points) / (n - 1); // computes interval length
        float D = 0;
        for (int i = 1; i < points.Length; i++)
        {
            if (points[i].stroke == points[i - 1].stroke)
            {
                float d = pointDistance(points[i - 1], points[i]);
                if (D + d >= I)
                {
                    Point firstPoint = points[i - 1];
                    while (D + d >= I)
                    {
                        // add interpolated point
                        float t = Math.Min(Math.Max((I - D) / d, 0.0f), 1.0f);
                        if (float.IsNaN(t))
                            t = 0.5f;
                        newPoints[numPoints++] = new Point(
                            (1.0f - t) * firstPoint.x + t * points[i].x,
                            (1.0f - t) * firstPoint.y + t * points[i].y,
                            (1.0f - t) * firstPoint.z + t * points[i].z,
                            points[i].stroke
                        );

                        // update partial length
                        d = D + d - I;
                        D = 0;
                        firstPoint = newPoints[numPoints - 1];
                    }
                    D = d;
                }
                else D += d;
            }
        }

        if (numPoints == n - 1) // sometimes we fall a rounding-error short of adding the last point, so add it if so
            newPoints[numPoints++] = new Point(
                points[points.Length - 1].x,
                points[points.Length - 1].y,
                points[points.Length - 1].z,
                points[points.Length - 1].stroke);
        return newPoints;
    }
}
