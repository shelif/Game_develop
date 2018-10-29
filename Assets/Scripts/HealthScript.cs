using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 使GameObject可以被子弹击中
/// </summary>
public class HealthScript : MonoBehaviour
{
    /// <summary>
    /// 生命值
    /// </summary>
    public int hp = 1;
    private int counter = 0;
    /// <summary>
    /// 区别玩家或者敌人
    /// </summary>
    public bool IsEnemy = true;
    private float _colorRemaining = 0f;
    private float _HurtSoundCooled;
    public float HurtSoundCooled = 1f;
    public AudioSource HurtSound;
    /// <summary>
    /// 对这个角色造成伤害
    /// </summary>
    /// <param name="damageCount">伤害值</param>
    /// 
 
    public void Damage(int damageCount)
    {
        if (GetComponent<Renderer>().isVisible)
        {
            if (IsEnemy == true)
                HurtSound.Play();
            else
            {
                if(canplay)
                {
                    _HurtSoundCooled = HurtSoundCooled;
                    HurtSound.Play();
                }
                CrossScenesScript.hp = hp;
            }
            hp -= damageCount;
            counter++;
            GetComponent<Renderer>().material.SetColor("_Color", new Color(168.0f / 255.0f, 0, 0, 1));
            _colorRemaining = 0.15f;
            if (hp <= 0)
                DeathEffect();
        }
    }
    public bool canplay
    {
        get { return (_HurtSoundCooled <= 0f); }
    }
    void Update()
    {
        if (_colorRemaining > 0)
        {
            _colorRemaining -= Time.deltaTime;
            if (_colorRemaining < 0)
                ResetColor();
        }
        if (_HurtSoundCooled > 0)
        {
            _HurtSoundCooled -= Time.deltaTime;
        }
    }
    /// <summary>
    /// 带击退的造成伤害，击退距离会随击退距离变化
    /// </summary>
    /// <param name="damageCount">伤害值</param>
    /// <param name="direction">击退方向（向量值的大小也可以控制击退距离）</param>
    public void DamageWithPush(int damageCount, Vector2 direction)
    {
        Damage(damageCount);
        Ultility.MyTranslate(transform, damageCount * direction * 0.3f);
    }
    private void ResetColor()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }
    private void DeathEffect()
    {
        Destroy(gameObject, 3);
        // 死了之后所有脚本都不起作用
        foreach (MonoBehaviour i in GetComponents<MonoBehaviour>())
        {
            i.enabled = false;
        }
        // 但是还是要播放死亡动画
        SpriteSheet sheet = GetComponent<SpriteSheet>();
        sheet.enabled = true;
        if (sheet.HasAnimation("death"))
            sheet.Play("death");
        // 让的尸体进入天堂位面
        gameObject.layer = LayerMask.NameToLayer("Heaven");
        // 强行让颜色恢复正常
        Invoke("ResetColor", 0.15f);
        // 调用GameObject的死亡事件
        foreach (DeathScript ds in GetComponents<DeathScript>())
        {
            ds.AfterDeath();
        }
    }
    /// <summary>
    /// 储存上次收到伤害的方向
    /// </summary>
    private int _collidedDirection = 1;
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            _collidedDirection = otherCollider.GetComponent<Rigidbody2D>().velocity.x > 0 ? 1 : -1;
            if (shot.IsEnemyShot != IsEnemy)
            {
                DamageWithPush(shot.Damage,
                    new Vector2(
                        _collidedDirection,
                        1
                    ));
                BulletEffect[] effects = shot.GetComponents<BulletEffect>();
                foreach (BulletEffect effect in effects)
                {
                    effect.Effect(gameObject);
                }
                Destroy(shot.gameObject);
            }
        }
    }

    void Start()
    {
        if (IsEnemy == false)
        {
            if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name=="bossroom")
                    hp = CrossScenesScript.hp;

        }
            
    }
}