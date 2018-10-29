using System;
using System.ComponentModel;
using UnityEngine;

public class Talker
{
    public String Name;
    public Texture2D Avatar;
    public String Direction;

    private Talk _parent;

    public Talker(Talk parent)
    {
        _parent = parent;
    }

    public void Say(System.Object line)
    {
        _parent.RegisterLine(this, line);
    }
}