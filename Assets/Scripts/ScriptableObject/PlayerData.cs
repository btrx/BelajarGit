using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Game/Player Stat")]
public class PlayerStat : ScriptableObject
{
    public int hp = 100;
    public float speed = 5f;
}