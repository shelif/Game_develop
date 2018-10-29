using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCloseLook : MonoBehaviour
{
    private bool _firstSeen = true;

    void OnBecameVisible()
    {
        if (_firstSeen)
        {
            Camera_Move.CloseLook(gameObject);
        }
        _firstSeen = false;
    }
}