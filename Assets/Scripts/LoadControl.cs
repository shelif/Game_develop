using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

public class LoadControl : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadSceneAsync(LoadSceneWithLoading.NextScene, LoadSceneMode.Single);
    }
}