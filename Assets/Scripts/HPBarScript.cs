using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 显示生命条，随便拖到一个GameObject上就成
/// </summary>
public class HPBarScript : MonoBehaviour
{
    /// <summary>
    /// 用于显示生命值的对象，需要带有HealthScript
    /// </summary>
    public Transform Player;

    /// <summary>
    /// 用于显示生命的红心
    /// </summary>
    public Texture Heart;

    /// <summary>
    /// 生命条的位置
    /// </summary>
    public Vector2 Position = new Vector2(10, 10);

    /// <summary>
    /// 生命条的大小
    /// </summary>
    public Vector2 Size = new Vector2(10, 10);

    public GUIStyle Style;

    void OnGUI()
    {
        if (Player)
        {
            Style.fontSize = (int)Size.y;
            GUI.DrawTexture(new Rect(Position.x, Position.y, Size.x, Size.y), Heart, ScaleMode.StretchToFill);
            GUI.Box(new Rect(
                Position.x + Size.x + 10,
                Position.y,
                100,
                Size.y
                ), String.Format("x {0}", Player.GetComponent<HealthScript>().hp), Style);
        }
    }
}