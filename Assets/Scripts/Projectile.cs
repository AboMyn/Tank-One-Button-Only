using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Target target = collision.gameObject.GetComponent<Target>();

        // Взрываемся только если попали в цель
        if (target != null)
        {
            target.Die();
            
            // Shake только при попадании
            CameraFollow cam = Object.FindFirstObjectByType<CameraFollow>();
            if (cam != null)
            {
                cam.Shake(0.12f, 0.18f);
            }

            StartCoroutine(ExplosionFlash());
            return;
        }

        // Если столкнулись НЕ с целью (например, с землёй) — просто уничтожаем пулю
        Destroy(gameObject);
    }

    IEnumerator ExplosionFlash()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale * 3f;

        float t = 0f;
        float duration = 0.12f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float k = t / duration;
            transform.localScale = Vector3.Lerp(startScale, endScale, k);
            yield return null;
        }

        Destroy(gameObject);
    }
}