using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Easy.FrameUnity.Manager;
using Easy.FrameUnity.Net;

public enum ScenesName
{
    A_SceneEnter,
    B_SceneLoading,
    C_SceneLogin,
    D_SceneGameInit,
    E_SceneGame_1,
    F_SceneGame_2
}

public class ScenesManager : Singleton<ScenesManager> 
{
    private void Awake()
    {
        base.GetInstance();
    }

    public void EnterScene(ScenesName name)
    {
        AsyncOperation op;
        switch(name)
        {
            case ScenesName.A_SceneEnter:
                break;
            case ScenesName.B_SceneLoading:
                break;
            case ScenesName.C_SceneLogin:
                op = SceneManager.LoadSceneAsync("C_SceneLogin");
                break;
            case ScenesName.D_SceneGameInit:
                break;
            case ScenesName.E_SceneGame_1:
                op = SceneManager.LoadSceneAsync("E_SceneGame_1");
                StartCoroutine(Loading(op, 
                            () => 
                            { 
                            }));
                break;
            case ScenesName.F_SceneGame_2:
                break;
        }
    }

    private IEnumerator Loading(AsyncOperation op, Action action)
    {
        while(!op.isDone)
        {
            yield return null;
        }
        action.Invoke();
    }
}
