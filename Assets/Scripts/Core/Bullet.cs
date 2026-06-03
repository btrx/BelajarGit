using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
[SerializeField] private float speed = 10f;

private Rigidbody2D body;
private Vector2 moveDir;

private void Awake()
{
    body = GetComponent<Rigidbody2D>();
}

private void OnEnable()
{
    StartCoroutine(AutoDisable());
}

private IEnumerator AutoDisable()
{
    yield return new WaitForSeconds(5f);
    DisableBullet();
}

private void FixedUpdate()
{
    if (body != null)
    {
        body.linearVelocity = moveDir * speed;
        return;
    }

    transform.Translate(moveDir * speed * Time.fixedDeltaTime, Space.World);
}

public void SetDirection(Vector3 dir)
{
    moveDir = dir.normalized;
}

private void OnTriggerEnter2D(Collider2D other)
{
    DisableBullet();
}

private void DisableBullet()
{
    gameObject.SetActive(false);
}

}