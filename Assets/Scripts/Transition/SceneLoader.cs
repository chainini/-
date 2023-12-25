using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Transform playerTrans;
    public Vector3 firstPosition;
    public GameSceneSO FirstLoatScene;

    private GameSceneSO currentLoadScene;
    private GameSceneSO tempSceneSO;
    private Vector3 tempPos;
    private bool tempIsFade;

    public bool isLoading;

    public float fadeDuration;

    public GameSceneSO menuScene;
    public Vector3 menuPos;


    private void Awake()
    {
        //Addressables.LoadSceneAsync(FirstLoatScene.sceneReference,LoadSceneMode.Additive);
        //currentLoadScene = FirstLoatScene;
        //currentLoadScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true); 
    }

    private void Start()
    {
        EventHandle.CallLoadRequestEvent(menuScene, menuPos, true);
        //NewGame();
    }

    private void OnEnable()
    {
        EventHandle.LoadRequestEvent += OnLoadRequestEvent;
        EventHandle.StartNewGame += NewGame;
    }
    private void OnDisable()
    {
        EventHandle.LoadRequestEvent -= OnLoadRequestEvent;
        EventHandle.StartNewGame -= NewGame;
    }

    private void NewGame()
    {
        tempSceneSO = FirstLoatScene;
        EventHandle.CallLoadRequestEvent(tempSceneSO, firstPosition, true);
    }


    /// <summary>
    /// 场景加载事件请求
    /// </summary>
    /// <param name="sceneToGo"></param>
    /// <param name="positionToGo"></param>
    /// <param name="isFade"></param>
    private void OnLoadRequestEvent(GameSceneSO sceneToGo, Vector3 positionToGo, bool isFade)
    {
        
        if(isLoading)
        {
            return;
        }
        isLoading = true;
        tempSceneSO = sceneToGo;
        tempPos = positionToGo;
        tempIsFade = isFade;
        if (currentLoadScene != null)
            StartCoroutine(UnLoadPreviousScene());
        else
            LoadNewScene();
    }

    IEnumerator UnLoadPreviousScene()
    {
        if(tempIsFade)
        {
            EventHandle.OnFadeEvent(Color.black, fadeDuration, true);
        }

        yield return new WaitForSeconds(fadeDuration);

        yield return currentLoadScene.sceneReference.UnLoadScene();
        
        //关闭人物
        playerTrans.gameObject.SetActive(false);
        LoadNewScene();
    }
    //加载新场景
    private void LoadNewScene()
    {
        var loadingOption = tempSceneSO.sceneReference.LoadSceneAsync(LoadSceneMode.Additive,true);
        loadingOption.Completed += OnLoadCompleted;
    }

    /// <summary>
    /// 场景加载完后
    /// </summary>
    /// <param name="handle"></param>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        currentLoadScene = tempSceneSO;
        playerTrans.position= tempPos;
        playerTrans.gameObject.SetActive(true);
        if (tempIsFade)
        {
            EventHandle.OnFadeEvent(Color.clear, fadeDuration, false);
        }

        isLoading = false;

        EventHandle.OnAfterSceneLoadEvent();
    }
}
