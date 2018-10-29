using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 放在子弹上让它具有追踪功能
/// </summary>
public class Track : MonoBehaviour
{
    /// <summary>
    /// 超过这个距离的目标不能追踪
    /// </summary>
    public float VisionDistance = 10f;

    /// <summary>
    /// 追踪目标
    /// </summary>
    public String TargetTag;

    private float _currentFactor = 0;

    private float _factorTarget;

    void Start()
    {
        _factorTarget = GetComponent<ShotScript>().LifeTime * 50 / GetComponent<MoveScript>().Speed.magnitude;
    }

    void Update()
    {
        // 尝试寻找最近的目标
        GameObject target = null;
        float minDistance = VisionDistance;

        foreach (GameObject i in GameObject.FindGameObjectsWithTag(TargetTag))
        {
            float distance = (i.transform.position - transform.position).magnitude;
            if (distance <= minDistance && i.GetComponent<HealthScript>().hp > 0)
            {
                minDistance = distance;
                target = i;
            }
        }

        if (target)
        {
            _currentFactor += Time.deltaTime;
            // 设置子弹运行方向
            Vector2 direction = target.transform.position - transform.position;
            Vector2 prevDirection = GetComponent<MoveScript>().Direction;
            Vector2 newDirection = (_currentFactor * direction + _factorTarget * prevDirection) /
                                   (_factorTarget + _currentFactor);
            GetComponent<MoveScript>().Direction = newDirection;

            // 旋转子弹图片
            transform.rotation = Quaternion.Euler(0, 0,
                (float) (Math.Atan2(newDirection.y, newDirection.x) / Math.PI * 180));
        }
    }
}