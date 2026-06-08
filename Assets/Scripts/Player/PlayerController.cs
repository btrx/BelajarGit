using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerStat playerData;
    private float currentHP;
    private float speed;
    public GameObject bulletPrefab;
    private Vector2 lastDirection = Vector2.right;
    public Transform bulletSpawnPoint;
    void Start()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

            speed = 5f;
            currentHP = 100f;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(h, v);

        if (moveDir != Vector2.zero)
        {
            lastDirection = moveDir.normalized;
        }

        Vector3 move = new Vector3(h, v, 0);

        transform.position += move * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = PooledObjects.Instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = transform.position + (Vector3)lastDirection;

            bullet.SetActive(true);

            bullet.GetComponent<Bullet>()
                .SetDirection(lastDirection);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;

        if (currentHP < 0)
        {
            currentHP = 0;
        }

        Debug.Log("Player HP: " + currentHP);
    }
}