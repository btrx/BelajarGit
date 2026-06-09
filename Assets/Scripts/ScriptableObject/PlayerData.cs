using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Player Stats")]
    public float maxHP = 3f;
    public float moveSpeed = 5f;
    public float wallDamage = 1f;

    [Header("Shooting")]
    public float fireRate = 3f;
}