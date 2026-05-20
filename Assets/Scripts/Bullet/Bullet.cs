using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;

    private Vector3 direction;

    // Waktu hidup maksimal peluru dalam detik
    private float lifetime = 5f;

    // Waktu yang telah berlalu sejak peluru dibuat
    private float elapsedTime = 0f;

    // Penanda apakah bullet sudah selesai dipakai
    private bool expired = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Debug.Log($"Bullet spawned with speed: {speed}, direction: {direction}");
    }

    void OnEnable()
    {
        // Reset timer saat peluru aktif
        elapsedTime = 0f;

        // Reset status expired
        expired = false;
    }

    void Update()
    {
        // Jika bullet sudah expired, hentikan update
        if (expired) return;

        // Tambahkan waktu yang telah berlalu
        elapsedTime += Time.deltaTime;

        // Jika peluru sudah melampaui lifetime
        if (elapsedTime > lifetime)
        {
            // Tandai bullet sudah selesai dipakai
            expired = true;

            // Hentikan gerakan peluru
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }

            // Hilangkan bullet dari scene
            gameObject.SetActive(false);

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
}