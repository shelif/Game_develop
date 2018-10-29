using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Success : DeathScript {
    // Use this for initialization
    private int hp;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      

    }
    public override void AfterDeath()
    {
        Invoke("Jump", 2.4f);
    }

    public void Jump()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name== "Level2_1")
            LoadSceneWithLoading.LoadAsync("Ending1");
        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name=="Level_2")
            LoadSceneWithLoading.LoadAsync("Ending2");
    }
}
