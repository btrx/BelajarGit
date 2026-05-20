using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;
    private Vector3 direction;
    private BulletPool pool;
    private float lifetime = 5f;
    private float elapsedTime = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // OnEnable dipanggil SETIAP KALI SetActive(true), termasuk dari pool
    void OnEnable()
    {
        // Reset waktu dan gravitasi setiap kali peluru diaktifkan
        elapsedTime = 0f;

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > lifetime)
        {
            ReturnToPool();
            return;
        }

        if (rb != null)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        Debug.Log($"Bullet direction set: {direction}");
    }

    public void SetPool(BulletPool bulletPool)
    {
        pool = bulletPool;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
        {
            Debug.Log("Bullet hit: " + collision.gameObject.name);
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (pool != null)
        {
            pool.ReturnBullet(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}