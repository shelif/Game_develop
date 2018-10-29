using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HdmapGotoscene2 : MonoBehaviour {
    public GameObject colliders;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter2D(Collision2D collisions)//碰撞进入
    {
        if (collisions.gameObject.name == colliders.name)
        {

            if (Input.GetKey(KeyCode.D))
            {
                LoadSceneWithLoading.LoadAsync("scene2");
            }
        }
    }
}
