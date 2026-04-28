using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game Data/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Player Stats")]
    public float maxHP;
    public float moveSpeed;
}

// [CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
// public class PlayerData : ScriptableObject
// {
//     public float maxHP;
//     public float moveSpeed;
// }