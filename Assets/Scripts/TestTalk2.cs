using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTalk2 : Talk
{
    public Texture2D LeftAvatar;
    public Texture2D RightAvatar;
    private bool option = true;

    public override void Setup()
    {
        //GameObject.Find ("s1").GetComponent<Renderer>().material.SetColor("_Color",new Color (1,1,1,0));
        //GameObject.Find ("s2").GetComponent<Renderer>().material.SetColor("_Color",new Color (1,1,1,0));
        Talker Left = base.CreateTalker("提莫", LeftAvatar, "Left");
        Talker Right = base.CreateTalker("克拉迪克", RightAvatar, "Right");
        Right.Say("……");
        Left.Say("……");
        Right.Say("那好，朋友，这是最后一次了。" + "\r\n" + "我将你引入布拉热的房间，后面的事情……唉……");
    }

    // W option为True S 或者其他 option 为False
    public override void TalkEnd(bool option)
    {
        LoadSceneWithLoading.Load("Level_2");
    }
}