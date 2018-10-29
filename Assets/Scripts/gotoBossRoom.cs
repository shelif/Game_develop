using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotoBossRoom : MonoBehaviour
{
    public GameObject colliders;

    void OnCollisionEnter2D(Collision2D collisions) //碰撞进入
    {
        if (Application.loadedLevelName == "Level1")
        {
            if (collisions.gameObject.name == colliders.name)
            {
                LoadSceneWithLoading.Load("talktest3");
            }
        }
        else if (Application.loadedLevelName == "Guidence")
        {
            if (collisions.gameObject.name == colliders.name)
            {
                LoadSceneWithLoading.Load("Level1");
            }
        }
    }
}