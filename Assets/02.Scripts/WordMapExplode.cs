using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMapExplode : MonoBehaviour
{
    private Transform[] mapList;
    public GameObject plane;
    public AudioClip fallDownSound;
    private AudioSource _aduio;
    private void Awake()
    {
        mapList = this.gameObject.GetComponentsInChildren<Transform>();
        _aduio = this.gameObject.GetComponent<AudioSource>();
        _aduio.clip = fallDownSound;

    }
    void Start()
    {
        _aduio.Play();
        plane.SetActive(false);
        for (int i = 1; i < mapList.Length; i++)
        {
            mapList[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
