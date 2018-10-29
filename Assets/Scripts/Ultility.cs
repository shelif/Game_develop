using System;
using UnityEngine;

public static class Ultility
{
    public static void SetTimeOut(double interval, System.Action act)
    {
        System.Timers.Timer tmr = new System.Timers.Timer();
        tmr.Interval = interval;
        tmr.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs args)
        {
            act();
            tmr.Stop();
        };
        tmr.Start();
    }

    public static void MyTranslate(Transform transform, Vector3 direction)
    {
        foreach (RaycastHit2D info in Physics2D.RaycastAll(transform.position, direction, direction.magnitude))
        {
            if (info.collider.gameObject.layer == LayerMask.NameToLayer("Block")
                && !info.collider.isTrigger)
            {
                return;
            }
        }

        transform.Translate(direction);
    }

    public static void InstantiateParticle(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = UnityEngine.Object.Instantiate(
            prefab,
            position,
            Quaternion.identity
        ) as ParticleSystem;
        UnityEngine.Object.Destroy(
            newParticleSystem.gameObject,
            newParticleSystem.startLifetime
        );
    }

    public static AudioSource PlaySound(this GameObject obj, String path)
    {
        AudioSource audioSource = obj.GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = obj.AddComponent<AudioSource>();
        }

        audioSource.clip = Resources.Load<AudioClip>(path);
        audioSource.Play();
        return audioSource;
    }
}