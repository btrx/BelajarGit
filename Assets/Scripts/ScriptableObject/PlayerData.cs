using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    public float maxHP = 100f;
    public float moveSpeed = 5f;
}