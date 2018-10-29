using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制子弹的属性
/// </summary>
public class ShotScript : MonoBehaviour
{
    /// <summary>
    /// 子弹攻击力
    /// </summary>
    public int Damage = 1;

    /// <summary>
    /// 子弹是否由敌人发出（用于区分能够造成伤害的目标）
    /// </summary>
    public bool IsEnemyShot = false;

    /// <summary>
    /// 子弹最长存在时间（秒）
    /// </summary>
    public float LifeTime = 5;

    private float _remainedLife;

    private void Awake()
    {
        _remainedLife = LifeTime;
    }

    public void Update()
    {
        _remainedLife -= Time.deltaTime;
        if (_remainedLife <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        // 子弹不能穿墙
        foreach (RaycastHit2D info in Physics2D.RaycastAll(transform.position,
            GetComponent<MoveScript>().Speed, 0.1f))
        {
            if (info.collider.gameObject.layer == LayerMask.NameToLayer("Block")
                && !info.collider.isTrigger)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}