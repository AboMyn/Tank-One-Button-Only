using UnityEngine;

public class Target : MonoBehaviour
{
    public void Die()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnTargetDestroyed();

        Destroy(gameObject);
    }
}