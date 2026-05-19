using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector3 direction;

    void OnEnable(){
        // setiap kali peluru muncul, 5 detik kemudian akan otomatis dinonaktifkan jika
        StartCoroutine(DeactivateRoutine());
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log($"Bullet spawned with speed: {speed}, direction: {direction}");
    }

    void Update()
    {
        if (rb != null)
        {
            // Move using Rigidbody2D
            rb.linearVelocity = direction * speed;
        }
        else
        {
            // Fallback to manual movement if no Rigidbody2D
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        Debug.Log($"Bullet SetDirection called with: {newDirection}, normalized: {direction}");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet hit: " + collision.gameObject.name);
        gameObject.SetActive(false); // NONAKTIFKAN OBJEK
    }

     // Pastikan namespace ini ada di paling atas

    IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(5f);
        // Kembalikan peluru ke kolam dengan menonaktifkannya
        gameObject.SetActive(false); 
    }


}
