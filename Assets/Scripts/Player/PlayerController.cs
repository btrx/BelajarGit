using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
[SerializeField] private PlayerData playerData;

public GameObject bulletPrefab;
public Transform bulletSpawnPoint;

public float currentHP;
public float speed;

private PlayerInput playerInput;
private Vector2 moveInput;

private float attackValue;
private float lastAttackValue;

void Start()
{
    playerInput = GetComponent<PlayerInput>();

    if (playerData == null)
    {
        Debug.LogError("PlayerData belum di-assign di Inspector!");
        return;
    }

    currentHP = playerData.maxHP;
    speed = playerData.moveSpeed;
}

void Update()
{
    if (GameManager.Instance != null && GameManager.Instance.currentState != GameState.Playing)
        return;

    if (playerInput != null)
    {
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        Vector3 direction = new Vector3(moveInput.x, moveInput.y, 0);
        transform.Translate(direction * speed * Time.deltaTime);

        HandleShoot();
    }
}

void HandleShoot()
{
    attackValue = playerInput.actions["attack"].ReadValue<float>();

    if (attackValue > 0 && lastAttackValue <= 0)
    {
        SpawnBullet();
    }

    lastAttackValue = attackValue;
}

void SpawnBullet()
{
    Vector3 startPos = bulletSpawnPoint != null
        ? bulletSpawnPoint.position
        : transform.position;

    Vector3 target =
        Camera.main.ScreenToWorldPoint(Input.mousePosition);

    target.z = 0;

    Vector3 dir = (target - startPos).normalized;

    GameObject bullet = PooledObjects.Instance.GetPooledObject();

    if (bullet == null)
        return;

    bullet.transform.position = startPos;
    bullet.transform.rotation = Quaternion.identity;
    bullet.SetActive(true);

    Bullet bulletScript = bullet.GetComponent<Bullet>();

    if (bulletScript != null)
    {
        bulletScript.SetDirection(dir);
    }
}

void OnCollisionStay2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Wall"))
    {
        TakeDamage(0.1f);
    }
}

void TakeDamage(float dmg)
{
    currentHP -= dmg;

    if (currentHP <= 0 && GameManager.Instance != null)
    {
        GameManager.Instance.GameOver();
    }
}
}