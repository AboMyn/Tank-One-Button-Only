using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingTarget : MonoBehaviour
{
    public bool move = true;
    public float speed = 2f;
    public float range = 3f;

    public Vector3 direction = Vector3.right;

    private Rigidbody rb;
    private Vector3 startPos;
    private int dirSign = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        startPos = transform.position;

        if (direction.sqrMagnitude < 0.0001f)
            direction = Vector3.right;

        direction = direction.normalized;

        // чтобы не заваливалась
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (!move) { rb.linearVelocity = Vector3.zero; return; }

        // 1) если вышли за range — разворачиваемся
        float dist = Vector3.Distance(new Vector3(startPos.x, 0, startPos.z),
                                      new Vector3(rb.position.x, 0, rb.position.z));
        if (dist > range)
            dirSign *= -1;

        // 2) едем маленькими шагами через физику (НЕ телепорт)
        Vector3 vel = direction * dirSign * speed;
        rb.linearVelocity = new Vector3(vel.x, 0f, vel.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        // если ударились о стену/препятствие — разворот
        dirSign *= -1;
    }
}