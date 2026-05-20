using Unity.VisualScripting;
using UnityEngine;
using System.Collections; // Tambahkan system.collections untuk menggunakan IEnumerator

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector3 direction;
    private BulletPool pool;
    private float elapsedTime = 0f;

    void OnEnable()
    {
        // Setiap kali peluru muncul, 5 detik kemudian akan otomatis dinonaktifkan jika belum mengenai apa-apa
        elapsedTime = 0f;
        StartCoroutine(DeactivateRoutine());
    }

     IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(5f);

        if (pool != null)
        {
            pool.ReturnBullet(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log($"Bullet spawned with speed: {speed}, direction: {direction}");
       // Reset waktu yang berlalu saat peluru aktif (newly added)   
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        {


         if (rb != null)
            {
                rb.linearVelocity = direction * speed;
            }
            else
            {
                // Fallback to manual movement if no Rigidbody2D
                transform.position += direction * speed * Time.deltaTime;
            }
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
        
        // Jika peluru menabrak sesuatu (selain peluru lain), kembalikan ke pool (newly added)
        if (!collision.CompareTag("Bullet"))
        {
            Debug.Log("Bullet hit: " + collision.gameObject.name);

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
}
