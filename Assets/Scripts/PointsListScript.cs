using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsListScript : MonoBehaviour
{
    public List<Vector3> pointsList;
    public Vector3[] pointsArray;
    public Vector3[] splineArray;
    //public Transform[] transformArray;
    public Vector3[] deleteArray;
    public float[] radiusArray;
    public float radius = 0.06f;
    int multiplier = 2;

    private void Start()
    {

    }

    public void AddPoint(Vector3 newPoint)
    {
        pointsList.Add(newPoint);
        int listCount = pointsList.Count;

        if (listCount >= 3)
        {
            pointsList[listCount - 2] = Vector3.Lerp(pointsList[listCount - 3], pointsList[listCount - 1], 0.5f);
        }

        pointsArray = new Vector3[listCount];

        for (int i = 0; i < listCount; i++)
        {
            pointsArray[i] = pointsList[i];
        }

        splineArray = new Vector3[multiplier * listCount];
        for (int i = 0; i < multiplier * listCount; i++)
        {
            splineArray[i] = CatmullRomSpline.GetSplinePos(pointsArray, (float) i/(multiplier * listCount));
        }

        radiusArray = new float[multiplier * listCount];
        for (int i = 0; i < multiplier * listCount; i++)
        {
            radiusArray[i] = radius;
            if (i == multiplier * listCount - 1)
            {
                radiusArray[i] = radius * 0.5f;
            }
            if (i == multiplier * listCount - 2)
            {
                radiusArray[i] = radius * 0.75f;
            }
        }

        deleteArray = new Vector3[multiplier * listCount];
    }
    
    public void DeletePoint()
    {
        deleteArray = new Vector3[deleteArray.Length - 1];
        if (deleteArray.Length != 0)
        {
            for (int i = 0; i < deleteArray.Length; i++)
            {
                deleteArray[i] = splineArray[i];
            }
        }
    }
}
