using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;
    private Vector3 direction;

    // Referensi ke BulletPool
    private BulletPool pool;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    if (rb != null)
        {
        rb.linearVelocity = direction * speed;
        }
    else
        {
        transform.position += direction * speed * Time.deltaTime;
        }
    }

    // Dipanggil oleh PlayerController
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    // Dipanggil oleh BulletPool
    public void SetPool(BulletPool bulletPool)
    {
        pool = bulletPool;
    }

    private void OnEnable()
    {
        // Balik ke pool setelah 5 detik
        Invoke(nameof(ReturnToPool), 12f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void ReturnToPool()
    {
        if (pool != null)
        {
            pool.ReturnBullet(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            return;

        Debug.Log("Bullet hit: " + collision.gameObject.name);

        // Balik ke pool, bukan destroy
        ReturnToPool();
    }
}