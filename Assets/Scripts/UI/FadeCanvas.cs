using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeCanvas : MonoBehaviour
{
    public Image fadeImage;

    private void OnEnable()
    {
        EventHandle.FadeEvent += OnFadeEvent;
    }
    private void OnDisable()
    {
        EventHandle.FadeEvent -= OnFadeEvent;
    }

    private void OnFadeEvent(Color targetColor,float duration, bool fadeIn)
    {
        fadeImage.DOBlendableColor(targetColor, duration);
    }
}
