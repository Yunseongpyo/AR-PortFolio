using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapTopPlaceObject : MonoBehaviour
{


    private ARRaycastManager rayManager;
    private GameObject visual;
    private List<ARRaycastHit> hits;
    private GameObject instanceObj;


    private Ray ray;
    private RaycastHit hitobj;

    //포트폴리오 오브젝트
    public GameObject objectToSpwan;

    //클리어 체크 
    public bool ischeckClear;

    //aliveCube On/Off체크
    private bool aliveCubeVideoOnoff;

    //ar카메라 부분
    public Camera arCamera;
    private CameraRay onoffCameraRay;


    //각 포트폴리오 맵
    public GameObject cubemap;
    public GameObject wordWorldmap;
    public GameObject mlAgentmap;


    private GameObject destroyMap;


    public GameObject wordExplode;
    private void Awake()
    {
        onoffCameraRay = arCamera.GetComponent<CameraRay>();

    }
    private void Start()
    {

        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;
        //Touch touch = Input.GetTouch(0);
        visual.SetActive(false);
    }

  


    private void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        
        //포티폴리오 생성
        if (rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (!visual.activeInHierarchy && ischeckClear == false)
            {
                visual.SetActive(true);
            }
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && ischeckClear == false)
            {
                ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                if(Physics.Raycast(ray, out hitobj, 100.0f, 1<<9))
                {
                    onoffCameraRay.enabled = true;
                    instanceObj = Instantiate(objectToSpwan, hits[0].pose.position, hits[0].pose.rotation);

                    ischeckClear = !ischeckClear;
                    visual.SetActive(false);
                }

            }
        }


        //포트폴리오 생성중
        if(ischeckClear == true)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                

               if (Physics.Raycast(ray, out hitobj, 100.0f, 1 << 11) && aliveCubeVideoOnoff == false)
                {
                    //설명UI끄기
                    hitobj.transform.GetChild(1).gameObject.SetActive(false);

                    destroyMap = Instantiate(cubemap, hitobj.transform.position + new Vector3(0,-0.5f,-1.0f), hitobj.transform.rotation);
                    //카메라 레이 끄기
                    onoffCameraRay.enabled = false;
                    StartCoroutine(DelayVideoPlayer(hitobj));
                    
                    aliveCubeVideoOnoff = !aliveCubeVideoOnoff;
                }
                else if (Physics.Raycast(ray, out hitobj, 100.0f, 1 << 11) && aliveCubeVideoOnoff == true)
                {
                    hitobj.transform.GetChild(0).gameObject.SetActive(false);

                    GameObject temp = GameObject.Find("TEMP");
                    GameObject[] cubewall = GameObject.FindGameObjectsWithTag("CUBEWALL");
                    for (int i = 0; i < cubewall.Length; i++)
                    {
                        cubewall[i].GetComponent<Rigidbody>().useGravity = true;
                        cubewall[i].GetComponent<Rigidbody>().isKinematic = false;
                    }

                    Destroy(temp, 7.0f);
                    Destroy(destroyMap, 8.0f);
                    aliveCubeVideoOnoff = !aliveCubeVideoOnoff;
                    onoffCameraRay.enabled = true;
                }

                //워드월드

                if (Physics.Raycast(ray, out hitobj, 100.0f, 1 << 15) && aliveCubeVideoOnoff == false)
                {
                    Debug.Log("워드월드");
                    GameObject parenthitObj = hitobj.transform.parent.gameObject.transform.parent.gameObject;
                    
                    //파티클 켜기
                    wordExplode.SetActive(true);

                    parenthitObj.transform.parent.transform.GetChild(1).gameObject.SetActive(true);
                    destroyMap = Instantiate(wordWorldmap);
                    destroyMap.transform.localPosition = new Vector3(0, -0.3f, 0.554f);
                    parenthitObj.SetActive(false);

                    onoffCameraRay.enabled = false;
                    aliveCubeVideoOnoff = !aliveCubeVideoOnoff;

                }
                else if (Physics.Raycast(ray, out hitobj, 100.0f, 1 << 15) && aliveCubeVideoOnoff == true)
                {
                    GameObject parenthitObj = hitobj.transform.parent.gameObject.transform.parent.gameObject;

                    parenthitObj.transform.parent.transform.GetChild(1).gameObject.SetActive(false);

                    //파티클 끄기
                    wordExplode.SetActive(false);
                    Destroy(destroyMap);
                    onoffCameraRay.enabled = true;
                    aliveCubeVideoOnoff = !aliveCubeVideoOnoff;

                }

                //ML-Agent
                if (Physics.Raycast(ray, out hitobj, 100.0f, 1 << 12) && aliveCubeVideoOnoff == false)
                {
                    Debug.Log("펭귄펭귄");

                    //설명UI 비활성화
                    hitobj.transform.GetChild(2).gameObject.SetActive(false);
                    //destroyMap = Instantiate(mlAgentmap);
                    //destroyMap.transform.localPosition = new Vector3(-1.5f, -0.3f, 0);
                    //destroyMap.transform.localRotation = arCamera.transform.rotation;
                    hitobj.transform.GetChild(2).localRotation = arCamera.transform.rotation;
                    hitobj.transform.GetChild(1).gameObject.SetActive(true);
                    hitobj.transform.GetChild(3).gameObject.SetActive(true);

                    onoffCameraRay.enabled = false;
                    aliveCubeVideoOnoff = !aliveCubeVideoOnoff;

                }
                else if (Physics.Raycast(ray, out hitobj, 100.0f, 1 << 12) && aliveCubeVideoOnoff == true)
                {

                    hitobj.transform.GetChild(1).gameObject.SetActive(false);
                    hitobj.transform.GetChild(3).gameObject.SetActive(false);

                    //Destroy(destroyMap);

                    onoffCameraRay.enabled = true;
                    aliveCubeVideoOnoff = !aliveCubeVideoOnoff;

                }


            }

        }

    }
    
    IEnumerator DelayVideoPlayer(RaycastHit _hitobj)
    {
        yield return new WaitForSeconds(4.0f);
        _hitobj.transform.GetChild(0).gameObject.SetActive(true);

    }

}
