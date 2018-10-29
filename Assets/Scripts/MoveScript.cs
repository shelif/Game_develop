using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 给GameObject添加移动的功能
/// </summary>
public class MoveScript : MonoBehaviour
{
    /// <summary>
    /// 移动速度
    /// </summary>
    public Vector2 Speed = new Vector2(10, 10);

    /// <summary>
    /// 移动的方向
    /// </summary>
    public Vector2 Direction = new Vector2(-1, 0);

    private Vector2 _movement;
    private Rigidbody2D _rigidbodyComponent;

    void Update()
    {
        _movement = new Vector2(
            Speed.x * Direction.normalized.x,
            Speed.y * Direction.normalized.y);
    }

    void FixedUpdate()
    {
        if (_rigidbodyComponent == null)
            _rigidbodyComponent = GetComponent<Rigidbody2D>();

        _rigidbodyComponent.velocity = _movement;
    }
}