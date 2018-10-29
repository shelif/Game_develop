using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWeaponScript : MonoBehaviour
{

    SpriteSheet sheet;
    Vector2 P1;
    Vector2 P2;
    public int ItemType;

    public int BulletNum = 1;
    /// <summary>
    /// 发射的扇形区域的角度
    /// </summary>
    public double SectorDegree = 45;

    public float ShootingRate = 1.0f;
    public Transform ShotPrefab;
    private float patrol_speed = 1.5f;
    public float speed = 2;
    private float _shootCooldown;
    public bool Seen = false;
    public bool IsEnemy;
    public int count = 0;
    public int stop_count = 0;
    public int stop_limit_count = 0;
    public int SeenDistance = 15;
    public int StandDistance = 5;
    private float refresh = 0.0f;
    // Use this for initialization
    void Start()
    {
        _shootCooldown = 0f;
        sheet.spriteRenderer.flipX = UnityEngine.Random.Range(1, 10) > 5;

    }

    void Awake()
    {
        sheet = GetComponent<SpriteSheet>();
        sheet.AddAnim("right", 2, 0.2);
        sheet.AddAnim("rightStop", 1, 0.2);
        sheet.AddAnim("death", 3, 0.05, true);
        sheet.AddAnim("attacked", 3, 0.2);
        stop_limit_count = UnityEngine.Random.Range(1,25);
    }

    // Update is called once per frame
    void Update()
    {
        //MoveAndShoot();

        //如果主角已经死亡就直接返回
        if (!GameObject.Find("Player")) return;
        P1 = GameObject.Find("Player").GetComponent<Transform>().position;
        P2 = GetComponent<Transform>().position;
        var jl = (P1 - P2).magnitude;
        Seen = GetComponent<Renderer>().isVisible;
        Seen = jl <= SeenDistance;
        if (Seen == true)
        {
            if (Vector2.Distance(P1, P2) > StandDistance)
            {
                if(Mathf.Abs((P1 - P2).x) > 0.05)
                {
                    if ((P1 - P2).x < 0)
                    {
                        sheet.spriteRenderer.flipX = true;
                        sheet.Play("right");
                        Ultility.MyTranslate(transform, -Vector2.right * speed * Time.deltaTime);
                        shoot(P1, P2);
                    }
                    else
                    {
                        sheet.spriteRenderer.flipX = false;
                        sheet.Play("right");
                        Ultility.MyTranslate(transform, Vector2.right * speed * Time.deltaTime);
                        shoot(P1, P2);
                    }
                }
            }
            else
            {
                if((P1 - P2).x < 0)
                {
                    sheet.spriteRenderer.flipX = true;
                    sheet.Play("right");
                }
                else
                {
                    sheet.spriteRenderer.flipX = false;
                    sheet.Play("right");
                }
                shoot(P1, P2);
            }

        }
        else
        {
            count++;
            if (count <= 1.0f / Time.deltaTime)
            {
                if (sheet.spriteRenderer.flipX == false)
                {
                    sheet.Play("right");
                    Ultility.MyTranslate(transform, Vector2.right * patrol_speed * Time.deltaTime);
                }
                else
                {
                    sheet.Play("right");
                    Ultility.MyTranslate(transform, -Vector2.right * patrol_speed * Time.deltaTime);
                }
            }
            else
            {

                stop_count++;
                if (stop_count == stop_limit_count)
                {

                    sheet.spriteRenderer.flipX = !sheet.spriteRenderer.flipX;
                    sheet.Play("rightStop");
                    stop_count = 0;
                    count = 0;
                }

            }

        }


        if (_shootCooldown > 0)
        {
            _shootCooldown -= Time.deltaTime;
        }

    }
    void shoot(Vector2 from, Vector2 to)
    {
        //Debug.Log("check");
        //getAngle(from,to);
        Vector2 direction = from - to;
        Attack(direction);
    }
    public void Attack(Vector2 direction)
    {
        if (CanAttack)
        {
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
        }
    }
    /// <summary>
    /// 发射单个子弹
    /// </summary>
    /// <param name="direction">子弹的方向</param>
    private void ProjectBullet(Vector2 direction)
    {
        var shotTransform = Instantiate(ShotPrefab) as Transform;
        shotTransform.position = transform.position;
        // 根据发射角度旋转子弹图像
        shotTransform.rotation = Quaternion.Euler(0, 0, (float)(Math.Atan2(direction.y, direction.x) / Math.PI * 180));
        ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            shot.IsEnemyShot = IsEnemy;
        }
        // 设定子弹运行方向
        MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
        if (move != null)
        {
            move.Direction = direction;
        }
    }
    public bool CanAttack
    {
        get { return _shootCooldown <= 0f; }
    }
}
