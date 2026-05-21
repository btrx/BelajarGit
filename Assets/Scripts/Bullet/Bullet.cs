using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        Invoke(nameof(DisableBullet), 10f); 
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void DisableBullet()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}