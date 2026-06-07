using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector3 direction;

    void Awake()
    {
        // Ambil Rigidbody sekali saja saat objek pertama kali dibuat di memori
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // Setiap kali peluru muncul dari pool, jalankan batas waktu aktif (5 detik)
        StartCoroutine(DeactivateRoutine());
    }

    void OnDisable()
    {
        // AMAN: Matikan Coroutine jika peluru mati sebelum 5 detik (misal karena nabrak)
        StopAllCoroutines();
    }

    IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false); // Kembalikan ke pool
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        
        // LANGSUNG GERAKKAN DI SINI: Biar Rigidbody langsung dapet arah baru seketika!
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
        
        Debug.Log($"Bullet aktif dari pool dengan kecepatan: {speed}, arah: {direction}");
    }

    void Update()
    {
        // Jika tidak pakai Rigidbody2D, baru gunakan pergerakan manual ini
        if (rb == null)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // FILTER AMAN: Peluru hanya akan mati jika menabrak dinding (Wall)
        // Ini biar peluru gak sengaja nabrak badan player sendiri pas baru keluar!
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Bullet sukses mengenai: " + collision.gameObject.name);
            gameObject.SetActive(false); // Nonaktifkan peluru, kembalikan ke pool
        }
    }
}