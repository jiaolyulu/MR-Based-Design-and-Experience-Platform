using UnityEngine;

public class ScreenshotMovie : MonoBehaviour
{
   
    public string folder = "ScreenshotMovieOutput";
    public int frameRate = 30;
    public int sizeMultiplier = 1;

    private string realFolder = "";

    void Start()
    {
        
        Time.captureFramerate = frameRate;

        realFolder = folder;
        int count = 1;
        while (System.IO.Directory.Exists(realFolder))
        {
            realFolder = folder + count;
            count++;
        }
        // Create the folder
        System.IO.Directory.CreateDirectory(realFolder);
    }

    void Update()
    {
       
        var name = string.Format("{0}/shot {1:D04}.png", realFolder, Time.frameCount);

        // Capture the screenshot
        ScreenCapture.CaptureScreenshot(name, sizeMultiplier);
    }
}
