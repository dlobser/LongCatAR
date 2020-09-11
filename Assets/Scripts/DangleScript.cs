using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangleScript : MonoBehaviour
{
    Vector3 currentRotate;
    public int maxRotateAmt = 10;
    public float angleOffset = 10;
    public bool xAxis = false;
    public bool tail = false;
    float angleOffsetAmt;
    float timeOffset;
    //int numChildren;

    // Start is called before the first frame update
    void Start()
    {
        angleOffsetAmt = angleOffset * Random.value;
        timeOffset = 3 * Random.value;
        currentRotate = transform.localEulerAngles;
        //numChildren = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (tail)
        {
            transform.localEulerAngles = new Vector3(currentRotate.x, currentRotate.y, currentRotate.z + Mathf.Sin(Time.time * 2) * maxRotateAmt);
        }

        if (xAxis && !tail)
        {
            //transform.localEulerAngles = new Vector3(currentRotate.x, currentRotate.y + Mathf.Sin(Time.time * timeOffset) * maxRotateAmt + angleOffset, currentRotate.z);
            transform.localEulerAngles = new Vector3(currentRotate.x + Mathf.Sin(Time.time * timeOffset) * maxRotateAmt + angleOffsetAmt, currentRotate.y, currentRotate.z);
        }

        if (!xAxis && !tail)
        {
            transform.localEulerAngles = new Vector3(currentRotate.x, currentRotate.y, currentRotate.z + Mathf.Sin(Time.time * timeOffset) * maxRotateAmt + angleOffsetAmt);
        }
    }
}
