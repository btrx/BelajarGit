using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public float lifetime = 3f;
    public float damage = 1f;

    [Header("Optional Data")]
    public BulletData bulletData;

    [Header("Collision Settings")]
    public string shooterTag = "";
    public string[] targetTags = { "Enemy" };
    public string[] obstacleTags = { "Wall", "Ground" };

    private float timer;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isFired = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        timer = 0f;
        isFired = false;

        if (bulletData != null)
        {
            speed = bulletData.speed;
            damage = bulletData.damage;
            lifetime = bulletData.lifetime;
        }
    }

    public void Fire(Vector2 direction)
    {
        moveDirection = direction.normalized;
        isFired = true;

        if (rb != null)
        {
            rb.linearVelocity = moveDirection * speed;
        }
    }

    void Update()
    {
        if (!isFired) return;

        if (rb == null)
        {
            transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        }

        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            DestroyBullet();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!string.IsNullOrEmpty(shooterTag) && other.gameObject.tag == shooterTag)
            return;

        foreach (string tag in targetTags)
        {
            if (other.gameObject.tag == tag)
            {
                DestroyBullet();
                return;
            }
        }

        foreach (string tag in obstacleTags)
        {
            if (other.gameObject.tag == tag)
            {
                DestroyBullet();
                return;
            }
        }
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}
