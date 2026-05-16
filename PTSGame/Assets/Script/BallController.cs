using UnityEngine;

public class BallController : MonoBehaviour
{
    public PlayerData data;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartBall() 
    {
        Invoke("PushBall", 2f);
    }

    void PushBall()
    {
        if (data == null) {
            Debug.LogError("File PlayerData belum ditarik ke objek Bola!");
            return;
        }

        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.linearVelocity = new Vector2(x * data.ballSpeed, y * data.ballSpeed);
    }
void Update()
{
    // Jika bola melewati batas kiri (-10) atau kanan (10) di layar
    if (transform.position.x < -10f || transform.position.x > 10f)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.TriggerGameOver();
        }
    }
}
}