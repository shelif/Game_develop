using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPT : FlipOnAction
{
    /// <summary>
    /// 每两张图片之间变换的效果
    /// </summary>
    public float TransitionTime = 1f;

    private float _remainingTransitionTime;

    public List<Texture2D> Images = new List<Texture2D>();

    void Awake()
    {
        _remainingTransitionTime = TransitionTime;
    }

    public void OnEnd()
    {
        // 这里写播放结束后的代码
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Ending1" ||
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Ending2")
            LoadSceneWithLoading.LoadAsync("End");
        else
            LoadSceneWithLoading.Load("Guidence");
    }

    public override void Next()
    {
        if (Images.Count > 0)
        {
            if (Images.Count == 1)
            {
                OnEnd();
            }
            else
            {
                _remainingTransitionTime = TransitionTime;
                Images.RemoveAt(0);
            }
        }
    }

    private void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }

    void Update()
    {
        _remainingTransitionTime -= Time.deltaTime;
        if (_remainingTransitionTime < 0) _remainingTransitionTime = 0;
        base.Update();
    }

    public void OnGUI()
    {
        if (Images.Count > 0)
        {
            GUI.DrawTexture(
                new Rect(0, 0, Screen.width, Screen.height),
                Images[0], ScaleMode.StretchToFill);
            DrawQuad(new Rect(0, 0, Screen.width, Screen.height),
                new Color(0, 0, 0, _remainingTransitionTime / TransitionTime));
        }
    }
}