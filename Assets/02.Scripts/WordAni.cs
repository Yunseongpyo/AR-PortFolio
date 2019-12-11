using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WordAni : MonoBehaviour
{
    private List<GameObject> wordList = new List<GameObject>();

    private Transform[] tempWordlist;


    //기본위치
    private Transform firstObj;

    public bool repaetCheck;
    void Start()
    {
        tempWordlist = this.gameObject.GetComponentsInChildren<Transform>();
        for (int i=0; i<tempWordlist.Length; i++)
        {
            if (tempWordlist[i].CompareTag("HANGLE"))
            {
                wordList.Add(tempWordlist[i].gameObject);
            }
         
   
        }
        for (int i=1; i<wordList.Count; i++)
        {
            wordList[i].SetActive(false);
        }

        firstObj = wordList[0].transform;
    }

 
    public IEnumerator AnimationWord()
    {
        int wordlistCount = 0;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        wordList[0].transform.localPosition = new Vector3(0, -1.3f ,- 1.5f);
        wordList[0].transform.localRotation = Quaternion.Euler(0, 0, 0);
        while (repaetCheck)
        {
            wordList[wordlistCount].SetActive(false);

            if (wordlistCount == wordList.Count-1)
            {
                wordlistCount = -1;
            }
            wordList[wordlistCount + 1].SetActive(true);
            yield return new WaitForSeconds(1.0f);
            wordlistCount++;
            
        }

    }
    public void AniInit()
    {

        wordList[0].SetActive(true);
        wordList[0].transform.localPosition = new Vector3(0, -1.3f, 0.03f);
        wordList[0].transform.localRotation = Quaternion.Euler(0, 180, 0);
        for (int i = 1; i < wordList.Count; i++)
        {
            wordList[i].SetActive(false);
        }
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

}
