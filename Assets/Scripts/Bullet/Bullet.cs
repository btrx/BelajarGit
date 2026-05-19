using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // setiap peluru aktif mulai timer
        StartCoroutine(DeactivateRoutine());
    }

    IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(5f);

        // matikan peluru
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
        else
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet hit: " + collision.gameObject.name);

        // nonaktifkan peluru
        gameObject.SetActive(false);
    }
}