using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class IceMove : MonoBehaviour
{
  
  
  
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("WALL"))
        {
            Destroy(this.gameObject);
        }
    }
}
