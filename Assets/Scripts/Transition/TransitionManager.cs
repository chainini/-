using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>
{
    public void Transition(string form,string to)
    {
        StartCoroutine(TranstionToScene(form, to));
    }

    IEnumerator TranstionToScene(string form,string to)
    {
        if(form!=string.Empty)
        {
            yield return SceneManager.UnloadSceneAsync(form);
        }
        //加载场景并激活状态
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
    }
}
