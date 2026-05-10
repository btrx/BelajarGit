using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerData playerData;

    private float currentHP;
    private float speed;

    void Start()
    {
        currentHP = playerData.maxHP;
        speed = playerData.moveSpeed;

        Debug.Log("Player HP: " + currentHP);
        Debug.Log("Player Speed: " + speed);
    }

    void Update()
    {
        if (GameManager.Instance.currentState != GameState.Playing)
        {
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(h, v, 0);

        transform.Translate(move * speed * Time.deltaTime);
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

        Debug.Log("Current HP: " + currentHP);

        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log("Press R to Restart");
            GameManager.Instance.GameOver();
        }
    }
}