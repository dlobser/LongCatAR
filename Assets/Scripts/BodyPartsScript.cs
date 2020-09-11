﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartsScript : MonoBehaviour
{
    public bool catParentObj = false;
    Transform parent;
    int childNumber;
    //Vector3 currentPosition;
    float newScale;
    float scaleTime = 0;
    float timePassed;

    //List<Vector3> points;
    Vector3[] points;
    /*
    bool collapse = false;
    int pointNum;
    int childAmt;
    float timer;
    float maxDistance;
    */
    public bool rotate = true;

    void Awake()
    {
        childNumber = transform.GetSiblingIndex();
        parent = transform.parent;
        timePassed = 0f;
        StartCoroutine(ScaleObject());
        StartCoroutine(RotateObject());

    }

    void Rotate()
    {
        if (parent.childCount > 1 && parent.childCount - 1 > childNumber)
        {
            //transform.LookAt(parent.GetChild(childNumber + 1));
            transform.LookAt(parent.GetComponent<PointsListScript>().pointsList[childNumber + 1]);
        }

        if (parent.childCount > 1 && parent.childCount - 1 == childNumber)
        {
            //transform.localEulerAngles = parent.GetChild(childNumber - 1).localEulerAngles;
            points = parent.GetComponent<PointsListScript>().splineArray;
            Vector3 point = 2.0f * transform.position - points[points.Length - 2];
            transform.LookAt(point);
        }
    }

    IEnumerator ScaleObject()
    {
        while (scaleTime <= 1)
        {
            newScale = Mathf.Lerp(0, 0.2f, scaleTime);
            transform.localScale = new Vector3(newScale, newScale, newScale);
            scaleTime += 0.05f;
            yield return null;
        }
    }

    IEnumerator RotateObject()
    {
        while (timePassed < 2f)
        {
            timePassed += Time.deltaTime;
            Rotate();
            yield return null;
        }
    }
}
