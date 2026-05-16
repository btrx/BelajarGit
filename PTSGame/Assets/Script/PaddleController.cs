using UnityEngine;

public class PaddleController : MonoBehaviour 
{
    public PlayerData data;
    [Header("Centang jika ini Paddle Kiri. Jangan dicentang jika Paddle Kanan")]
    public bool isLeftPaddle; 

    void Update() 
    {
        // Jika file data belum dipasang di Inspector, batalkan gerakan agar tidak error
        if (data == null) return;

        float input = 0;

        // KONTROL PADDLE KIRI (Hanya merespon W dan S)
        if (isLeftPaddle) 
        {
            if (Input.GetKey(KeyCode.W)) {
                input = 1;
            }
            if (Input.GetKey(KeyCode.S)) {
                input = -1;
            }
        } 
        // KONTROL PADDLE KANAN (Hanya merespon Panah Atas dan Panah Bawah)
        else 
        {
            if (Input.GetKey(KeyCode.UpArrow)) {
                input = 1;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                input = -1;
            }
        }

        // Eksekusi pergerakan paddle naik/turun sesuai Speed dari file Data
        transform.Translate(Vector3.up * input * data.paddleSpeed * Time.deltaTime);
    }
}