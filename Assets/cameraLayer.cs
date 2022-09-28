using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraLayer : MonoBehaviour
{
    void Update () {
    // Bit shift the index of the layer (8) to get a bit mask
    int layerMask = 1 << 3;
        
    // Esto lanzaría rayos solo contra colliders en la layer (capa) 8.
    // Pero en cambio queremos colisionar contra todo excepto la layer (capa) 8. El operador ~ hace esto, invierte una bitmask.
    layerMask = ~layerMask;
    
    RaycastHit hit;
    // El rayo se cruza con cualquier objeto que excluya la layer (capa) del jugador?
    if (Physics.Raycast(transform.position, transform.TransformDirection (Vector3.forward), out hit, Mathf.Infinity, layerMask)) {
        Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * hit.distance, Color.yellow);
        Debug.Log("Did Hit");
    } else {
        Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) *1000, Color.white);
        Debug.Log("Did not Hit");
    }
}
    
}
