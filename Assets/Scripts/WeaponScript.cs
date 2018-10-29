using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 使得GameObject具有能够发射子弹的功能
/// 子弹应该具有ShotScript和MoveScript这两个Component
/// </summary>
public class WeaponScript : MonoBehaviour
{
    /// <summary>
    /// 子弹的类别
    /// </summary>summary>
    /// 
    public int WeaponType = 1;

    public AudioSource shootsound;

    public float Recoil = 1.2f;
    //public GameObject Wp;

    /// <summary>
    /// 子弹的形状
    /// </summary>
    public GameObject shotobject;

    /// <summary>
    /// 发射的冷却时间
    /// </summary>
    public float ShootingRate = 0.1f;

    private float _shootCooldown;

    // Use this for initialization
    void Start()
    {
        _shootCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shootCooldown > 0)
        {
            _shootCooldown -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 是否属于敌人
    /// </summary>
    public bool IsEnemy;

    /// <summary>
    /// 一次发射子弹的数目
    /// </summary>
    public int BulletNum = 1;

    /// <summary>
    /// 发射的扇形区域的角度
    /// </summary>
    public double SectorDegree = 45;

    /// <summary>
    /// 发射后的烟尘效果
    /// </summary>
    public Transform SmokeEffect;

    /// <summary>
    /// 发射子弹
    /// </summary>
    /// <param name="direction">发射的方向</param>
    public void Attack(Vector2 direction)
    {
        if (CanAttack)
        {
            // shootsound.Play();
            gameObject.PlaySound("shooting");
            // 设置冷却
            _shootCooldown = ShootingRate;

            // 转化角度值为弧度值
            double sectorDegree = SectorDegree / 180 * Math.PI;
            // 火力扇形区域中心
            double centerDegree = Math.Atan2(direction.y, direction.x);
            // 每两个子弹之间的夹角
            double deltaDegree = sectorDegree / BulletNum;
            // 扇形区域开始角度
            double sectorStartDegree = centerDegree - sectorDegree / BulletNum / 2 * (BulletNum - 1);

            for (int i = 0; i < BulletNum; i++)
            {
                double projectDegree = sectorStartDegree + i * deltaDegree;
                ProjectBullet(new Vector2((float)Math.Cos(projectDegree), (float)Math.Sin(projectDegree)));
            }

            // 后坐力
            MoveScript move = shotobject.GetComponent<MoveScript>();
            Rigidbody2D rigidComponent = GetComponent<Rigidbody2D>();
            // 应对单独使用GameObject来对其枪口的用法
            // 这种情况下后坐力应该施加在它的父对象上
            if (!rigidComponent) rigidComponent = transform.parent.GetComponent<Rigidbody2D>();
            if (rigidComponent)
                rigidComponent.AddForce(-move.Direction * Recoil, ForceMode2D.Impulse);

            // 烟尘粒子效果
            if (SmokeEffect)
            {
                Transform smoke = Instantiate(SmokeEffect);
                smoke.position = transform.position;
            }
        }
    }

    /// <summary>
    /// 用于随机微调子弹方向
    /// </summary>
    /// <param name="direction">原方向</param>
    /// <returns>新方向</returns>
    private Vector2 ShakeDirection(Vector2 direction)
    {
        double rotation = Math.Atan2(direction.y, direction.x);
        rotation += (UnityEngine.Random.value - 0.5) / 10;
        return new Vector2(
            (float)(direction.magnitude * Math.Cos(rotation)),
            (float)(direction.magnitude * Math.Sin(rotation))
        );
    }

    /// <summary>
    /// 发射单个子弹
    /// </summary>
    /// <param name="direction">子弹的方向</param>
    private void ProjectBullet(Vector2 direction)
    {
        direction = ShakeDirection(direction);
        WeaponSprite(WeaponType);
        shotobject.transform.position = transform.position;
        // 根据发射角度旋转子弹图像
        shotobject.transform.rotation =
            Quaternion.Euler(0, 0, (float)(Math.Atan2(direction.y, direction.x) / Math.PI * 180));

        ShotScript shot = shotobject.GetComponent<ShotScript>();
        if (shot != null)
        {
            shot.IsEnemyShot = IsEnemy;
        }

        // 设定子弹运行方向
        MoveScript move = shotobject.GetComponent<MoveScript>();
        if (move != null)
        {
            move.Direction = direction;
        }
    }

    /// <summary>
    /// 是否已经能够进行下一次发射
    /// </summary>
    public bool CanAttack
    {
        get { return _shootCooldown <= 0f; }
    }


    /// <summary>
    /// 选择子弹精灵种类
    /// </summary>
    void WeaponSprite(int wt)
    {
        // Debug.Log("check");
        switch (wt)
        {
            case 1:
                shotobject = Instantiate(Resources.Load("Sprite/Player Bullet")) as GameObject;
                break;
            case 2:
                shotobject = Instantiate(Resources.Load("Sprite/Enemy Bullet Frozen")) as GameObject;
                break;
            case 3:
                shotobject = Instantiate(Resources.Load("Sprite/Enemy Bullet Blaze")) as GameObject;
                break;
            case 4:
                shotobject = Instantiate(Resources.Load("Sprite/Enemy Bullet Track")) as GameObject;
                break;
            default:
                break;
        }
    }
}