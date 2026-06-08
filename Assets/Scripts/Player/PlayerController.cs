using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerData playerData;

    private float currentHP;
    private float speed;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    [Header("Shooting")]
    public Transform bulletSpawnPoint;

    private float attackInput;
    private float previousAttackInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        currentHP = playerData.maxHP;
        speed = playerData.moveSpeed;
    }

    void Update()
    {
        if (playerInput == null) return;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        attackInput = playerInput.actions["Attack"].ReadValue<float>();

        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);

        if (attackInput > 0 && previousAttackInput == 0)
        {
            Shoot();
        }

        previousAttackInput = attackInput;
    }

    void Shoot()
    {
        Vector3 spawnPos = bulletSpawnPoint != null
            ? bulletSpawnPoint.position
            : transform.position;

        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        Vector3 shootDirection = (mouseWorldPos - spawnPos).normalized;

        GameObject bulletObj = ObjectPool.Instance.GetPooledObject();

        if (bulletObj == null)
        {
            return;
        }

        bulletObj.transform.position = spawnPos;
        bulletObj.transform.rotation = Quaternion.identity;
        bulletObj.SetActive(true);

        Bullet bullet = bulletObj.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.SetDirection(shootDirection);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(Time.deltaTime);
        }
    }

    void TakeDamage(float dmg)
    {
        if (currentHP <= 0)
        {
            return;
        }

        currentHP -= dmg;

        if (currentHP <= 0)
        {
            currentHP = 0;

            Debug.Log("Player HP : " + currentHP);
            Debug.Log("Game Over");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }

            return;
        }

        Debug.Log("Player HP : " + currentHP);
    }
}