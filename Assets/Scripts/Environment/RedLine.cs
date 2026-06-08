using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RedLine : MonoBehaviour
{
    [Tooltip("Damage applied when player touches the red line (single hit)")]
    public float damageOnTouch = 10f;

    [Tooltip("If true, will deal damage continuously while player stays in contact (every intervalSeconds)")]
    public bool damageOverTime = false;

    [Tooltip("Interval between damage ticks when damageOverTime is enabled")]
    public float intervalSeconds = 0.5f;

    private void Reset()
    {
        // make sure collider is trigger by default for easy setup
        var col = GetComponent<Collider2D>();
        if (col != null)
            col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var pc = other.GetComponent<PlayerController>();
        if (pc == null)
        {
            Debug.LogWarning("RedLine: PlayerController not found on Player object.");
            return;
        }

        if (damageOverTime)
        {
            // start coroutine on this RedLine instance to damage player
            StartCoroutine(DamageOverTimeRoutine(pc));
        }
        else
        {
            pc.TakeDamage(damageOnTouch);
            Debug.Log("RedLine: applied " + damageOnTouch + " damage to player on touch.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // stop any running coroutines when player leaves
        if (!other.CompareTag("Player")) return;
        StopAllCoroutines();
    }

    private System.Collections.IEnumerator DamageOverTimeRoutine(PlayerController pc)
    {
        while (true)
        {
            if (pc == null) yield break;
            pc.TakeDamage(damageOnTouch);
            yield return new WaitForSeconds(intervalSeconds);
        }
    }

    // Also support non-trigger colliders
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        var pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc == null) return;
        pc.TakeDamage(damageOnTouch);
    }
}
