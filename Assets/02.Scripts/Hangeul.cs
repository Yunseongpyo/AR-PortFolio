using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;
using System.Text;
using System.Windows;
public class Hangeul : MonoBehaviour
{
    //check on/off
    public bool check_break = true;

    public Transform tr;
    private Transform cam;

    Vector3 voice_dir;

    TextMesh test_t;
    //Text test_t;
    public Text f_text;
    string save_text;

    public GameObject word_1st;
    public GameObject word_2nd;
    public GameObject word_3rd;



    // 초성, 중성, 종성 테이블.
    private static string mFirstWordTbl = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
    private static string mMiddleWordTbl = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
    private static string mLastWordTbl = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";

    // 초성, 중성, 종성 3D 모델링 매칭을 위한 배열
    private static GameObject[] m3D_FirstWordTbl;
    private static GameObject[] m3D_MiddleWordTbl;
    private static GameObject[] m3D_LastWordTbl;


    private static ushort mUniCodeHangleBase = 0xAC00; // 유니코드 한글 첫번째 위치
    private static ushort mUniCodeHangleLast = 0xD79F; // 유니코드 한글 마지막 위치
    private static string mFirstWord;
    private static string mMiddleWord;
    private static string mLastWord;

    void Start()
    {

        test_t = GetComponent<TextMesh>();

        m3D_FirstWordTbl = new GameObject[19];
        m3D_MiddleWordTbl = new GameObject[21];
        m3D_LastWordTbl = new GameObject[28];
        test_t.text = test_t.text.Replace(" ", "");
        Debug.Log(test_t.text);
        voice_dir = new Vector3(0, 0, 19);
        cam = this.GetComponent<Transform>();

        //각각의 배열들에 3D 모델링

        for (int i = 0; i < m3D_FirstWordTbl.Length; i++)
        {
            m3D_FirstWordTbl[i] = Resources.Load<GameObject>(new string(mFirstWordTbl[i], 1));
        }

        for (int i = 0; i < m3D_MiddleWordTbl.Length; i++)
        {
            m3D_MiddleWordTbl[i] = Resources.Load<GameObject>(new string(mMiddleWordTbl[i], 1));
        }

        for (int i = 0; i < m3D_LastWordTbl.Length; i++)
        {
            m3D_LastWordTbl[i] = Resources.Load<GameObject>(new string(mLastWordTbl[i], 1));
        }

    }

    private void Update()
    {
        if (check_break == false)
        {
            check_break = !check_break;
            Matching_WordBreak();

        }
    }

    private void Matching_WordBreak()
    {


        for (int i = 0; i < test_t.text.Length; i++)
        {
            WordBreak(test_t.text[i]);
            for (int j = 0; j < 3; j++)
            {
                switch (j)
                {
                    case 0:
                        for (int k = 0; k < m3D_FirstWordTbl.Length; k++)
                        {
                            if (save_text[0] == mFirstWordTbl[k])
                            {
                                Instantiate(m3D_FirstWordTbl[k], cam.position, this.transform.rotation);
                                break;
                            }
                        }
                        break;
                    case 1:
                        for (int k = 0; k < m3D_MiddleWordTbl.Length; k++)
                        {
                            if (save_text[1] == mMiddleWordTbl[k])
                            {
                                Instantiate(m3D_MiddleWordTbl[k], cam.position, this.transform.rotation);
                                break;
                            }
                        }
                        break;
                    case 2:
                        for (int k = 0; k < m3D_LastWordTbl.Length; k++)
                        {
                            if (save_text[1] == mLastWordTbl[k])
                            {
                                Instantiate(m3D_LastWordTbl[k], cam.position, this.transform.rotation);
                                break;
                            }
                        }
                        break;
                }

            }

        }

    }
    public static string word_1stum(string sFirstWord, string sMiddleWord, string sLastWord)
    {
        int i_FristPos, i_MiddlePos, i_LastWord;
        int iUniCode;
        i_FristPos = mFirstWordTbl.IndexOf(sFirstWord);   // 초성 위치
        i_MiddlePos = mMiddleWordTbl.IndexOf(sMiddleWord);   // 중성 위치
        i_LastWord = mLastWordTbl.IndexOf(sLastWord);   // 종성 위치
                                                        // 앞서 만들어 낸 계산식
        iUniCode = mUniCodeHangleBase + (i_FristPos * 21 + i_MiddlePos) * 28 + i_LastWord;
        // 코드값을 문자로 변환
        char temp = System.Convert.ToChar(iUniCode);
        return temp.ToString();
    }

    public void WordBreak(char OneWord)
    {
        int One_FirstIdx, One_MiddleIdx, One_LastIdx; // 초성,중성,종성의 인덱스
        ushort uTempCode = 0x0000;       // 임시 코드용
                                         //Char을 16비트 부호없는 정수형 형태로 변환 - Unicode
        uTempCode = System.Convert.ToUInt16(OneWord);

        // 캐릭터가 한글이 아닐 경우 처리
        if ((uTempCode < mUniCodeHangleBase) || (uTempCode > mUniCodeHangleLast))
        {
            mFirstWord = ""; mMiddleWord = ""; mLastWord = "";
        }
        // iUniCode에 한글코드에 대한 유니코드 위치를 담고 이를 이용해 인덱스 계산.
        int iUniCode = uTempCode - mUniCodeHangleBase;
        One_FirstIdx = iUniCode / (21 * 28);
        iUniCode = iUniCode % (21 * 28);
        One_MiddleIdx = iUniCode / 28;
        iUniCode = iUniCode % 28;
        One_LastIdx = iUniCode;

        mFirstWord = new string(mFirstWordTbl[One_FirstIdx], 1);
        mMiddleWord = new string(mMiddleWordTbl[One_MiddleIdx], 1);
        mLastWord = new string(mLastWordTbl[One_LastIdx], 1);

        save_text = mFirstWord + mMiddleWord + mLastWord;

    }
}
