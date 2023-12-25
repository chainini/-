using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandle : Singleton<EventHandle>
{
    public static Action PlayerE;
    public static void OnPlayerE()
    {
        PlayerE?.Invoke();
    }

    public static Action<GameSceneSO, Vector3, bool> LoadRequestEvent;
    public static void CallLoadRequestEvent(GameSceneSO gameSceneSO,Vector3 vector3,bool isFade)
    {
        LoadRequestEvent?.Invoke(gameSceneSO,vector3,isFade);
    }

    public static Action AfterSceneLoadEvent;
    public static void OnAfterSceneLoadEvent()
    {
        AfterSceneLoadEvent?.Invoke();
    }

    public static Action<Color, float,bool> FadeEvent;
    public static void OnFadeEvent(Color targetColor,float duration,bool fadeIn)
    {
        FadeEvent?.Invoke(targetColor,duration,fadeIn);
    }

    public static Action StartNewGame;
    public static void CallStartNewGame()
    {
        StartNewGame?.Invoke();
    }

    public static Func<Vector3> SmallBeeRandomPos;
    public static Vector3 CallSmallBeeRandomPos()
    {
        return SmallBeeRandomPos.Invoke();
    }

    public static Func<Vector3> PlayerPos;
    public static Vector3 CallPlayerPos()
    {
        return PlayerPos.Invoke();
    }
}
