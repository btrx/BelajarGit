using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector3 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Menggantikan Start(), fungsi ini jalan SETIAP KALI peluru dipinjam dari pool
    void OnEnable()
    {
        Debug.Log($"Bullet spawned with speed: {speed}, direction: {direction}");
        
        // JANGAN PAKAI DESTROY! Pakai Invoke untuk menonaktifkan peluru setelah 5 detik
        Invoke("DeactivateBullet", 5f);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        // JANGAN PAKAI DESTROY! Cukup sembunyikan peluru saat menabrak musuh/dinding
        Debug.Log("Bullet hit: " + collision.gameObject.name);
        DeactivateBullet();
    }

    // Fungsi pembantu untuk mengembalikan peluru ke dalam bank (pool)
    void DeactivateBullet()
    {
        CancelInvoke(); // Batalkan timer 5 detik agar tidak bentrok
        gameObject.SetActive(false); // Sembunyikan objek
    }
}
