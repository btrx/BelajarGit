using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float maxHP = 100f;
    public float speed = 5f; // Pastikan ini tertulis 'speed' dengan huruf kecil!
}