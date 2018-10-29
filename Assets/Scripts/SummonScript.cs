using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonScript : MonoBehaviour {

    private int hp;
    private int full_hp;
    private GameObject npc_object;
    private GameObject weapon_object;
    private int flag = 0;
	// Use this for initialization
	void Start () {
        HealthScript boss_hp_control = GetComponent<HealthScript>();
        full_hp = boss_hp_control.hp;
	    NpcWeaponScript npc_weapon_controler = GetComponent<NpcWeaponScript>();
	    npc_weapon_controler.ShotPrefab = Resources.Load("Sprite/Boss Bullet Normal", typeof(Transform)) as Transform;
	}
	
	// Update is called once per frame
	void Update () {
        HealthScript boss_hp_control = GetComponent<HealthScript>();
        hp = boss_hp_control.hp;

        if (full_hp - hp == 25&&flag==0)
        {
            
            for (int i = 0; i < 5; i++)
            {
                Vector2 right_bottom = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
                Vector2 left_top = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
                float x1 = right_bottom.x; float x2 = left_top.x;
                npc_object = Instantiate(Resources.Load("Sprite/Enemy")) as GameObject;

                npc_object.transform.position = new Vector2(Random.Range(x1,x2), right_bottom.y);

                // 全屏追踪玩家
                npc_object.GetComponent<NpcWeaponScript>().SeenDistance = 1000;
            }
            
            NpcWeaponScript npc_weapon_controler = GetComponent<NpcWeaponScript>();
            npc_weapon_controler.ShotPrefab = Resources.Load("Sprite/Boss Bullet Blaze", typeof(Transform)) as Transform;
            flag++;
        }
        if(full_hp- hp == 50 && flag == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 right_bottom = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
                Vector2 left_top = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
                float x1 = right_bottom.x; float x2 = left_top.x;
                npc_object = Instantiate(Resources.Load("Sprite/Enemy")) as GameObject;

                npc_object.transform.position = new Vector2(Random.Range(x1, x2), right_bottom.y);

                npc_object.GetComponent<NpcWeaponScript>().SeenDistance = 1000;
            }

            NpcWeaponScript npc_weapon_controler = GetComponent<NpcWeaponScript>();
            npc_weapon_controler.ShotPrefab = Resources.Load("Sprite/Boss Bullet Frozen", typeof(Transform)) as Transform;
            flag++;
        }
        if (full_hp - hp == 75 && flag == 2)
        {
            
            for (int i = 0; i < 5; i++)
            {
                Vector2 right_bottom = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
                Vector2 left_top = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
                float x1 = right_bottom.x; float x2 = left_top.x;
                npc_object = Instantiate(Resources.Load("Sprite/Enemy")) as GameObject;

                npc_object.transform.position = new Vector2(Random.Range(x1, x2), right_bottom.y);

                npc_object.GetComponent<NpcWeaponScript>().SeenDistance = 1000;
            }
            
            NpcWeaponScript npc_weapon_controler = GetComponent<NpcWeaponScript>();
            npc_weapon_controler.BulletNum = 10;
            npc_weapon_controler.SectorDegree = 180;
            npc_weapon_controler.ShotPrefab = Resources.Load("Sprite/Boss Bullet Normal", typeof(Transform)) as Transform;
            flag++;
        }

        if (full_hp - hp == 85 && flag == 3)
        {

            for (int i = 0; i < 5; i++)
            {
                Vector2 right_bottom = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
                Vector2 left_top = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
                float x1 = right_bottom.x; float x2 = left_top.x;
                npc_object = Instantiate(Resources.Load("Sprite/Enemy")) as GameObject;

                npc_object.transform.position = new Vector2(Random.Range(x1, x2), right_bottom.y);

                npc_object.GetComponent<NpcWeaponScript>().SeenDistance = 1000;
            }

            NpcWeaponScript npc_weapon_controler = GetComponent<NpcWeaponScript>();
            npc_weapon_controler.BulletNum = 2;
            npc_weapon_controler.SectorDegree = 60;
            npc_weapon_controler.ShotPrefab = Resources.Load("Sprite/Enemy Bullet Track", typeof(Transform)) as Transform;
            flag++;
        }

    }
}
