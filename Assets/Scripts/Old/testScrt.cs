using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScrt : MonoBehaviour
{
    public AnimationClip clip;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit catHit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out catHit, 10f))
        {
            catHit.transform.GetComponent<Animator>().Play(clip.name);
        }
    }
}
