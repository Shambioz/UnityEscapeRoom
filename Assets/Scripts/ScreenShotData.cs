using UnityEngine;
using System.IO;

public class ScreenShotData : MonoBehaviour
{
    public Camera screenshotCamera; // Assign the camera you want to use for the screenshot
    public string folderName = "Screenshots"; // Name of the folder to store screenshots
    public string screenshotFileName = "screenshot.png"; // Name of the screenshot file
    public string FileNameHeader = "screenshot_"; // Name of the screenshot file

    private void OnApplicationQuit()
    {
        string screenshotFileName = FileNameHeader + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

        if (screenshotCamera == null)
        {
            Debug.LogError("Screenshot camera is not assigned!");
            return;
        }

        // Enable the camera
        screenshotCamera.gameObject.SetActive(true);

        // Create the folder if it doesn't exist
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Debug.Log("Created folder: " + folderPath);
        }

        // Create a render texture to capture the camera's view
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        screenshotCamera.targetTexture = renderTexture;

        // Render the camera's view to the render texture
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshotCamera.Render();
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // Save the screenshot to the folder
        string filePath = Path.Combine(folderPath, screenshotFileName);
        byte[] bytes = screenshot.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);

        Debug.Log("Screenshot saved to: " + filePath);

        // Clean up
        screenshotCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);
        Destroy(screenshot);

        // Disable the camera
        screenshotCamera.gameObject.SetActive(false);
    }
}
