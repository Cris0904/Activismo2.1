using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    [SerializeField]
    ShowPicture showPicture;


    bool takepicture;


    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (takepicture)
        {
            takepicture = false;


            var tempRend = RenderTexture.GetTemporary(source.width, source.height);
            Graphics.Blit(source, tempRend);


            Texture2D tempText = new Texture2D((source.width/2)+100, (source.height/2)-300, TextureFormat.RGBA32, false);
            Rect rect = new Rect(source.width/3-100,source.height/3-100, source.width/2 +100, source.height/2-300);
            tempText.ReadPixels(rect, 0,0, true);
            tempText.Apply();
            showPicture.Show(tempText);
            RenderTexture.ReleaseTemporary(tempRend);
        }

        Graphics.Blit(source, destination);

    }

    public void TakeScreenshot()
    {
        takepicture = true;
    }
}
