using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IGrabbable
{

    public GameObject[] DestroyOnGrab;

    public void Grab(Transform grabbedBy = null)
    {

        if(DestroyOnGrab.Length > 0)
            for(int i = 0; i < DestroyOnGrab.Length; i++)
                Destroy(DestroyOnGrab[i]);

        Destroy(gameObject);

    }

}
