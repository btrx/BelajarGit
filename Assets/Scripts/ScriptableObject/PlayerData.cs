using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game Data/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Player Stats")]
    public float maxHP;
    public float moveSpeed;
}