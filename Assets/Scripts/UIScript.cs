using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIScript : MonoBehaviour
{
    public GameObject StartScreenUI;
    public GameObject LookAroundUI;
    public GameObject DoneLookingUI;
    public GameObject GameUI;
    public GameObject InstructionsUI;
    public GameObject ScreenshotUI;
    public GameObject SsInstructionsUI;
    public GameObject ShareUI;
    public RawImage screenshotPreview;
    public GameState currentState;
    public enum GameState
    {
        StartScreen,
        LookAroundScreen,
        DoneLookingScreen,
        GameScreen,
        InstructionsScreen,
        ScreenshotScreen,
        SsInstructionsScreen,
        ShareScreen,

    }
    private ARRaycastManager rayManager;
    private float currentTime;
    private bool canScreenshot;

    private void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        SetState(GameState.StartScreen);
    }

    private void Update()
    {
        if (currentState == GameState.StartScreen)
        {
            if (Input.touchCount > 0 || Time.time > 3f)
            {
                SetState(GameState.LookAroundScreen);
            }
        }

        if (currentState == GameState.LookAroundScreen)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
            if (hits.Count > 0 && Time.time > 5f)
            {
                currentTime = Time.time;
                SetState(GameState.DoneLookingScreen);
            }
        }

        if (currentState == GameState.DoneLookingScreen)
        {
            if (Time.time - currentTime > 2f)
            {
                SetState(GameState.InstructionsScreen);
            }
        }
    }

    private void SetState(GameState newState)
    {
        currentState = newState;

        if (newState == GameState.StartScreen) 
        {
            StartScreenUI.SetActive(true);
            LookAroundUI.SetActive(false);
            DoneLookingUI.SetActive(false);
            GameUI.SetActive(false);
            InstructionsUI.SetActive(false);
            ScreenshotUI.SetActive(false);
            SsInstructionsUI.SetActive(false);
            ShareUI.SetActive(false);
        }

        if (newState == GameState.LookAroundScreen)
        {
            StartScreenUI.SetActive(false);
            LookAroundUI.SetActive(true);
            DoneLookingUI.SetActive(false);
            GameUI.SetActive(false);
            InstructionsUI.SetActive(false);
            ScreenshotUI.SetActive(false);
            SsInstructionsUI.SetActive(false);
            ShareUI.SetActive(false);
        }

        if (newState == GameState.DoneLookingScreen)
        {
            StartScreenUI.SetActive(false);
            LookAroundUI.SetActive(false);
            DoneLookingUI.SetActive(true);
            GameUI.SetActive(false);
            InstructionsUI.SetActive(false);
            ScreenshotUI.SetActive(false);
            SsInstructionsUI.SetActive(false);
            ShareUI.SetActive(false);
        }

        if (newState == GameState.GameScreen)
        {
            StartScreenUI.SetActive(false);
            LookAroundUI.SetActive(false);
            DoneLookingUI.SetActive(false);
            GameUI.SetActive(true);
            InstructionsUI.SetActive(false);
            ScreenshotUI.SetActive(false);
            SsInstructionsUI.SetActive(false);
            ShareUI.SetActive(false);
        }

        if (newState == GameState.InstructionsScreen)
        {
            StartScreenUI.SetActive(false);
            LookAroundUI.SetActive(false);
            DoneLookingUI.SetActive(false);
            GameUI.SetActive(false);
            InstructionsUI.SetActive(true);
            ScreenshotUI.SetActive(false);
            SsInstructionsUI.SetActive(false);
            ShareUI.SetActive(false);
        }

        if (newState == GameState.ScreenshotScreen)
        {
            StartScreenUI.SetActive(false);
            LookAroundUI.SetActive(false);
            DoneLookingUI.SetActive(false);
            GameUI.SetActive(false);
            InstructionsUI.SetActive(false);
            ScreenshotUI.SetActive(true);
            SsInstructionsUI.SetActive(false);
            ShareUI.SetActive(false);
        }

        if (newState == GameState.SsInstructionsScreen)
        {
            StartScreenUI.SetActive(false);
            LookAroundUI.SetActive(false);
            DoneLookingUI.SetActive(false);
            GameUI.SetActive(false);
            InstructionsUI.SetActive(false);
            ScreenshotUI.SetActive(false);
            SsInstructionsUI.SetActive(true);
            ShareUI.SetActive(false);
        }

        if (newState == GameState.ShareScreen)
        {
            StartScreenUI.SetActive(false);
            LookAroundUI.SetActive(false);
            DoneLookingUI.SetActive(false);
            GameUI.SetActive(false);
            InstructionsUI.SetActive(false);
            ScreenshotUI.SetActive(false);
            SsInstructionsUI.SetActive(false);
            ShareUI.SetActive(true);
        }
    }

    public void SetGameScreen()
    {
        SetState(GameState.GameScreen);
    }

    public void SetInstructionScreen()
    {
        SetState(GameState.InstructionsScreen);
    }

    public void SetScreenshotScreen()
    {
        SetState(GameState.ScreenshotScreen);
    }

    public void SetSsInstructionsScreen()
    {
        SetState(GameState.SsInstructionsScreen);   
    }

    public void SetShareScreen()
    {
        SetState(GameState.ShareScreen);
    }

}
