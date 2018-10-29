using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulScript : MonoBehaviour
{
    /// <summary>
    /// 死亡动画的持续时间
    /// </summary>
    public float AnimationTime = 3.0f;

    /// <summary>
    /// 灵魂向上移动的速度
    /// </summary>
    public float AnimationSpeed = 1.0f;

    private float _currentAnimationTime;

    void Awake()
    {
        _currentAnimationTime = AnimationTime;
    }

    void Update()
    {
        _currentAnimationTime -= Time.deltaTime;

        transform.Translate(new Vector3(0, AnimationSpeed * Time.deltaTime));

        if (_currentAnimationTime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color",
                new Color(1, 1, 1, _currentAnimationTime / AnimationTime));
        }
    }
}