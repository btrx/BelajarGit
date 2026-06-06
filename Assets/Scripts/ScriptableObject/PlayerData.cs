using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{

    // Mengubah agar bisa diatur dari Inspector
    public float maxHP;
    public float moveSpeed;
}