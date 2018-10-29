using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathAnimation : DeathScript
{
    /// <summary>
    /// Boss死亡时的动作
    /// </summary>
    public Sprite DeathSprite;

    /// <summary>
    /// 爆炸火焰效果
    /// </summary>
    public Transform Boom;

    /// <summary>
    /// 动画持续时间
    /// </summary>
    public float LifeTime = 5.0f;

    /// <summary>
    /// 两次小爆炸之间的间隔
    /// </summary>
    public float BoomInterval = 0.1f;

    private float _startTime;

    private GameObject _deathSprite;

    private void CastBoom(Vector3 position)
    {
        Transform boom = Instantiate(Boom);
        boom.position = position;
        ShakeCamera();
    }

    /// <summary>
    /// 镜头晃动效果
    /// </summary>
    private void ShakeCamera()
    {
        GameObject camera = GameObject.Find("Main Camera");
        if (camera)
        {
            camera.transform.position += new Vector3(Random.value, Random.value, 0) * 0.5f;
        }
    }

    private IEnumerator CastBoomContinously()
    {
        Rect boomArea = new Rect();
        boomArea.size = _deathSprite.GetComponent<Renderer>().bounds.size;
        boomArea.center = _deathSprite.transform.position;

        _startTime = Time.time;
        while (Time.time < _startTime + LifeTime)
        {
            CastBoom(new Vector3(
                boomArea.xMin + Random.value * boomArea.width,
                boomArea.yMin + Random.value * boomArea.height,
                _deathSprite.transform.position.z - 0.5f // 确保爆炸效果在人物前面
            ));
            yield return new WaitForSeconds(BoomInterval);
        }
    }

    public override void AfterDeath()
    {
        // 隐藏当前对象
        gameObject.SetActive(false);

        // 产生新贴图表示死亡状态
        _deathSprite = new GameObject();
        SpriteRenderer renderer = _deathSprite.AddComponent<SpriteRenderer>();
        renderer.sprite = DeathSprite;
        renderer.flipX = renderer.flipX;
        _deathSprite.transform.position = transform.position;
        _deathSprite.transform.localScale = transform.localScale;
        Destroy(_deathSprite, LifeTime);

        // 播放爆炸效果
        // 由于Coroutine一定要寄托在一个对象作为载体
        // 而原对象现在已经被禁用了
        // 所以就把执行Coroutine的任务交给Player了
        GameObject.Find("Player").GetComponent<HealthScript>().StartCoroutine(CastBoomContinously());
    }
}