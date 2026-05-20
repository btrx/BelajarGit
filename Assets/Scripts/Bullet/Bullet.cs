using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector3 direction;
    // Referensi ke pool peluru (newly added)
    private BulletPool pool;
    // Waktu hidup maksimal peluru dalam detik (newly added)
    private float lifetime = 5f;
    // Waktu yang telah berlalu sejak peluru dibuat (newly added)
    private float elapsedTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log($"Bullet spawned with speed: {speed}, direction: {direction}");
        // Destroy bullet after 5 seconds if it hasn't been destroyed already
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // Tambahkan waktu yang telah berlalu (newly added)
        elapsedTime += Time.deltaTime;

        // Jika peluru sudah melampaui lifetime, kembalikan ke pool (newly added)
        if (elapsedTime > lifetime)
        {
            if (pool != null)
            {
                pool.ReturnBullet(gameObject);
            }
            return;
        }
        if (rb != null)
        {
            // Move using Rigidbody2D
            rb.linearVelocity = direction * speed;
        }
        else
        {
            // Fallback to manual movement if no Rigidbody2D
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        Debug.Log($"Bullet SetDirection called with: {newDirection}, normalized: {direction}");
    }
        public void SetPool(BulletPool bulletPool)
    {
        pool = bulletPool;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy bullet on collision
        Debug.Log("Bullet hit: " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
