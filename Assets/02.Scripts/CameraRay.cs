using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    private ARTapTopPlaceObject onoffPortFolio;

    private Transform tr;
    private Ray cameraRay;
    private RaycastHit hit;

    //hit objects
    private GameObject hitAlive;
    private GameObject hitPenguin;
    private GameObject hitWord;
    private Transform penguinTr;

    private Camera arCamera;

    //WordWorld
    private WordAni aniCheck;


    //AliveCube material
    private Material alivecubeMat;

    //시선볼경우
    public bool seeOnOff;
    void Start()
    {
        tr = this.transform;
        //hitPenguin = GameObject.FindWithTag("PENGUIN");

        //hitAlive = GameObject.FindWithTag("ALIVECUBE");
    }


    void Update()
    {
        hitAlive = GameObject.FindWithTag("ALIVECUBE");
        hitPenguin = GameObject.FindWithTag("PENGUIN");
        hitWord = GameObject.FindWithTag("WORD");


        cameraRay = new Ray(tr.position, tr.forward);

        WordAni();
        AliveCubeAni();
        PenguinAni();

    }

    void WordAni()
    {
        
        if (Physics.Raycast(cameraRay, out hit, 100.0f, 1 << 13))
        {
            seeOnOff = true;
            hitWord.gameObject.GetComponent<WordAni>().repaetCheck = true;
            hitWord.transform.LookAt(tr);
            hitWord.transform.GetChild(2).gameObject.SetActive(true);


            StartCoroutine(hitWord.gameObject.GetComponent<WordAni>().AnimationWord());
            
        }
       
        //벗어날 경우는 우선 임시적으로 큐브를 봤을 경우도 대체

    }

    void PenguinAni()
    {
        if (Physics.Raycast(cameraRay, out hit, 100.0f, 1 << 12))
        {
            seeOnOff = true;

            hitPenguin.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("JUMP", true);
            hitPenguin.transform.GetChild(0).transform.LookAt(tr);

            //Penguin설명 UI 활성화
            hitPenguin.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            //seeOnOff = false;

            hitPenguin.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("JUMP", false);
            hitPenguin.transform.GetChild(0).rotation = Quaternion.Euler(0,180,0);
            
            //Penguin설명 UI 비활성화
            hitPenguin.transform.GetChild(2).gameObject.SetActive(false);

        }
    }



    void AliveCubeAni()
    {

        if (Physics.Raycast(cameraRay, out hit, 100.0f, 1 << 11))
        {
            //wordani 비활성화
            hitWord.gameObject.GetComponent<WordAni>().repaetCheck = false;
            hitWord.transform.GetChild(2).gameObject.SetActive(false);


            hitWord.transform.rotation = Quaternion.Euler(0, 0, 0);

            hitWord.gameObject.GetComponent<WordAni>().AniInit();


            //애니메이션(무한회전)
            hitAlive.gameObject.GetComponent<Animator>().SetBool("ISLOOK", true);

            //AliveCube설명 UI 활성화
            hitAlive.transform.GetChild(1).gameObject.SetActive(true);
            //Glow 효과
            alivecubeMat = hitAlive.gameObject.GetComponent<MeshRenderer>().material;
            alivecubeMat.SetVector("_GLOWCOLOR", new Color(0f, 105f, 190f, 0f) * 0.1f);
            seeOnOff = true;
            
        }
        else
        {

            hitAlive.gameObject.GetComponent<Animator>().SetBool("ISLOOK", false);

            //AliveCube설명 UI 활성화
            hitAlive.transform.GetChild(1).gameObject.SetActive(false);

            alivecubeMat = hitAlive.gameObject.GetComponent<MeshRenderer>().material;
            alivecubeMat.SetVector("_GLOWCOLOR", new Color(0f, 105f, 190f, 0f) * 0.01f);
            seeOnOff = false;

        }


    }

}
