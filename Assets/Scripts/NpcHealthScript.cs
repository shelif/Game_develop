using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHealthScript : HealthScript
{

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            if (shot.IsEnemyShot != IsEnemy)
            {
                int wt = GameObject.Find("Player").GetComponent<WeaponScript>().WeaponType;

                switch (wt)
                {
                    case 1:
                        Damage(shot.Damage);
                        break;
                    case 2:
                        Damage(shot.Damage);
                        NpcWeaponScript npc_speed_control = GetComponent<NpcWeaponScript>();
                        npc_speed_control.speed /= 2;
                        SetTimeOut(2000, () => { npc_speed_control.speed *= 2; });
                        break;
                    case 3:
                        Damage(shot.Damage * 2);
                        break;


                }
                Destroy(shot.gameObject);
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

}