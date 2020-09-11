using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testColorScript : MonoBehaviour
{
    int colorNum;
    Material material;
    public Texture[] textures;
    // Start is called before the first frame update
    void Start()
    {
        colorNum = 0;
        material = gameObject.GetComponent<MeshRenderer>().material;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeColor();
            colorNum += 1;
            if (colorNum > 3)
            {
                colorNum = 0;
            }
        }
    }

    void ChangeColor()
    {
        material.color = Color.blue;
        material.SetTexture("_MainTex", textures[colorNum]);
        Debug.Log("changed color");
        GameObject ball = new GameObject("1");
        ball.transform.parent = gameObject.transform;
    }
}
