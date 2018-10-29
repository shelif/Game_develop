using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_move : MonoBehaviour {

	public bool option_f = true;
    // Use this for initialization
    public AudioSource click;//sounds
	void Start () {
		transform.localScale = new Vector3 (0.6f,0.6f,0.6f);
	}
	void movea(bool option){
		if(option)
			this.gameObject.transform.localPosition = new Vector3 (-0.71f, -0.112f, 0f);
		else
			this.gameObject.transform.localPosition = new Vector3 (-0.71f, -0.87f, 0f);
        
    }
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            click.Play();
            option_f = true;

        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            click.Play();
            option_f = false;
        }
        movea(option_f);
    }
}	
