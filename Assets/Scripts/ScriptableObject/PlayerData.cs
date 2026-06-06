using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    // Jumlah HP maksimum pemain. Digunakan untuk menyetel HP awal dan batas atas bar HP.
    public float maxHP;

    // Kecepatan gerak pemain. Digunakan oleh PlayerController untuk mengatur seberapa cepat pemain bergerak.
    public float moveSpeed;
}