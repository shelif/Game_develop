using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {
    /// <summary>
    /// 物品的属性编号
    /// </summary>
    public int item_no;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 手动添加重力效果
    void FixedUpdate()
    {
        bool onGround = false;
        foreach (RaycastHit2D info in Physics2D.RaycastAll(transform.position, Vector2.down, transform.GetComponent<Renderer>().bounds.size.y / 2))
        {
            if (info.collider.gameObject.layer == LayerMask.NameToLayer("Block") &&
                !info.collider.isTrigger)
            {
                onGround = true;
                break;
            }
        }

        Rigidbody2D rigidComponent = GetComponent<Rigidbody2D>();
        if (onGround)
        {
            rigidComponent.velocity = Vector2.zero;
        }
        else
        {
            rigidComponent.AddForce(Physics2D.gravity * Time.fixedDeltaTime * 4, ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.tag == "Player")
        {
            GameObject.Find("Player").PlaySound("DropItemSound");
            Destroy(this.gameObject);
            //item_no = GetItemNO();
            switch (item_no)
            {
                case 1:
                    SpeedUp(col);
                    break;

                case 5:
                    RecoverHp(col);
                    break;
                case 2:
                    FrozenWeapon(col);
                    break;
                case 3:
                    BlazeWeapon(col);
                    break;
                case 4:
                    CircleShoot(col);
                    break;
                case 6:
                    AdvancedJump(col);
                    break;
            }
        }
        
    }


    void SetTimeOut(double interval, System.Action act)
    {
        System.Timers.Timer tmr = new System.Timers.Timer();
        tmr.Interval = interval;
        tmr.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs args)
        {
            act();
            tmr.Stop();
        };
        tmr.Start();
    }
    /*
    int GetItemNO()
    {
        NpcWeaponScript Npcw = GetComponent<NpcWeaponScript>();
        return Npcw.ItemType;
    }
    */
    void SpeedUp(Collider2D col)
    {
        Player_remove player_speed_control = col.gameObject.GetComponent<Player_remove>();


        if (player_speed_control != null)
        {
            //Debug.Log(player_speed_control);
            player_speed_control.speed = 10;
            SetTimeOut(5000, () => {player_speed_control.speed = 5; });
        }
    }



    void RecoverHp(Collider2D col)
    {
        HealthScript player_health_control = col.gameObject.GetComponent<HealthScript>();
        player_health_control.hp += 3;
    }

    void FrozenWeapon(Collider2D col)
    {
        foreach (WeaponScript player_weapon_control in col.gameObject.GetComponentsInChildren<WeaponScript>())
        {
            player_weapon_control.WeaponType = 2;
            SetTimeOut(5000, () =>
            {
                if (player_weapon_control.WeaponType == 2) player_weapon_control.WeaponType = 1;
            });
        }
    }

    void BlazeWeapon(Collider2D col)
    {
        foreach (WeaponScript player_weapon_control in col.gameObject.GetComponentsInChildren<WeaponScript>())
        {
            player_weapon_control.WeaponType = 3;
            SetTimeOut(5000, () =>
            {
                if (player_weapon_control.WeaponType == 3) player_weapon_control.WeaponType = 1;
            });
        }
    }

    void CircleShoot(Collider2D col)
    {
        foreach (WeaponScript player_weapon_control in col.gameObject.GetComponentsInChildren<WeaponScript>())
        {
            player_weapon_control.WeaponType = 1;
            player_weapon_control.BulletNum = 5;
            player_weapon_control.SectorDegree = 180;
            SetTimeOut(5000, () =>
            {
                player_weapon_control.BulletNum = 1;
                player_weapon_control.SectorDegree = 45;
            });
        }
    }

    void AdvancedJump(Collider2D col)
    {
        Player_remove player_move_control = col.gameObject.GetComponent<Player_remove>();
        player_move_control.m_JumpSpeed *= 2;
        SetTimeOut(5000, () => { player_move_control.m_JumpSpeed /= 2; });
    }

}
