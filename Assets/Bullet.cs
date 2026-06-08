using UnityEngine;
using System.Collections; // Tambahkan system.collections untuk menggunakan IEnumerator

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector2 direction;

    void Awake()
    {
        // Ensure Rigidbody2D is available before OnEnable/Update
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // Setiap kali peluru muncul, 5 detik kemudian akan otomatis dinonaktifkan jika belum mengenai apa-apa
        
    }

    

    void Start()
    {
        Debug.Log($"Bullet spawned with speed: {speed}, direction: {direction}");
    }

    void Update()
    {
        if (rb != null)
        {
            // Use Rigidbody2D.velocity (not linearVelocity)
            rb.linearVelocity = direction * speed;
        }
        else
        {
            // Fallback to manual movement if no Rigidbody2D
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = new Vector2(newDirection.x, newDirection.y).normalized;
        Debug.Log($"Bullet SetDirection called with: {newDirection}, normalized: {direction}");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy bullet on collision
        Debug.Log("Bullet hit: " + collision.gameObject.name);
        // Nonaktifkan peluru jika mengenai sesuatu, dan kembalikan ke pool
        gameObject.SetActive(false);
    }
}
    