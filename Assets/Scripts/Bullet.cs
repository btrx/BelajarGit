using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector3 direction;

    void Awake()
    {
        // Mengambil Rigidbody di Awake agar lebih aman
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // Setiap kali peluru muncul dari pool, reset waktunya (5 detik otomatis mati)
        StartCoroutine(DeactivateRoutine());
    }

    IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

    void Start()
    {
        // Memastikan komponen rb dicari ulang jika belum dapat
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        Debug.Log($"Bullet active. Speed: {speed}, Direction: {direction}");
    }

    void Update()
    {
        // JIKA ada Rigidbody2D, pakai velocity (Fisika)
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
        // JIKA TIDAK ADA Rigidbody2D, pakai pergerakan manual (Mencegah Error Null)
        else
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        
        // SINKRONISASI: Jika arah disetel setelah peluru aktif, langsung paksa velocity-nya jalan
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Peluru menabrak sesuatu, kembalikan ke pool (matikan)
        Debug.Log("Bullet hit: " + collision.gameObject.name);
        gameObject.SetActive(false);
    }
}