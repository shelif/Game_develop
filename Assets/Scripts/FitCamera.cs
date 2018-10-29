using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 强行拉伸背景图片以适应屏幕
/// </summary>
public class FitCamera : MonoBehaviour
{
    void Update()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = renderer.bounds.size.x;
        float height = renderer.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(
            worldScreenWidth / width,
            worldScreenHeight / height
        );
    }
}