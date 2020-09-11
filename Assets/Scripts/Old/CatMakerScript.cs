using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMakerScript : MonoBehaviour
{
    public GameObject catHeadObj;
    public GameObject catBodyObj;
    public GameObject catTailObj;
    public float minDistance = 0.3f;


    public GameObject list;
    List<Vector3> myList;
    GameObject listGO;
    PointsListScript myPointsScript;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CreateCat();
        DestroyTube();
    }

    void CreateCat()
    {
        if (Input.GetMouseButtonDown(0))
        {
            listGO = Instantiate(list);
            myPointsScript = listGO.GetComponent<PointsListScript>();
            myList = myPointsScript.pointsList;

            GameObject tailGO = Instantiate(catTailObj);
            tailGO.transform.parent = listGO.transform;
            tailGO.transform.localScale = Vector3.zero;
            tailGO.transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            myPointsScript.AddPoint(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
        }

        if (Input.GetMouseButton(0))
        {
            if (Vector3.Distance(myList[myList.Count - 1], cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10))) > minDistance)
            {
                GameObject bodyGO = Instantiate(catBodyObj);
                bodyGO.transform.parent = listGO.transform;
                bodyGO.transform.localScale = Vector3.zero;
                myPointsScript.AddPoint(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
                bodyGO.transform.position = myList[myList.Count - 1];
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            GameObject headGO = Instantiate(catHeadObj);
            headGO.transform.parent = listGO.transform;
            headGO.transform.localScale = Vector3.zero;
            headGO.transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            myPointsScript.AddPoint(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));

            if (Vector3.Distance(myList[myList.Count - 1], myList[myList.Count - 2]) < 0.25f)
            {
                Destroy(listGO.transform.GetChild(listGO.transform.childCount - 2).gameObject);
            }
        }
    }

    void DestroyTube()
    {


    }
}
