using UnityEngine;
using UnityEngine.InputSystem;

public class TankOneButtonController : MonoBehaviour
{
    [Header("One Button")]
    public bool useMouseLeftButton = false; // false = Space, true = ЛКМ

    [Header("Movement")]
    public float moveSpeed = 4f;

    [Header("Rotation")]
    public float rotateSpeed = 120f;
    private int rotateDirection = 1;

    [Header("Shooting")]
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 1f;

    [Header("Ammo")]
    public int ammo = 30;

    [Header("Visual")]
    public Transform barrel;

    [Header("Audio")]
    public AudioClip shootSound;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        bool held = ButtonIsHeld();

        // ✅ Вращаемся только когда НЕ держим кнопку
        if (!held)
        {
            float yRotation = rotateSpeed * rotateDirection * Time.deltaTime;
            transform.Rotate(0f, yRotation, 0f, Space.World);
        }

        // ✅ Нажал (один раз) -> выстрел
        if (ButtonPressedThisFrame())
        {
            Fire();
        }

        // ✅ Отпустил -> меняем направление вращения
        if (ButtonReleasedThisFrame())
        {
            rotateDirection *= -1;
        }
    }

    void FixedUpdate()
    {
        // ✅ Пока держим -> едем прямо
        if (ButtonIsHeld())
        {
            Vector3 nextPos = rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(nextPos);
        }
    }

    void Fire()
    {
        if (ammo <= 0) return;
        if (projectilePrefab == null || firePoint == null) return;

        ammo--;

        // Отдача назад
        rb.AddForce(-transform.forward * 4f, ForceMode.Impulse);

        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
        // Вспышка
        if (barrel != null)
        {
            StartCoroutine(BarrelFlash());
        }

        // 1) Спавним пулю чуть впереди, чтобы не появлялась внутри ствола/танка
        Vector3 spawnPos = firePoint.position + firePoint.forward * 0.35f;

        GameObject proj = Instantiate(projectilePrefab, spawnPos, firePoint.rotation);

        // 2) Игнорируем коллизии пули с танком (все коллайдеры танка)
        Collider projCol = proj.GetComponent<Collider>();
        if (projCol != null)
        {
            Collider[] myCols = GetComponentsInChildren<Collider>();
            foreach (Collider c in myCols)
            {
                Physics.IgnoreCollision(projCol, c);
            }
        }

        // 3) Даём пуле скорость вперёд
        Rigidbody projRb = proj.GetComponent<Rigidbody>();
        if (projRb != null)
        {
            projRb.linearVelocity = firePoint.forward * projectileSpeed;
        }
    }

    // --- New Input System button helpers ---
    bool ButtonIsHeld()
    {
        if (useMouseLeftButton)
            return Mouse.current != null && Mouse.current.leftButton.isPressed;

        return Keyboard.current != null && Keyboard.current.spaceKey.isPressed;
    }

    bool ButtonPressedThisFrame()
    {
        if (useMouseLeftButton)
            return Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

        return Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
    }

    bool ButtonReleasedThisFrame()
    {
        if (useMouseLeftButton)
            return Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame;

        return Keyboard.current != null && Keyboard.current.spaceKey.wasReleasedThisFrame;
    }

    System.Collections.IEnumerator BarrelFlash()
    {
        Vector3 originalScale = barrel.localScale;
        Vector3 flashScale = originalScale * 1.3f;

        barrel.localScale = flashScale;

        yield return new WaitForSeconds(0.05f);

        barrel.localScale = originalScale;
    }
}