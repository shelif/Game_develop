using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_s_2 : MonoBehaviour {
	public bool Option = true;
    public AudioSource click;
	// Use this for initialization
	void Start () {

	}
	private int state = 0;
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			state = 0;
			Option = true;
		} else if (Input.GetKey (KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			Option = false;
		} else if (Input.GetKeyDown (KeyCode.J) || Input.GetKey(KeyCode.Return)) {
            if (Option == true)            {
                if (Application.loadedLevelName == "startmenu")
                {
                    LoadSceneWithLoading.LoadAsync("PPT");
                    click.Play();
                }
            }
            else
            {
                if (Application.loadedLevelName == "startmenu")
                {
                    UnityEngine.Application.Quit();
                    click.Play();
                }

            }
		}
		if (!Option) {
			if (state > 3) {
				transform.rotation = Quaternion.Euler (0, 0, 1);
			} else {
				transform.rotation = Quaternion.Euler (0, 0, -1);
			}
			state++;
			if (state > 6) {
				state = 0;
			}
		}
	}
}
