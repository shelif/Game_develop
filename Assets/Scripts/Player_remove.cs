using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_remove : MonoBehaviour
{
    public float m_JumpSpeed = 400f;
    public bool m_jumping = false;
    private Rigidbody2D m_Rigidbody2D;

    public AudioSource jumpsound;
    private Animator m_Anim;

    //是否在地面上
    private bool grounded = true;

    public const int dirfront = 0;
    public const int dirback = 1;
    public int dir; //方向

   

    //前一次跳跃是否完成
    // Use this for initialization
    SpriteSheet sheet;

    public float speed = 5;

    private void Awake()
    {
        sheet = GetComponent<SpriteSheet>();
        sheet.AddAnim("right", 10, 1);
        sheet.AddAnim("rightStop", 1);
        sheet.AddAnim("Jump", 5, 1, true);

        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        SetFlip(sheet.spriteRenderer.flipX);
    }

    void Start()
    {
    }

    private void SetFlip(bool isFliped)
    {
        sheet.spriteRenderer.flipX = isFliped;
        transform.Find("Gun").localPosition = new Vector3(
            isFliped ? -0.882f : 0.882f,
            0.452f
        );

        dir = isFliped? dirback : dirfront;
    }

    // Update is called once per frame
    void OnGUI()
    {
        bool shoot = Input.GetKey(KeyCode.J);
        if (shoot)
        {
            // shootsound.Play();
            WeaponScript weapon = GetComponentInChildren<WeaponScript>();
            if (weapon != null)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    weapon.Attack(new Vector2(1, 0));
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    weapon.Attack(new Vector2(-1, 0));
                }

                else if (dir == dirfront)
                    weapon.Attack(new Vector2(1, 0));
                else
                    weapon.Attack(new Vector2(-1, 0));
            }
        }
    }


    private void Jump()
    {
        if (this.grounded && !this.m_jumping)
        {
            m_jumping = true;
            sheet.Play("Jump");
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpSpeed));

            //audio = GetComponent<AudioSource>().audio;
            jumpsound.Play();

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Ultility.MyTranslate(transform, -Vector2.right * speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Ultility.MyTranslate(transform, -Vector2.right * speed * Time.deltaTime);
            }
            grounded = false;
        }
    }

    //这个时候在地面上
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<Rigidbody2D>().velocity.y <= 0.01)
        {
            m_jumping = false;
            grounded = true;
        }
        sheet.Play("rightStop");
        sheet.Play("rightStop");
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.A))
        {
            SetFlip(true);
            if (!m_jumping)
                sheet.Play("right");

            Ultility.MyTranslate(transform, -Vector2.right * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SetFlip(false);
            if (!m_jumping)
                sheet.Play("right");
            Ultility.MyTranslate(transform, Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            if (m_jumping)
                sheet.Play("Jump");
            else
                sheet.Play("rightStop");
        }
    }
}