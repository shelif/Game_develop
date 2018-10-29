using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Talk : FlipOnAction
{
    public abstract void Setup();

    public virtual void TalkEnd(bool option)
    {
    }

    struct Line
    {
        public Talker Talker;
        public System.Object Content;

        public Line(Talker talker, System.Object content)
        {
            this.Talker = talker;
            this.Content = content;
        }
    }

    public void Awake()
    {
        Setup();
        _style = GUIStyle.none;
        _style.fontSize = 20;
    }

    private List<Line> _lines = new List<Line>();

    public void RegisterLine(Talker talker, System.Object line)
    {
        _lines.Add(new Line(talker, line));
    }

    public Talker CreateTalker(String name, Texture2D avatar, String direction)
    {
        var talker = new Talker(this);
        talker.Name = name;
        talker.Avatar = avatar;
        talker.Direction = direction;

        return talker;
    }

    private Rect AvatarRect(bool isDirectionLeft)
    {
        Rect avatarRect = new Rect();
        avatarRect.yMin = Camera.main.ViewportToScreenPoint(new Vector3(0, 0.25f)).y;
        avatarRect.yMax = Camera.main.ViewportToScreenPoint(new Vector3(0, 0.95f)).y;

        if (isDirectionLeft)
        {
            avatarRect.xMin = Camera.main.ViewportToScreenPoint(new Vector3(0.05f, 0)).x;
            avatarRect.xMax = Camera.main.ViewportToScreenPoint(new Vector3(0.35f, 0)).x;
        }
        else
        {
            avatarRect.xMin = Camera.main.ViewportToScreenPoint(new Vector3(0.65f, 0)).x;
            avatarRect.xMax = Camera.main.ViewportToScreenPoint(new Vector3(0.95f, 0)).x;
        }

        return avatarRect;
    }

    private Rect TextRect(bool isDirectionLeft)
    {
        Rect textRect = new Rect();

        if (isDirectionLeft)
        {
            textRect.min = Camera.main.ViewportToScreenPoint(new Vector3(0.40f, 0.60f));
            textRect.max = Camera.main.ViewportToScreenPoint(new Vector3(0.90f, 0.90f));
        }
        else
        {
            textRect.min = Camera.main.ViewportToScreenPoint(new Vector3(0.10f, 0.60f));
            textRect.max = Camera.main.ViewportToScreenPoint(new Vector3(0.60f, 0.90f));
        }

        return textRect;
    }

    private Rect TalkBoxRect()
    {
        Rect talkboxRect = new Rect();
        talkboxRect.min = Camera.main.ViewportToScreenPoint(new Vector3(0, 0.5f));
        talkboxRect.max = Camera.main.ViewportToScreenPoint(new Vector3(1, 1));

        return talkboxRect;
    }

    public override void Next()
    {
        if (_lines.Count > 0)
        {
            if (_lines.Count == 1)
                TalkEnd(Input.GetKeyDown(KeyCode.W));
            else
                _lines.RemoveAt(0);
        }
    }

    private GUIStyle _style;

    public void OnGUI()
    {
        if (_lines.Count > 0)
        {
            Line l = _lines[0];
            GUI.DrawTexture(TalkBoxRect(), Resources.Load("transition/talkbox") as Texture2D);
            GUI.DrawTexture(AvatarRect(l.Talker.Direction == "Left"), l.Talker.Avatar, ScaleMode.StretchToFill);
            if (l.Content is String)
            {
                GUI.Box(TextRect(l.Talker.Direction == "Left"), l.Content as String, GUIStyle.none);
            }
            else if (l.Content is Texture2D)
            {
                GUI.DrawTexture(TextRect(l.Talker.Direction == "Left"), l.Content as Texture2D, ScaleMode.ScaleToFit);
            }
        }
    }
}