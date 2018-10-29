using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkScript : MonoBehaviour
{
    /// <summary>
    /// 闪烁间隔
    /// </summary>
    public float Interval = 1.0f;

    private float _remainingInterval;

    private bool _visible;

    void Start()
    {
        _remainingInterval = Interval;
        _visible = true;
    }

    void Update()
    {
        _remainingInterval -= Time.deltaTime;
        if (_remainingInterval < 0)
        {
            _remainingInterval = Interval;
            _visible = !_visible;
            if (_visible)
            {
                GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1, 1));
            }
            else
            {
                GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 1, 1, 0));
            }
        }
    }
}