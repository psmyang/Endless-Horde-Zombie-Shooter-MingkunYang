using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float currentHealth;
    public float CurrentHealth => currentHealth;

    [SerializeField]
    private float maxHealth;
    public float MaxHealth => maxHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
        //Score.scoreAmount = Score.scoreAmount + 10;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            //StartCoroutine(Delay());
            Destroy();
            
        }        

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
        //Destroy(gameObject);
    }
}
