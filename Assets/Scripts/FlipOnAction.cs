using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlipOnAction : MonoBehaviour
{
    public abstract void Next();

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.J)
            || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            Next();
        }
    }
}