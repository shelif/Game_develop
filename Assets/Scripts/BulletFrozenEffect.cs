using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFrozenEffect : BulletEffect
{
    public override void Effect(GameObject target)
    {
        if (target.tag == "Player")
        {
            Player_remove player_speed_control = target.GetComponent<Player_remove>();
            player_speed_control.speed = 2.5f;
            Ultility.SetTimeOut(3000, () => { player_speed_control.speed = 5f; });
        }
        else
        {
            NpcWeaponScript npc_speed_control = target.GetComponent<NpcWeaponScript>();
            npc_speed_control.speed /= 2;
            Ultility.SetTimeOut(3000, () => { npc_speed_control.speed *= 2; });
        }
    }


}