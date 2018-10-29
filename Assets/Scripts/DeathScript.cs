using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用来定义死亡事件的一个类
/// 使用方法： 继承这个类，重写AfterDeath函数，然后把子类拖到对应的GameObject上去
/// </summary>
public abstract class DeathScript : MonoBehaviour
{

    public abstract void AfterDeath();
}
