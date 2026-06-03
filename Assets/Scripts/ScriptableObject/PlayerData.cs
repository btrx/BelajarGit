using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public float jumpForce = 5f;
    public float moveSpeed = 10f;

    [Header("Health")]
    public float maxHealth = 100;
}