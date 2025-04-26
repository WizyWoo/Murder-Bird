using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour, IGrabbable
{

    public float Damage, FollowSpeed;
    public GameObject HitEffectPrefab;
    private Transform _grabbedBy;
    private Rigidbody _rb;

    private void Awake()
    {

        _rb = GetComponent<Rigidbody>();

    }

    public void Grab(Transform grabbedBy = null)
    {

        _grabbedBy = grabbedBy;

    }

    private void Update()
    {

        if(_grabbedBy == null)
            return;

        _rb.velocity = (_grabbedBy.position - transform.position).normalized * Vector3.Distance(_grabbedBy.position, transform.position) * FollowSpeed;
        transform.rotation = _grabbedBy.rotation;

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {

            damageable.TakeDamage(Damage);
            Instantiate(HitEffectPrefab, collision.contacts[0].point, Quaternion.identity).transform.LookAt(collision.contacts[0].point + collision.contacts[0].normal);
    
        }

    }

}
