using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector3 direction;
    private BulletPool pool;
    private float lifetime = 5f;
    private float elapsedTime = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log($"Bullet spawned with speed: {speed}, direction: {direction}");
        // Destroy bullet after 5 seconds if it hasn't been destroyed already
        // Destroy(gameObject, 5f);
        elapsedTime = 0f;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

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
        if (collision.CompareTag("Player"))
        return;

        Debug.Log("Bullet hit: " + collision.gameObject.name);
        gameObject.SetActive(false);

        // if (!collision.CompareTag("Bullet"))
        // {
        //     Debug.Log("Bullet hit: " + collision.gameObject.name);
        //     if (pool != null)
        //     {
        //         pool.ReturnBullet(gameObject);
        //     }
        // }
        // Destroy bullet on collision
        // Debug.Log("Bullet hit: " + collision.gameObject.name);
        // Destroy(gameObject);
    }

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
