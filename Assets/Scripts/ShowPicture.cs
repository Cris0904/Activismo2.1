using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPicture : MonoBehaviour
{
    [SerializeField]
    RawImage Rawpicture;

    [SerializeField]
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    public void Show(Texture imageText)
    {
        canvasGroup.alpha = 1;
        Rawpicture.texture = imageText;
        
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
    }
}
