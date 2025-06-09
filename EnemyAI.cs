using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Simulasi musuh selalu berjalan
        // Bisa ganti dengan logika AI kalau perlu

        // Trigger serangan jika dekat target (contoh)
        if (ShouldAttack())
        {
            animator.SetTrigger("Attack");
        }
    }

    bool ShouldAttack()
    {
        // Ganti logika ini sesuai kebutuhan
        // Misalnya, jika dekat player
        return Vector3.Distance(transform.position, PlayerPos()) < 1.5f;
    }

    Vector3 PlayerPos()
    {
        return GameObject.FindWithTag("Player").transform.position;
    }
}
