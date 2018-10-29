using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBGM : MonoBehaviour
{

    /// <summary>
    /// 用来更换的BGM
    /// </summary>
    public AudioSource NewBGM;

    private bool _firstBeenSeen = true;

    void OnBecameVisible()
    {
        if (_firstBeenSeen)
        {
            _firstBeenSeen = false;
            NewBGM.Play();
        }
    }
}
