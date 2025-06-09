using UnityEngine;

public class CompanionAI : MonoBehaviour
{
    public Transform player;
    public float roamRadius = 3f;        // Radius roaming di sekitar player
    public float detectionRadius = 4f;   // Jarak deteksi musuh
    public float attackRange = 1.2f;     // Jarak serang musuh
    public float moveSpeed = 2f;

    public int attackDamage = 1;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    private Transform targetEnemy;
    private Vector2 roamTarget;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        roamTarget = GetRoamPosition();
    }

    void Update()
    {
        targetEnemy = FindNearestEnemy();

        if (targetEnemy != null && Vector2.Distance(transform.position, targetEnemy.position) < attackRange)
        {
            AttackEnemy();
        }
        else if (targetEnemy != null)
        {
            MoveTo(targetEnemy.position);
        }
        else
        {
            // Companion roaming terus di sekitar player dalam radius roamRadius
            if (Vector2.Distance(transform.position, roamTarget) < 0.1f)
            {
                roamTarget = GetRoamPosition();
            }
            else
            {
                MoveTo(roamTarget);
            }
        }
    }

    void MoveTo(Vector2 destination)
    {
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    void AttackEnemy()
    {
        rb.velocity = Vector2.zero;

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            if (targetEnemy != null)
            {
                EnemyHealth enemyHealth = targetEnemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                }
            }
        }
    }

    Transform FindNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        float minDist = Mathf.Infinity;
        Transform nearest = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = hit.transform;
                }
            }
        }

        return nearest;
    }

    Vector2 GetRoamPosition()
    {
        Vector2 randomOffset = Random.insideUnitCircle * roamRadius;
        return (Vector2)player.position + randomOffset;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(player.position, roamRadius);
    }
}
