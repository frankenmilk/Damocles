using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Animator animator;

    [SerializeField] int maxHealth;
    int currentHealth;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] float knockBackForce;
    [SerializeField] float knockBackForceUp;

    private bool takingDamage;
    private float timeTime;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (takingDamage == true)
        {
            GetComponent<Patrol>().enabled = false;
        }
        else if (takingDamage == false)
        {
            GetComponent<Patrol>().enabled = true;
        }

        if (Time.time >= timeTime + 1f)
        {
            takingDamage = false;
        }
    }

    public void TakeDamage(int damage)
    {
        takingDamage = true;
        timeTime = Time.time;
        Knockback();

        currentHealth -= damage;

        animator.SetTrigger("Ouch");

        // Play hurt Animation

        if (currentHealth <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        Debug.Log("enemy died :)");

        // Plays death animation
        animator.SetBool("IsNotAlive", true);

        // Disables the enemy

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Patrol>().enabled = false;
        this.enabled = false;

    }

    public void Knockback()
    {
        Transform attacker = GetClosestDamageSource();
        Vector2 knockbackDirection = new Vector2(transform.position.x - attacker.transform.position.x, 0);
        rb.velocity = new Vector2(knockbackDirection.x, knockBackForceUp) * knockBackForce;
    }

    public Transform GetClosestDamageSource()
    {
        GameObject[] DamageSources = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        Transform currentClosestDamageSource = null;

        foreach (GameObject go in DamageSources)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position); 
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                currentClosestDamageSource = go.transform;
            }
        }
        return currentClosestDamageSource;
    }

}