using Unity.VisualScripting;
using UnityEngine;
using System.Collections; // Tambahkan system.collections untuk menggunakan IEnumerator

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector3 direction;
    private BulletPool pool;
    private float lifetime = 5f;
    private float elapsedTime = 0f;

    private Coroutine deactivateCoroutine;

    void OnEnable()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        elapsedTime = 0f;
        if (deactivateCoroutine != null)
        {
            StopCoroutine(deactivateCoroutine);
        }
        deactivateCoroutine = StartCoroutine(DeactivateRoutine());
    }

    IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(lifetime);
        ReturnToPool();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        elapsedTime = 0f;
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
            rb.linearVelocity = direction * speed;
        }
        else
        {
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
        if (collision.CompareTag("Bullet"))
        {
            return;
        }

        Debug.Log("Bullet hit: " + collision.gameObject.name);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (deactivateCoroutine != null)
        {
            StopCoroutine(deactivateCoroutine);
            deactivateCoroutine = null;
        }

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