using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_cartoon : MonoBehaviour {
	public float speed = 0.01f;
	public bool m_jumping = false;
	public bool grounded = false;
	public float m_JumpSpeed = 400f;
	private Rigidbody2D m_Rigidbody2D;
    public int DestroyTime = 3;
	private Animator m_Anim;
	private int down = -1;
	// Use this for initialization
	private void Awake()
	{
		sheet = GetComponent<SpriteSheet>();

		sheet.AddAnim("right", 2,0.5);
		sheet.AddAnim("rightStop", 1);
		sheet.AddAnim("shoot", 3,0.2);
		sheet.AddAnim("Jump", 2, 0.2, true);
	}
	void Start () {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        DestroyObject(gameObject,DestroyTime);
	}
	SpriteSheet sheet;
	// Update is called once per frame

	void Update () {
		
	}

	private void Jump()
	{

		if (this.grounded && !this.m_jumping) {
			m_jumping = true;
			transform.Translate (-Vector2.right * speed * Time.deltaTime / 2);
			sheet.Play ("Jump");
			m_Rigidbody2D.AddForce (new Vector2 (2f, m_JumpSpeed));
			//grounded = false;

		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		down++;
		if (GetComponent<Rigidbody2D>().velocity.y <= 0.01 && down > 0 )
		{
			m_jumping = false;
			grounded = true;
		}
		sheet.Play("rightStop");
		Jump ();
	}
	
	void FixedUpdate()
	{
		sheet.Play ("right");
		transform.Translate (Vector2.right * speed * Time.deltaTime / 10);
	}
}
