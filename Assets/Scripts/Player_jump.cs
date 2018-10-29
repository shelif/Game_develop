using UnityEngine;
using System.Collections;

public class Player_jump : MonoBehaviour
{

	public float m_JumpSpeed = 400f;
	private bool m_jumping = false;
	private Rigidbody2D m_Rigidbody2D;
	private Animator m_Anim;
	//是否在地面上
	private bool grounded = true;


	private void Awake()
	{
		m_Anim = GetComponent<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey (KeyCode.UpArrow))
		{
			Jump();
		}
	}

	private void Jump()
	{
		if (this.grounded)
		{
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpSpeed));
			grounded = false;
		}
	}
	//这个时候在地面上
	private void OnCollisionEnter2D(Collision2D collision)
	{
		grounded = true;
	}
}