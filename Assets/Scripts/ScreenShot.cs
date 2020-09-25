using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShot : MonoBehaviour
{
    private const string ScreenshotPrefix = "LongARCat_Screen_";
    private const string ScreenshotFileType = ".png";
    private const string DateTimeFormat = "yyyyMMddhhmmss";
    private const string DefaultSubject = "LongARCat - Default subject line";
    private const string DefaultText = "LongARCat";

    public GameObject userInterface;
    public GameObject shareUI;
    public RawImage screenshotPreview;

    private Camera mainCamera;
    private string saveLocation;
    private string screenShotFilePath;
    private Texture2D screenshotTexture;
    private bool clickedShareButton;

    private void Start()
    {
        CloseShareUI();
        saveLocation = Application.temporaryCachePath;
        mainCamera = Camera.main;
        DeleteSavedScreenShots();
    }

    // We need this because the Native Share library is non blocking and doesn't notify us when the user has completed the sharing action
    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && clickedShareButton)
        {
            CloseShareUI();
        }
    }

    public void CaptureScreenShot()
    {
        DisableUserInterface();
        StartCoroutine(CaptureScreenshot());
    }

    public void ShareScreenShot()
    {
        clickedShareButton = true;
        ShareFile(screenShotFilePath);
    }

    public void CloseShareUI()
    {
        shareUI.SetActive(false);
        screenshotPreview.texture = null;
        Destroy(screenshotTexture);
        clickedShareButton = false;
    }

    private IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame();
        CaptureAndSaveScreenshot();
        EnableUserInterface();
        ShowShareUI();
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

    private void DisableUserInterface()
    {
        userInterface.SetActive(false);
    }

    private void EnableUserInterface()
    {
        userInterface.SetActive(true);
    }

    private void ShowShareUI()
    {
        SetcreenshotPreview();
        shareUI.SetActive(true);
    }

    private void SetcreenshotPreview()
    {
        screenshotPreview.texture = screenshotTexture;
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