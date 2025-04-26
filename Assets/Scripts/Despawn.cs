using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Despawn : MonoBehaviour
{

    public float DespawnTime = 5f;

    private void Start()
    {
        
        Destroy(gameObject, DespawnTime);

    }

}
