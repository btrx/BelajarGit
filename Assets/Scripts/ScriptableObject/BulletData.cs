using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Game/Bullet Data")]
public class BulletData : ScriptableObject
{
    [Header("Bullet Settings")]
    public float speed = 10f;
    public float damage = 1f;
    public float lifetime = 3f;
    public int poolSize = 20;
}
