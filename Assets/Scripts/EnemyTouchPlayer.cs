using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouchPlayer : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<HealthScript>().DamageWithPush(
                1,
                new Vector2(
                    transform.position.x - collision.transform.position.x > 0 ? -1 : 1,
                    1.0f
                )
            );
        }
    }
}