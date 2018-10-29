using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTalk : Talk
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
        Left.Say("迪克，你真的要这样为虎作伥吗？" + "\r\n" + "你为什么要这样眼睁睁看着比·布拉热那家伙为非作歹？");
        Right.Say("提莫，我才是要问你，你简直就是疯了！" + "\r\n" + "你知道你一路打到这里是怎样的后果吗？");
        Left.Say("我知道！可我不能眼睁睁的看着人们变为思想上的奴隶！");
        Right.Say("奴隶？这怎么能叫奴隶！" + "\r\n" + "我们积极引导人们，给他们灌输正确的思想，" + "\r\n" +
            "抵制世界的阴暗面，这难道不对吗？");
        //Left.Say(Resources.Load("selection"));
        Left.Say("使用W和S选择：" + "\r\n" + "W:  当然不对！人类应该依靠自己明辨是非" + "\r\n" + "      而不应该让自己无知得让他人来灌输善恶！"
            + "\r\n\r\n\r\n"
            + "S:   布拉热确是在把人们引入歧途！" + "\r\n" + "      我一定要打败他,重新将人们引入正轨！");

    }

    // W option为True S 或者其他 option 为False
    public override void TalkEnd(bool option)
    {
        
        if (option)
            LoadSceneWithLoading.LoadAsync("talktest1");
        else
            LoadSceneWithLoading.LoadAsync("talktest2");
    }
}