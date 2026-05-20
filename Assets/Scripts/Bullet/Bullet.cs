using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private BulletPool pool;
    private float lifetime = 5f;
    // Waktu yang telah berlalu sejak peluru dibuat (newly added)
    private float elapsedTime = 0f;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log($"Bullet spawned with speed: {speed}, direction: {direction}");
        void OnEnable()
{
    // Setiap kali peluru aktif, mulai hitung mundur 5 detik
    StartCoroutine(DeactivateRoutine());
}

IEnumerator DeactivateRoutine()
{
    yield return new WaitForSeconds(5f);
    // Kembalikan peluru ke kolam dengan menonaktifkannya
    gameObject.SetActive(false); 
}

    }

    void Update()
    {

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

    public void SetPool(BulletPool bulletPool)
    {
        pool = bulletPool;
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        Debug.Log($"Bullet SetDirection called with: {newDirection}, normalized: {direction}");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy bullet on collision
        Debug.Log("Bullet hit: " + collision.gameObject.name);
          gameObject.SetActive(false); // NONAKTIFKAN OBJEK
    }
}
