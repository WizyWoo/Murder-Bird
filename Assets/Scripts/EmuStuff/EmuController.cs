using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmuController : MonoBehaviour
{

    public float GrabRange;
    private Transform _heldObject;
    [SerializeField]
    private Transform _mouth;

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.E) && _heldObject == null)
        {

            RaycastHit[] hits = Physics.SphereCastAll(transform.position, GrabRange, transform.forward, GrabRange, LayerMask.GetMask("Grabbable"), QueryTriggerInteraction.Ignore);

            if(hits.Length == 0)
                return;
            
            RaycastHit closestHit = hits[0];
            for(int i = 1; i < hits.Length; i++)
            {

                if(hits[i].distance < closestHit.distance)
                    closestHit = hits[i];

            }

            closestHit.transform.GetComponent<IGrabbable>()?.Grab(_mouth);
            _heldObject = closestHit.transform;

        }
        else if(Input.GetKeyDown(KeyCode.E) && _heldObject != null)
        {

            _heldObject.GetComponent<IGrabbable>()?.Grab();
            _heldObject = null;

        }

    }

}
