using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTr;
    private Transform tr;
    void Start()
    {
        camTr = GameObject.Find("AR Camera").GetComponent<Transform>();
        tr = this.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tr.LookAt(camTr.position);
    }
}
