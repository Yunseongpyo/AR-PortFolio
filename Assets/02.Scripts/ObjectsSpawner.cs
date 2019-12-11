using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    public GameObject objectToSpwan;
    private ARTapTopPlaceObject placementIndicator;

    public bool ischeckClear;
    private void Start()
    {
        placementIndicator = FindObjectOfType<ARTapTopPlaceObject>();

    }
    private void Update()
    {
        if(Input.touchCount >0 && Input.touches[0].phase == TouchPhase.Began  /*&& !ischeckClear*/)
        {
            GameObject obj = Instantiate(objectToSpwan, placementIndicator.transform.position, placementIndicator.transform.rotation);
            placementIndicator.gameObject.SetActive(false);
            //ischeckClear = !ischeckClear;
        }
    }
}
