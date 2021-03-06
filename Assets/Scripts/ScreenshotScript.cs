﻿using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotScript : MonoBehaviour
{
    private const string ScreenshotPrefix = "LongCat_Screen_";
    private const string ScreenshotFileType = ".png";
    private const string DateTimeFormat = "yyyyMMddhhmmss";
    private const string DefaultSubject = "LongCatAR - Default subject line";
    private const string DefaultText = "LongCatAR - Default text";

    public RawImage screenshotPreview;
    public GameObject userInterface;
    public UIScript uiScript;
    private string saveLocation;
    private string screenShotFilePath;
    private Texture2D screenshotTexture;
    private bool clickedShareButton;

    void Start()
    {
        screenshotPreview.texture = null;
        Destroy(screenshotTexture);
        clickedShareButton = false;
        saveLocation = Application.temporaryCachePath;
        DeleteSavedScreenShots();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && clickedShareButton)
        {
            uiScript.SetScreenshotScreen();
            screenshotPreview.texture = null;
            Destroy(screenshotTexture);
            clickedShareButton = false;
        }
    }

    public void CaptureScreenshot()
    {
        DisableUI();
        StartCoroutine(CaptureScreenShot());
        screenshotPreview.texture = screenshotTexture;
    }

    public void ShareScreenshot()
    {
        clickedShareButton = true;
        ShareFile(screenShotFilePath);
    }

    private IEnumerator CaptureScreenShot()
    {
        yield return new WaitForEndOfFrame();
        CaptureAndSaveScreenshot();
        EnableUI();
        uiScript.SetShareScreen();
    }

    private void CaptureAndSaveScreenshot()
    {
        try
        {
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;
            screenshotTexture = new Texture2D(screenWidth, screenHeight, TextureFormat.RGB24, false);
            screenshotTexture.ReadPixels(new Rect(0, 0, screenWidth, screenHeight), 0, 0);
            screenshotTexture.Apply();

            string filePath = Path.Combine(saveLocation, GenerateFileName());
            File.WriteAllBytes(filePath, screenshotTexture.EncodeToPNG());

            screenShotFilePath = filePath;
            Debug.Log($"Saved screen shot - {screenShotFilePath}");
        }

        catch (Exception ex)
        {
            Debug.LogError("Failed to tak screenshot - " + ex.ToString());
        }
    }

    private void ShareFile(string filePath)
    {
        if (Application.isMobilePlatform)
        {
            try
            {
                new NativeShare()
                .AddFile(filePath)
                .SetSubject(DefaultSubject)
                .SetText(DefaultText)
                .Share();
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to share screenshot - " + ex.ToString());
            }
        }
    }

    private string GenerateFileName()
    {
        return $"{ScreenshotPrefix}{DateTime.Now.ToString(DateTimeFormat)}{ScreenshotFileType}";
    }

    private void DisableUI()
    {
        userInterface.SetActive(false);
    }

    private void EnableUI()
    {
        userInterface.SetActive(true);
    }

    private void DeleteSavedScreenShots()
    {
        try
        {
            foreach (string file in Directory.GetFiles(saveLocation, $"{ScreenshotPrefix}*"))
            {
                File.Delete(file);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error trying to delete screenshots : " + ex.ToString());
        }
    }
}
