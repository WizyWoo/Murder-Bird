using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurderableThing : MonoBehaviour, IDamageable
{

    public float health;
    public GameObject ToActivateOnDeath;
    public bool MoveToDeathLoc;
    public GameObject ToSpawnOnDeath;
    public bool SpawnOnDeathPos;
    public Vector3 WorldSpawnPos;
    public GameObject[] DestroyOnDeath;
    public GameObject DeathEffectPrefab;

    public void TakeDamage(float damage)
    {

        health -= damage;
        if(health <= 0)
        {

            Die();

        }

    }

    public void Die()
    {

        Instantiate(DeathEffectPrefab, transform.position, Quaternion.identity);

        if(ToActivateOnDeath != null)
        {

            ToActivateOnDeath.SetActive(true);
            if(MoveToDeathLoc)
                ToActivateOnDeath.transform.position = transform.position + Vector3.up;

        }

        if(ToSpawnOnDeath != null)
            Instantiate(ToSpawnOnDeath, SpawnOnDeathPos ? transform.position + Vector3.up : WorldSpawnPos, Quaternion.identity);

        if(DestroyOnDeath.Length > 0)
            for(int i = 0; i < DestroyOnDeath.Length; i++)
                Destroy(DestroyOnDeath[i]);

        Destroy(gameObject);

    }

}
