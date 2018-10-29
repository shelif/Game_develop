using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTalk3 : Talk
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
        Left.Say("克拉迪克？怎么是你！");
        Right.Say("提莫！"+ "\r\n"+"放弃吧……你是不可能打败布拉热的！");
        Left.Say("迪克……"+"\r\n"+"不！我不可能就此罢休的！");
        Right.Say("对不起了……我一定会阻止你的。");
        Left.Say("来吧！");

    }

    // W option为True S 或者其他 option 为False
    public override void TalkEnd(bool option)
    {
        LoadSceneWithLoading.Load("bossroom");
    }
}