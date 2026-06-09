using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletData bulletData;

    private float timer;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        timer = 0f;
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    public void Launch(Vector2 dir)
    {
        if (rb != null)
            rb.linearVelocity = dir.normalized * bulletData.speed;
    }

    void Update()
    {
        if (bulletData == null) return;

        timer += Time.deltaTime;
        if (timer >= bulletData.lifetime)
            gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
            gameObject.SetActive(false);
    }
}
