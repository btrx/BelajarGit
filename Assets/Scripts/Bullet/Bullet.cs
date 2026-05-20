using UnityEngine;
using System.Collections; // Pastikan namespace ini ada di paling atas

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
        // Reset waktu yang berlalu saat peluru aktif (newly added)
        elapsedTime = 0f;
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

    // Simpan referensi ke pool untuk nanti dikembalikan (newly added)
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
            gameObject.SetActive(false); // NONAKTIFKAN OBJEK

            // if (pool != null)
            // {
            //     pool.ReturnBullet(gameObject);
            // }
        }
    }

    void OnEnable()
    {
        // Setiap kali peluru aktif, mulai hitung mundur 5 detik
        elapsedTime = 0f;
        StartCoroutine(DeactivateRoutine());
    }

    IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(5f);
        // Kembalikan peluru ke kolam dengan menonaktifkannya
        gameObject.SetActive(false); 
    }
}
