using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class LongCatARScript : MonoBehaviour
{
    public GameObject catHeadObj;
    public GameObject catBodyObj;
    public GameObject catTailObj;
    public float minDistance = 0.3f;
    public GameObject listObj;
    public UIScript uiManager;

    private bool canCreateCat = true;
    private bool openSsInstructions = true;
    private Touch touch;
    private List<Vector3> myList;
    private GameObject listGO;
    private GameObject root;
    private PointsListScript myPointsScript;
    private RaycastHit rayHit;
    private RaycastHit catHit;
    private Camera cam;
    private TubeRenderer tube;
    private ARRaycastManager rayManager;
    private ChangeColorScript changeColorScript;

    private void Start()
    {
        root = new GameObject();
        cam = Camera.main;
        rayManager = FindObjectOfType<ARRaycastManager>();
        changeColorScript = gameObject.GetComponent<ChangeColorScript>();
        /*
        Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.Log("webcam found");
        }
        else
        {
            Debug.Log("webcam not found");
        }
        */
    }

    private void Update()
    {
        //CREATE CAT
        if (Input.touchCount == 1 && canCreateCat)
        {
            if (! EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (uiManager.currentState == UIScript.GameState.GameScreen)
                {
                    CreateCat();
                }
            }
        }

        //DELETE CAT
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).tapCount == 2)
            {
                touch = Input.GetTouch(0);

                if (Physics.Raycast(cam.ScreenPointToRay(touch.position), out catHit, 10f))
                {
                    if (catHit.collider != null)
                    {
                        tube = catHit.collider.transform.parent.GetComponent<TubeRenderer>();
                        myPointsScript = catHit.collider.transform.parent.GetComponent<PointsListScript>();
                        StartCoroutine(DeleteHead());
                        uiManager.SetGameScreen();
                        changeColorScript.ResetColorCycle();
                    }
                }
            }
        }
    }

    void CreateCat()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            listGO = Instantiate(listObj, root.transform);
            myPointsScript = listGO.GetComponent<PointsListScript>();
            myList = myPointsScript.pointsList;
            tube = listGO.GetComponent<TubeRenderer>();
            touch = Input.GetTouch(0);

            if (Physics.Raycast(cam.ScreenPointToRay(touch.position), out rayHit, 3f))
            {
                GameObject tailGO = Instantiate(catTailObj, listGO.transform);
                tailGO.transform.localScale = Vector3.zero;
                tailGO.transform.position = rayHit.point;
                myPointsScript.AddPoint(tailGO.transform.position);
                //tube.vertices = new TubeRenderer.TubeVertex[30];
                for (int i = 0; i < tube.vertices.Length; i++)
                {
                    tube.vertices[i] = new TubeRenderer.TubeVertex(Vector3.zero, 1, Color.white);
                }
            }
        }

        if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            touch = Input.GetTouch(0);

            if (Physics.Raycast(cam.ScreenPointToRay(touch.position), out rayHit, 3f))
            {
                if (Vector3.Distance(myList[myList.Count - 1], rayHit.point) > minDistance)
                {
                    myPointsScript.AddPoint(rayHit.point);
                    GameObject bodyGO = Instantiate(catBodyObj, listGO.transform);
                    bodyGO.transform.position = myPointsScript.splineArray[myPointsScript.splineArray.Length - 2];
                    GameObject bodyGO2 = Instantiate(catBodyObj, listGO.transform);
                    bodyGO2.transform.position = myPointsScript.splineArray[myPointsScript.splineArray.Length - 1];
                    tube.SetPoints(myPointsScript.splineArray, myPointsScript.radiusArray, Color.white);
                }
            }
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touch = Input.GetTouch(0);
            if (Physics.Raycast(cam.ScreenPointToRay(touch.position), out rayHit, 3f))
            {
                GameObject headGO = Instantiate(catHeadObj, listGO.transform);
                headGO.transform.localScale = Vector3.zero;
                headGO.transform.position = myPointsScript.splineArray[myPointsScript.splineArray.Length - 1];
                tube.SetPoints(myPointsScript.splineArray, myPointsScript.radius, Color.white);
                //headGO.transform.position = rayHit.point;
                //myPointsScript.AddPoint(headGO.transform.position);
            }

            if (myList.Count < 3)
            {
                StartCoroutine(ResetCat());
            }

            else
            {
                if (openSsInstructions)
                {
                    uiManager.SetSsInstructionsScreen();
                    openSsInstructions = false;
                }

                else
                {
                    uiManager.SetScreenshotScreen();
                }

                listGO.GetComponent<CatParentScript>().canMove = true;
                canCreateCat = false;
            }
        }
    }

    IEnumerator DeleteHead()
    {
        Transform headGO = catHit.transform.parent.GetChild(catHit.transform.parent.childCount - 1);
        while (headGO.transform.localScale.x > 0.01f)
        {
            Vector3 currentScale = headGO.transform.localScale;
            headGO.transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, 5 * Time.deltaTime);
            yield return null;
        }
        Destroy(headGO.gameObject);
        StartCoroutine(DeleteTube());
    }

    IEnumerator DeleteTube()
    {
        while (myPointsScript.deleteArray.Length > 0)
        {
            myPointsScript.DeletePoint();
            tube.SetPoints(myPointsScript.deleteArray, myPointsScript.radiusArray, Color.white);
            yield return null;
        }
        tube.SetPoints(myPointsScript.pointsArray, 0, Color.white);
        StartCoroutine(DeleteTail());
    }

    IEnumerator DeleteTail()
    {
        Transform tailGO = catHit.transform.parent.GetChild(0);
        while (tailGO.transform.localScale.x > 0.01f)
        {
            Vector3 currentScale = tailGO.transform.localScale;
            tailGO.transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, 5 * Time.deltaTime);
            yield return null;
        }

        Destroy(root);
        root = new GameObject();
        canCreateCat = true;
    }

    IEnumerator ResetCat()
    {
        float timer = 0f;
        while (timer < 0.5f)
        {
            Destroy(root);
            root = new GameObject();
            canCreateCat = true;
            timer += 0.5f;
            yield return null;
        }
    }
}