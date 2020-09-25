using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTubeScript : MonoBehaviour
{
    public Vector3[] myArray;
    public Vector3[] splineArray;
    //public Transform[] transformArray;
    List<Vector3> myList;
    TubeRenderer tube;
    public GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        tube = gameObject.GetComponent<TubeRenderer>();
        myList = new List<Vector3>();
        /*
        int arrayNum = 0;
        myArray = new Vector3[25];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                myArray[arrayNum] = new Vector3(i, 0, j);
                arrayNum++;
            }
        }

        tube.SetPoints(myArray, 0.5f, Color.white);
        tube.material = new Material(Shader.Find("Standard"));
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myList.Add(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
            GameObject mysphere = Instantiate(sphere);
            mysphere.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        }
        if (Input.GetMouseButton(0))
        {
            if (Vector3.Distance(myList[myList.Count - 1], Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10))) > 1f)
            {
                myList.Add(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
                GameObject mysphere = Instantiate(sphere);
                mysphere.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

                myArray = new Vector3[myList.Count];
                for (int i = 0; i < myList.Count; i++)
                {
                    myArray[i] = myList[i];
                }

                splineArray = new Vector3[myList.Count * 2];
                //transformArray = new Transform[myList.Count * 2];

                for (int i = 0; i < myList.Count * 2; i++)
                {
                    splineArray[i] = CatmullRomSpline.GetSplinePos(myArray, (float) i / (myList.Count * 2));
                    //GameObject myGO = new GameObject();
                    //myGO.transform.position = CatmullRomSpline.GetSplinePos(myArray, (float)i / (myList.Count * 2));
                    //transformArray[i] = myGO.transform;
                }

                tube.SetPoints(splineArray, 0.06f, Color.white);
            }
        }
    }
}
