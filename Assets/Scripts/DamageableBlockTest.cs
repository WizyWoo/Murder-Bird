using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableBlockTest : MonoBehaviour, IDamageable
{

    public float Health = 100f;
    public GameObject DeathPoff;

    public void TakeDamage(float damage)
    {

        Health -= damage;

        if(Health <= 0f)
        {

            Destroy(gameObject);
            Instantiate(DeathPoff, transform.position, Quaternion.identity);

        }

    }

}
