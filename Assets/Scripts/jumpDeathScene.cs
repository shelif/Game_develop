using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class jumpDeathScene : DeathScript
{
    private int hp;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    public override void AfterDeath()
    {
        PlayerSoulDeathEffect soulEffect = GetComponent<PlayerSoulDeathEffect>();
        if (soulEffect)
            Invoke("JumpScene", soulEffect.AnimationPrefab.GetComponent<SoulScript>().AnimationTime * 0.8f);
        else
            JumpScene();
    }

    public void JumpScene()
    {
        if (Application.loadedLevelName == "bossroom")
            LoadSceneWithLoading.Load("talktest");
        else
            LoadSceneWithLoading.LoadAsync("gameover");
    }
}