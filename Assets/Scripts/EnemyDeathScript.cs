using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathScript : DeathScript
{
    GameObject itemobject;
    public AudioSource DropItemSound;
    public override void AfterDeath()
    {
        //Debug.Log("I am Dead");
        //Debug.Log(transform.position);

        ///<summary>
        ///记录分数
        ///</summary>
        MarkBarScript mark_add_control = GameObject.FindGameObjectWithTag("MarkBar").GetComponent<MarkBarScript>();
        mark_add_control.Mark += 1;
        CrossScenesScript.mark += 1;


        ///<summary>
        ///随机掉落宝物
        ///</summary>
        if (Random.value < 0.30f)
        {
            //DropItemSound.Play();

            int r2 = Random.Range(1, 6);
            Vector2 npc_position = GetComponent<Transform>().position;
            switch (r2)
            {
                case 1:
                    itemobject = Instantiate(Resources.Load("Sprite/SpeedUp")) as GameObject;
                    itemobject.transform.position = npc_position;
                    itemobject.GetComponent<ItemScript>().item_no = 1;
                    break;
                case 2:
                    itemobject = Instantiate(Resources.Load("Sprite/Bullet Frozen")) as GameObject;
                    itemobject.transform.position = npc_position;
                    itemobject.GetComponent<ItemScript>().item_no = 2;
                    break;
                case 3:
                    itemobject = Instantiate(Resources.Load("Sprite/Bullet Blaze")) as GameObject;
                    itemobject.transform.position = npc_position;
                    itemobject.GetComponent<ItemScript>().item_no = 3;
                    break;

                case 4:
                    itemobject = Instantiate(Resources.Load("Sprite/Circle Shot")) as GameObject;
                    itemobject.transform.position = npc_position;
                    itemobject.GetComponent<ItemScript>().item_no = 4;
                    break;

                case 5:
                    itemobject = Instantiate(Resources.Load("Sprite/RecoverHp")) as GameObject;
                    itemobject.transform.position = npc_position;
                    itemobject.GetComponent<ItemScript>().item_no = 5;
                    break;
                case 6:
                    itemobject = Instantiate(Resources.Load("Sprite/Adavanced Jump")) as GameObject;
                    itemobject.transform.position = npc_position;
                    itemobject.GetComponent<ItemScript>().item_no = 6;
                    break;
                default:
                    itemobject = Instantiate(Resources.Load("Sprite/Item")) as GameObject;
                    itemobject.transform.position = npc_position;
                    itemobject.GetComponent<ItemScript>().item_no = 1;
                    break;
            }
            
            // 凋落物的上拋效果
            if (itemobject)
            {
                Rigidbody2D rigidComponent = itemobject.GetComponent<Rigidbody2D>();
                rigidComponent.AddForce(
                    new Vector2(Random.value, 1) * 10,
                    ForceMode2D.Impulse
                );
            }
            
        }
    }
}