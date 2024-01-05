using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotShare : MonoBehaviour
{
    public Animator screenFlash;
    public void share()
    {
        StartCoroutine(takeScreenShot());
    }
    private IEnumerator takeScreenShot()
    {
        StartCoroutine(waittillflash());
        yield return new WaitForEndOfFrame();
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();
        string filepath = System.IO.Path.Combine(Application.temporaryCachePath, "share.png");
        System.IO.File.WriteAllBytes(filepath, ss.EncodeToPNG());
        Destroy(ss);
        new NativeShare().AddFile(filepath)
            .SetSubject("").SetText("").SetUrl("").SetCallback((res, target) => Debug.Log($"results {res},target app: {target}"))
            .Share();
    }
    private IEnumerator waittillflash()
    {
        screenFlash.SetBool("screenshotFlash", true);
        yield return new WaitForSeconds(1);
        screenFlash.SetBool("screenshotFlash", false);
        
    }
}
