using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    /// <summary>
    /// 用来记录Boss是否已经进入摄像机视野
    /// </summary>
    private bool _hasBeenSeen;

    private float _maxHealth;

    /// <summary>
    /// Boss血条距离屏幕顶端的距离
    /// </summary>
    public float Offset = 10.0f;

    /// <summary>
    /// 血条的高度
    /// </summary>
    public float Height = 10.0f;

    /// <summary>
    /// 当前血量下的血条矩形
    /// </summary>
    /// <param name="hp">血量</param>
    /// <returns>返回的矩形</returns>
    private Rect BossHealthBarRect(float hp)
    {
        Rect bossHealthBarRect = new Rect();
        bossHealthBarRect.min = new Vector2((float) Screen.width / 4, Offset);
        bossHealthBarRect.max = new Vector2((float) Screen.width / 4 + hp / _maxHealth * (float) Screen.width / 2
            , Offset + Height);

        return bossHealthBarRect;
    }

    /// <summary>
    /// 用于显示血条最大值的矩形
    /// </summary>
    /// <returns>返回的矩形</returns>
    private Rect BossMaxHealthBarRect()
    {
        Rect bossHealthBarRect = new Rect();
        bossHealthBarRect.min = new Vector2((float) Screen.width / 4, Offset);
        bossHealthBarRect.max = new Vector2((float) Screen.width * 3 / 4, Offset + Height);

        return bossHealthBarRect;
    }

    private void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }

    private Color ColorByHp(int hp)
    {
        float r = 0, g = 0, b = 0;
        float halfHealth = _maxHealth / 2;
        if (hp > halfHealth)
        {
            g = 1.0f;
            r = 1.0f + (hp - halfHealth) / halfHealth * -1.0f;
        }
        else
        {
            r = 1.0f;
            g = hp / halfHealth;
        }

        return new Color(r, g, b);
    }

    void OnGUI()
    {
        int hp = GetComponent<HealthScript>().hp;
        if (_hasBeenSeen)
        {
            DrawQuad(BossHealthBarRect(GetComponent<HealthScript>().hp), ColorByHp(hp));
            GUI.DrawTexture(BossMaxHealthBarRect(), Resources.Load<Texture2D>("bbblood"));
        }
    }

    void OnBecameVisible()
    {
        // 以刚进入摄像机时的HP作为程序使用的最大HP
        if (!_hasBeenSeen)
            _maxHealth = GetComponent<HealthScript>().hp;

        _hasBeenSeen = true;
    }
}