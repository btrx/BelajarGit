using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    // Supaya hp dan speed dapat di atur dari Player Data
    public float maxHP;
    public float moveSpeed;
}