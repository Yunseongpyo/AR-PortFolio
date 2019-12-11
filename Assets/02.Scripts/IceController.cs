using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IceController : MonoBehaviour
{
    public Transform[] iceswpanpoint;
    public Transform[] icearrivepoint;
    public GameObject iceObj;
    void Start()
    {
        StartCoroutine(MakeIce());
        
    }
    IEnumerator MakeIce()
    {
        while(true)
        {
            yield return new WaitForSeconds(2.0f);
            int rannum = Random.Range(0, 3);
            GameObject makeIce = Instantiate(iceObj, iceswpanpoint[rannum]);
            makeIce.transform.DOMove(icearrivepoint[rannum].position, 10.0f).SetEase(Ease.Linear);
           

        }
    }
   
}
