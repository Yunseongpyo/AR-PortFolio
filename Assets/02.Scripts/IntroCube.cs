using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class IntroCube : MonoBehaviour
{


    //빈오브젝트 생성
    GameObject emptyGameObject;

    //메인큐브맵 오브젝트
    public AudioClip fallDownSound;


    public Transform tr;
    private Transform preTr_right;
    private Transform preTr_left;
    private Transform nextTr;
    private List<GameObject> nowObjs = new List<GameObject>();
    private bool mapMake = false;
    //인스턴스 큐브 오브젝트
    private GameObject nextObj;

    //큐브 매터리얼 정보
    private Material cubeLine;

    private GameObject tempObj;

    private Animator anim;
    private int cubeTouch;


    public float cubesize;

    void Start()
    {
        preTr_right = tr;
        preTr_left = tr;
        this.gameObject.transform.localScale = this.gameObject.transform.localScale * cubesize;
        //preTr_left.rotation = Quaternion.Euler(left_Rot);
        //preTr_left.position = preTr_left.position + Vector3.left;

        emptyGameObject = new GameObject("TEMP");
        nextObj = Resources.Load("CubePivot") as GameObject;
        StartCoroutine(MakeMapLeftRight());
        StartCoroutine(MakeMapTop());
        StartCoroutine(MakeMapLeftWall(tr));
        StartCoroutine(MakeMapRightWall(tr));
        //cubeLine = Resources.Load("CubeGridLines_Glow") as Material;

    }




    IEnumerator MakeMapTop()
    {
        //GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;
        Vector3 left_Rot = new Vector3(0, 180.0f, 0);
        Vector3 right_dir = Vector3.zero;
        Vector3 left_dir = Vector3.zero;
        Vector3 left_Rot_Var = new Vector3(0, 180.0f, 270.0f);
        Vector3 temp_dir = new Vector3(0, 10.0f, 0.0f);
        //Vector3 right_dir_Var = new Vector3(0.5f, 0, 0.5f);
        GameObject temp_Obj = new GameObject();
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 12; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            nowObj.transform.localScale = nowObj.transform.localScale * cubesize;
            nowObj.transform.parent = emptyGameObject.transform;
            //nowObjs.Add(nowObj);
            if (i % 2 == 0)
            {

                nowObj.transform.position = preTr_right.position + right_dir + temp_dir* cubesize;

                //nowObj.transform.Rotate(Vector3.forward * -90.0f);
                nowObj.transform.DORotate(Vector3.forward * -90.0f, 0.5f);

                right_dir = right_dir + Vector3.right* cubesize;
                StartCoroutine(MakeMapForwardBack(nowObj.transform));



            }
            else if (i % 2 == 1)
            {
                nowObj.transform.position = tr.position + temp_dir* cubesize;
                nowObj.transform.rotation = Quaternion.Euler(left_Rot);
                nowObj.transform.position = nowObj.transform.position + Vector3.left* cubesize + left_dir;

                nowObj.transform.DORotate(left_Rot_Var, 0.5f);
                //nowObj.transform.DORotate(Vector3.forward * 90.0f, 0.5f);
                left_dir = left_dir + Vector3.left* cubesize;
                StartCoroutine(MakeMapForwardBack(nowObj.transform));

            }
            yield return new WaitForSeconds(0.1f);
        }
    }

  
    IEnumerator MakeMapLeftRight()
    {
        //GameObject nextObj = Resources.Load("CubePivot") as GameObject;
        Vector3 left_Rot = new Vector3(0, 180.0f, 0);
        Vector3 right_dir = Vector3.zero;
        Vector3 left_dir = Vector3.zero;
        Vector3 left_Rot_Var = new Vector3(0, 180.0f, 270.0f);
        Vector3 temp_dir = new Vector3(0, 10.0f, 0.0f);
        //Vector3 right_dir_Var = new Vector3(0.5f, 0, 0.5f);
        GameObject temp_Obj = new GameObject();

        for (int i = 0; i < 12; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            nowObj.transform.localScale = nowObj.transform.localScale * cubesize;

            nowObj.transform.parent = emptyGameObject.transform;

            if (i % 2 == 0)
            {

                nowObj.transform.position = preTr_right.position + right_dir;
                //temp_Obj.transform.position = preTr_right.position + right_dir + temp_dir;
                //nowObj.transform.Rotate(Vector3.forward * -90.0f);
                nowObj.transform.DORotate(Vector3.forward * -90.0f, 0.5f);

                right_dir = right_dir + Vector3.right* cubesize;
                StartCoroutine(MakeMapForwardBack(nowObj.transform));

                StartCoroutine(MakeMapUp(nowObj.transform, cubesize * 11f));
                StartCoroutine(MakeMapUp(nowObj.transform, 0));

            }
            else if (i % 2 == 1)
            {
                nowObj.transform.position = tr.position;
                nowObj.transform.rotation = Quaternion.Euler(left_Rot);
                nowObj.transform.position = nowObj.transform.position + Vector3.left* cubesize + left_dir;

                nowObj.transform.DORotate(left_Rot_Var, 0.5f);
                //nowObj.transform.DORotate(Vector3.forward * 90.0f, 0.5f);
                left_dir = left_dir + Vector3.left * cubesize;
                StartCoroutine(MakeMapForwardBack(nowObj.transform));
                StartCoroutine(MakeMapUp(nowObj.transform, cubesize*11f));
                StartCoroutine(MakeMapUp(nowObj.transform, 0));
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MakeMapForwardBack(Transform _tr)
    {
        //GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;

        Vector3 forward_Rot = new Vector3(0.0f, 270.0f, 0.0f);
        Vector3 forward_dir_Var = new Vector3(-0.5f, 0, 0.5f);
        Vector3 forward_Rot_Var = new Vector3(0, 270.0f, 270.0f);
        Vector3 forward_dir = Vector3.zero;


        for (int i = 0; i < 12; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            nowObj.transform.localScale = nowObj.transform.localScale * cubesize;

            nowObj.transform.parent = emptyGameObject.transform;

            nowObj.transform.position = _tr.position;
            nowObj.transform.rotation = Quaternion.Euler(forward_Rot);
            nowObj.transform.position = nowObj.transform.position + forward_dir_Var* cubesize + forward_dir;
            nowObj.transform.DORotate(forward_Rot_Var, 0.5f);
            //nowObj.transform.DOPunchPosition(Vector3.up, 0.5f,3);
            forward_dir = forward_dir + Vector3.forward * cubesize;


            yield return new WaitForSeconds(0.1f);

        }

    }

    IEnumerator MakeMapUp(Transform _tr, float _dirnum)
    {
        //GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;
        Vector3 up_Rot = new Vector3(0.0f, 0.0f, 180.0f);
        Vector3 up_dir_Var = new Vector3(1.0f, 1.0f, 0.0f);
        Vector3 up_Rot_Var = new Vector3(0.0f, 0.0f, 270.0f);
        Vector3 up_dir = Vector3.zero;

        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 11; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            nowObj.transform.localScale = nowObj.transform.localScale * cubesize;

            nowObj.transform.parent = emptyGameObject.transform;

            nowObj.transform.position = _tr.position + Vector3.forward * _dirnum + Vector3.left * cubesize *2;
            nowObj.transform.rotation = Quaternion.Euler(up_Rot);
            nowObj.transform.position = nowObj.transform.position + up_dir_Var* cubesize + up_dir;
            nowObj.transform.DORotate(up_Rot_Var, 0.5f);

            up_dir = up_dir + Vector3.up * cubesize;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MakeMapLeftWall(Transform _tr)
    {
        //GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;
        Vector3 LeftWall_Rot = new Vector3(0.0f, 0.0f, 90.0f);
        Vector3 LeftWall_dir_Var = new Vector3(0.0f, -1.0f, 0.0f);
        Vector3 LeftWall_Rot_Var = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 LeftWall_dir = Vector3.zero;
        //GameObject tempObj = new GameObject();
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 12; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            nowObj.transform.localScale = nowObj.transform.localScale * cubesize;

            nowObj.transform.parent = emptyGameObject.transform;

            //tempObj.transform.position = nowObj.transform.position;
            nowObj.transform.position = _tr.position + Vector3.right * cubesize* 5;
            nowObj.transform.rotation = Quaternion.Euler(LeftWall_Rot);
            nowObj.transform.position = nowObj.transform.position + LeftWall_dir_Var* cubesize + LeftWall_dir;
            nowObj.transform.DORotate(LeftWall_Rot_Var, 0.5f);
            //tempObj.transform.position = nowObj.transform.position + Vector3.left * 10;

            StartCoroutine(MakeMapForwardBack(nowObj.transform));


            //StartCoroutine(MakeMapForwardBack(tempObj.transform));

            LeftWall_dir = LeftWall_dir + Vector3.up * cubesize;

            yield return new WaitForSeconds(0.1f);
        }

    }


    IEnumerator MakeMapRightWall(Transform _tr)
    {
        //GameObject nextObj = Resources.Load("MakeIntroCube") as GameObject;
        Vector3 RightWall_Rot = new Vector3(0.0f, 0.0f, 90.0f);
        Vector3 RightWall_dir_Var = new Vector3(0.0f, -1.0f, 0.0f);
        Vector3 RightWall_Rot_Var = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 RightWall_dir = Vector3.zero;
        //GameObject tempObj = new GameObject();
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 12; i++)
        {
            GameObject nowObj = Instantiate(nextObj);
            nowObj.transform.localScale = nowObj.transform.localScale * cubesize;

            nowObj.transform.parent = emptyGameObject.transform;

            //tempObj.transform.position = nowObj.transform.position;
            nowObj.transform.position = _tr.position + Vector3.left * cubesize* 5;
            nowObj.transform.rotation = Quaternion.Euler(RightWall_Rot);
            nowObj.transform.position = nowObj.transform.position + RightWall_dir_Var* cubesize + RightWall_dir;
            nowObj.transform.DORotate(RightWall_Rot_Var, 0.5f);
            //tempObj.transform.position = nowObj.transform.position + Vector3.left * 10;

            StartCoroutine(MakeMapForwardBack(nowObj.transform));


            RightWall_dir = RightWall_dir + Vector3.up * cubesize;

            yield return new WaitForSeconds(0.1f);
        }

    }


}


