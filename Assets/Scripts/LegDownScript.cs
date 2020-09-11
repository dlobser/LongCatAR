using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegDownScript : MonoBehaviour
{
    //public bool paw = false;
    float parentRotateX;
    Vector3 currentRotation;
    Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        currentRotation = transform.localEulerAngles;
        currentPos = transform.TransformPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        parentRotateX = transform.parent.parent.eulerAngles.x;
        transform.localEulerAngles = new Vector3(-1 * parentRotateX, currentRotation.y, currentRotation.z);
    }
}
