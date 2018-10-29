using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Camera_Move : MonoBehaviour
{
    /// <summary>
    /// 摄像机跟随目标
    /// </summary>
    public Transform Player;

    /// <summary>
    /// 用于约束摄像机可视范围的碰撞体
    /// </summary>
    public GameObject Background;

    /// <summary>
    /// 摄像机跟随的速度
    /// </summary>
    public float Speed = 1.0f;

    /// <summary>
    /// 还要保持多少时间的特写时间
    /// </summary>
    private static float _closeLookRemaining = 0f;

    private static GameObject _closeLookTarget;

    // 对目标进行一段时间的特写效果
    public static void CloseLook(GameObject target)
    {
        _closeLookRemaining = 1.0f;
        _closeLookTarget = target;
    }

    /// <summary>
    /// 设定相机的追踪目标
    /// </summary>
    /// <returns>追踪目标的坐标</returns>
    private Vector3 TargetPos()
    {
        Vector3 targetPos;
        if (_closeLookRemaining > 0)
        {
            _closeLookRemaining -= Time.deltaTime;
            targetPos = _closeLookTarget.transform.position;
        }
        else
        {
            targetPos = Player.position;
        }
        return targetPos;
    }

    void Update()
    {
        Rect screenRect = new Rect();
        screenRect.min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        screenRect.max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector3 targetPos = TargetPos();

        // 相机偏移
        Player_remove player_remove = Player.GetComponent<Player_remove>();
        float offsetDistance = Camera.main.ViewportToWorldPoint(new Vector3(0.10f, 0)).x
                               - Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
        if (player_remove.dir == Player_remove.dirfront)
        {
            targetPos.x += offsetDistance;
        }
        else
        {
            targetPos.x -= offsetDistance;
        }

        // 弹簧相机效果
        Vector3 stepPos = Vector3.Lerp(transform.position, targetPos, Speed * Time.deltaTime);

        Rect newScreenRect = new Rect(screenRect);
        newScreenRect.position += (Vector2) (stepPos - transform.position);

        Rect backgroundRect = new Rect();
        backgroundRect.min = Background.transform.position - Background.GetComponent<Collider2D>().bounds.size / 2;
        backgroundRect.max = Background.transform.position + Background.GetComponent<Collider2D>().bounds.size / 2;

        Vector3 newPosition = stepPos;

        if (newScreenRect.xMin <= backgroundRect.xMin)
            newPosition.x = backgroundRect.xMin + screenRect.width / 2;
        else if (newScreenRect.xMax >= backgroundRect.xMax)
            newPosition.x = backgroundRect.xMax - screenRect.width / 2;

        if (newScreenRect.yMin <= backgroundRect.yMin)
            newPosition.y = backgroundRect.yMin + screenRect.height / 2;
        else if (newScreenRect.yMax >= backgroundRect.yMax)
            newPosition.y = backgroundRect.yMax - screenRect.height / 2;

        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}