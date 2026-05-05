using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    private float currentHP;
    private float speed;
    private PlayerInput playerInput;
    private Vector2 moveInput;
    private float attackInput;
    private float previousAttackInput;
    public PlayerData playerData;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        currentHP = playerData.maxHP;
        speed = playerData.moveSpeed;

    }
    
    
    void Update()
    {
        if (playerInput == null) return;

        attackInput = playerInput.actions["Attack"].ReadValue<float>();
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
        if (attackInput > 0 && previousAttackInput == 0)
        {
            Shoot();
        }
        previousAttackInput = attackInput;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(playerData.damage);
        }
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    void Shoot()
    {
        Debug.Log("Player shoots!");
        // Implement shooting logic here (e.g., instantiate a bullet prefab)
    }
}