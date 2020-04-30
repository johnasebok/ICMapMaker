using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SFB;
using System.Runtime.InteropServices;

public class Image_Import : MonoBehaviour
{
    public static Color32[] planetColorMap;

    private Texture2D importedTexture;
    [Header("Map Settings, 0=Empty 1=System 2=Family")]
    public Color32[] colorMap;

    private void Start()
    {
        planetColorMap = colorMap;
    }
#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnClick() {
        UploadFile(gameObject.name, "OnFileUpload", ".png, .jpg", false);
    }

    // Called from browser
    public void OnFileUpload(string url) {
       StartCoroutine(OutputRoutine(url));
    }
#else
    //
    // Standalone platforms & editor
    //




    public void OnClick()
    {

        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "png", false);
        if (paths.Length > 0)
        {
            //StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));

            importedTexture = new WWW(new System.Uri(paths[0]).AbsoluteUri).texture;
            GUIManager.guiManager.makeMapFromImage(importedTexture);
        }
    }
#endif

    private IEnumerator OutputRoutine(string url)
    {
        var loader = new WWW(url);
        yield return loader;
        importedTexture = loader.texture;
        GUIManager.guiManager.makeMapFromImage(importedTexture);
        

    }




}
