using UnityEngine;
 
[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Health")]
    public float maxHP = 100f;
 
    [Header("Movement")]
    public float moveSpeed = 5f;
}
