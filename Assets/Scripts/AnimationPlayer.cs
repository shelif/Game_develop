using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    /// <summary>
    /// 动画速度
    /// </summary>
    public float Speed = 0.1f;

    /// <summary>
    /// 动画是否重复播放
    /// </summary>
    public bool Loop = false;

    /// <summary>
    /// 动画存在时间（为零表示结束后就销毁）
    /// </summary>
    public float LifeTime = 0f;

    /// <summary>
    /// 动画帧数
    /// </summary>
    public int FrameNumber;

    void Start()
    {
        SpriteSheet sheet = GetComponent<SpriteSheet>();
        sheet.AddAnim("animation", FrameNumber, Speed, !Loop);
        sheet.Play("animation");
        if (LifeTime > 0)
            Destroy(gameObject, LifeTime);
        else
            sheet.AddAnimationEvent("animation", -1, () => { Destroy(gameObject); });
    }
}