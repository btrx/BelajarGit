using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector3 direction;

    void OnEnable()
    {
        StopAllCoroutines();
        // Setiap kali peluru muncul, 5 detik kemudian akan otomatis dinonaktifkan jika belum mengenai apa-apa
        StartCoroutine(DeactivateRoutine());
    }

    IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(5f);
        ReturnToPool();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log($"Bullet spawned with speed: {speed}, direction: {direction}");
    }

    void Update()
    {
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

    void ReturnToPool()
    {
        StopAllCoroutines();
        if (PooledObjects.Instance != null)
            PooledObjects.Instance.ReturnObject(gameObject);
        else
            gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy bullet on collision
        Debug.Log("Bullet hit: " + collision.gameObject.name);
        // Nonaktifkan peluru jika mengenai sesuatu, dan kembalikan ke pool
        ReturnToPool();
    }
}