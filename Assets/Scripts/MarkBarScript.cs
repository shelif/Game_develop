using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBarScript : MonoBehaviour
{
    /// <summary>
    /// 得分
    /// </summary>
    public int Mark = 0;

    /// <summary>
    /// 显示尺寸
    /// </summary>
    public Vector2 Size = new Vector2(100, 20);

    /// <summary>
    /// 分数栏的位置（距离屏幕右上角的x距离和y距离）
    /// </summary>
    public Vector2 Position = new Vector2(10, 10);

    /// <summary>
    /// 分数显示的风格
    /// </summary>
    public GUIStyle Style;

    void Awake()
    {
        Mark = CrossScenesScript.mark;
    }

    void OnGUI()
    {
        Style.fontSize = (int)Size.y;
        GUI.TextArea(new Rect(Screen.width - Size.x - Position.x, Position.y, Size.x, Size.y), String.Format("Mark: {0}", Mark), Style);
    }
}