using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeColorScript : MonoBehaviour
{
    //Material tubeMaterial;
    RaycastHit catHit;
    int colorNumber;
    public Material catHeadMaterial;
    public Material catTailMaterial;
    public Material baseMaterial;
    AudioSource source;
    public AudioClip[] meowSound;
    float MIN_PITCH = 0.75f;
    float MAX_PITCH = 1.25f;
    Color newColor;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        colorNumber = 0;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {   
            if (Input.GetTouch(0).tapCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    if (touch.phase == TouchPhase.Ended)
                    {
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out catHit, 10f))
                        {
                            if (catHit.collider != null && catHit.collider.CompareTag("Plane") == false)
                            {
                                GameObject parent = catHit.collider.transform.parent.gameObject;
                                animator = parent.transform.GetChild(parent.transform.childCount - 1).GetComponent<Animator>();
                                int animNumber = Random.Range(1, 4);
                                animator.Play("Cat Anim " + animNumber);
                                //tubeMaterial = parent.GetComponent<TubeRenderer>().material;

                                colorNumber += 1;
                                if (colorNumber > 7)
                                {
                                    ResetColorCycle();
                                }

                                PlayMeow();
                                ChangeCatColor();
                            }
                        }
                    }
                }
            }
        }
    }

    void ChangeCatColor()
    {
        switch (colorNumber)
        {
            default:
                break;

            case 0:
                newColor = Color.white;
                break;

            case 1:
                newColor = new Color32(179, 179, 179, 1);
                break;

            case 2:
                newColor = new Color32(77, 77, 77, 1);
                break;

            case 3:
                newColor = new Color32(19, 19, 19, 1);
                break;

            case 4:
                newColor = new Color32(199, 177, 153, 1);
                break;

            case 5:
                newColor = new Color32(251, 176, 59, 1);
                break;

            case 6:
                newColor = new Color32(96, 56, 20, 1);
                break;

            case 7:
                newColor = new Color32(237, 118, 137, 1);
                break;
        }

        baseMaterial.color = newColor;
        catHeadMaterial.color = newColor;
        catTailMaterial.color = newColor;
    }

    public void ResetColorCycle()
    {
        colorNumber = 0;
        baseMaterial.color = Color.white;
        catHeadMaterial.color = Color.white;
        catTailMaterial.color = Color.white;
    }

    void PlayMeow()
    {
        int selection = Random.Range(0, meowSound.Length);
        source.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        source.PlayOneShot(meowSound[selection]);
    }
}