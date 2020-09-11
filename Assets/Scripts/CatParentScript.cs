using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatParentScript : MonoBehaviour
{
    Vector3 currentPosition;
    public bool canMove;
    //float currentTime;

    void Start()
    {
        currentPosition = transform.position;
        canMove = false;
        //currentTime = Time.time;
    }

    void Update()
    {
        /*
        if (Input.touchCount == 0)
        {
            transform.position = currentPosition + new Vector3(
                Mathf.Sin(0.1f * (Time.time - currentTime)) * 0.02f,
                Mathf.Sin(0.1f * (Time.time - currentTime)) * 0.02f,
                Mathf.Cos(0.1f * (Time.time - currentTime)) * 0.02f);
        }
        */
        if (canMove)
        {
            transform.position = currentPosition + new Vector3(
                Mathf.Sin(0.1f * Time.time) * 0.02f,
                Mathf.Sin(0.1f * Time.time) * 0.02f,
                Mathf.Cos(0.1f * Time.time) * 0.02f);
        }
    }
}
