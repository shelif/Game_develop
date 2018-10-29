using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoulDeathEffect : DeathScript
{
    /// <summary>
    /// 死亡升天动画的Prefab
    /// </summary>
    public Transform AnimationPrefab;

    public override void AfterDeath()
    {
        gameObject.SetActive(false);
        Transform animation = Instantiate(AnimationPrefab);
        animation.transform.position = transform.position;
    }
}