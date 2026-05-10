using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;

    private float currentHP;
    private float speed;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        currentHP = playerData.maxHP;
        speed = playerData.moveSpeed;
    }
    void Update()
    {
        if (GameManager.Instance.currentState != GameState.Playing)
            return;

        if (playerInput == null) return;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
    }

}
    