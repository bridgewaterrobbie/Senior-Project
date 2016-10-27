using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PDollarMatcher : TemplateMatcher {




    public float match(List<Point> gesture, List<Point> trainingGesture)
    {
        Gesture a = new Gesture(gesture.ToArray(), "A");
        Gesture b = new Gesture(trainingGesture.ToArray(), "B");

        var l = a.Points.Length;
        //The JS says it should be 1-this.e, but it seems to be 0 anyways
        int step = (int)Math.Floor(Math.Pow(l, 1));
        float min = float.PositiveInfinity;

        for (var i = 0; i < l; i += step)
        {

            min = Math.Min(min, Math.Min(this.CloudDistance(a.Points, b.Points, i), this.CloudDistance(b.Points, a.Points, i)));
        }

        //float dist = GreedyCloudMatch(a.Points, trainingGesture.ToArray());
        return min;
    }

    public List<Point> process(List<Point> gesture)
    {
        //List<Point> points = new List<Point>();
        Gesture g = new Gesture(gesture.ToArray(), "NoName");
        List<Point> l = new List<Point>(g.Points);
        foreach (Point p in l)
        {
          ///programm  if (float.IsNaN(p.x))
              //  Debug.Log("The NaN is after Scale");
        }
        return l;
       
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    /// <summary>
    /// Main function of the $P recognizer.
    /// Classifies a candidate gesture against a set of training samples.
    /// Returns the class of the closest neighbor in the training set.
    /// </summary>
    /// <param name="candidate"></param>
    /// <param name="trainingSet"></param>
    /// <returns></returns>
    public string Classify(Gesture candidate, Gesture[] trainingSet)
    {
        float minDistance = float.MaxValue;
        string gestureClass = "";
        foreach (Gesture template in trainingSet)
        {
            float dist = GreedyCloudMatch(candidate.Points, template.Points);
            if (dist < minDistance)
            {
                minDistance = dist;
                gestureClass = template.Name;
            }
        }
        return gestureClass;
    }




    /// <summary>
    /// Computes the distance between two point clouds by performing a minimum-distance greedy matching
    /// starting with point startIndex
    /// </summary>
    /// <param name="points1"></param>
    /// <param name="points2"></param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    private float CloudDistance(Point[] points1, Point[] points2, int startIndex)
    {
        int n = points1.Length;       // the two clouds should have the same number of points by now
        bool[] matched = new bool[n]; // matched[i] signals whether point i from the 2nd cloud has been already matched
        Array.Clear(matched, 0, n);   // no points are matched at the beginning

        float sum = 0;  // computes the sum of distances between matched points (i.e., the distance between the two clouds)
        int i = startIndex;
        do
        {
            int index = -1;
            float minDistance = float.MaxValue;
            for (int j = 0; j < n; j++)
            {
                if (!matched[j])
                {
                    float dist = squaredValues(points1[i], points2[j]);  // use squared Euclidean distance to save some processing time
                    if(float.IsNaN(dist))
                    {
                       // Debug.Log("NaN and P1= " +points1[i].ToString() + " and P2= " + points2[j].ToString());
                    }
                    if (dist < minDistance)
                    {
                        minDistance = dist;
                        index = j;
                    }
              
                }
            }

            matched[index] = true; // point index from the 2nd cloud is matched to point i from the 1st cloud

            float weight = 1.0f - ((i - startIndex + n) % n) / (1.0f * n);
            sum += weight * minDistance; // weight each distance with a confidence coefficient that decreases from 1 to 0
            i = (i + 1) % n;
        } while (i != startIndex);
        return sum;
    }





    /// <summary>
    /// Implements greedy search for a minimum-distance matching between two point clouds
    /// </summary>
    /// <param name="points1"></param>
    /// <param name="points2"></param>
    /// <returns></returns>
    private float GreedyCloudMatch(Point[] points1, Point[] points2)
    {
        int n = points1.Length; // the two clouds should have the same number of points by now
        float eps = 0.5f;       // controls the number of greedy search trials (eps is in [0..1])
        int step = (int)Math.Floor(Math.Pow(n, 1.0f - eps));
        float minDistance = float.MaxValue;
        for (int i = 0; i < n; i += step)
        {
            float dist1 = CloudDistance(points1, points2, i);   // match points1 --> points2 starting with index point i
            float dist2 = CloudDistance(points2, points1, i);   // match points2 --> points1 starting with index point i
            minDistance = Math.Min(minDistance, Math.Min(dist1, dist2));
        }
        return minDistance;
    }



    private float squaredValues(Point a, Point b)
    {
        return (float)(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2) + Math.Pow(a.z - b.z, 2));
    }

}

