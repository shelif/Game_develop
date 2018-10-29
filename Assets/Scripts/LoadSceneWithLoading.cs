using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneWithLoading : MonoBehaviour
{
    /// <summary>
    /// Loading 之后转向的Scene的名字
    /// </summary>
    public static String NextScene;

    /// <summary>
    /// 不使用Loading场景进行加载，理论上比直接加载更加平滑
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadAsync(String sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// 使用Loading场景加载新的场景
    /// </summary>
    /// <param name="sceneName">加载的场景的名字</param>
    public static void Load(String sceneName)
    {
        NextScene = sceneName;
        SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Single);
    }
}