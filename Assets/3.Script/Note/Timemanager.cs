using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timemanager : MonoBehaviour
{
    // 타이밍 -> 노트 판정 -> 노드의 현재 position의 기준으로 판정을 내림
    [Header("Perfect -> Cool -> Good -> Bad")]
    [SerializeField] private RectTransform[] timmingRect;

    private Vector2[] TimeBox; //Vector의 다른 사용법
                               //(최소,최대값을 담기 위해,
                               //평소처럼 좌표값을 얻기 위함이 아님)

    [SerializeField] private RectTransform Center;

    public List<GameObject> boxnote_List = new List<GameObject>();

    [SerializeField] private EffectManager effect;

    private void Start()
    {
        effect = FindObjectOfType<EffectManager>();

      //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        TimeBox = new Vector2[timmingRect.Length];

        //최소,최대값
        //이미지의 중심을 기준 + - (이미지너비 / 2)
        for (int i = 0; i < timmingRect.Length; i++)
        {
            TimeBox[i].Set
             (
                //최소값
                Center.localPosition.x - (timmingRect[i].rect.width / 2),
                //최대값
                Center.localPosition.x + (timmingRect[i].rect.width / 2)
                );
        }
    }

    public bool Check_Timming()
    {
        for (int i = 0; i < boxnote_List.Count; i++)
        {
            float notePos_X = boxnote_List[i].transform.localPosition.x;
            for (int j = 0; j < TimeBox.Length; j++)
            {
                //현재 TimeBox 범위 안에 있다면
                if (TimeBox[j].x <= notePos_X && notePos_X <= TimeBox[j].y)
                {
                    if (boxnote_List[i].transform.TryGetComponent(out Note n))
                    {
                        n.HiedNote();
                    }
                    //판정이 난 상황
                    Debug.Log(Debug_Note(j));
                    effect.NoteHit_Effect();
                    effect.Judgement_Effect(j);
                    return true;
                }
            }
        }
        return false;
    }

    public string Debug_Note(int x)
    {
        switch (x)
        {
            case 0:
                return "Perfect";
            case 1:
                return "Cool";
            case 2:
                return "Good";
            case 3:
                return "Bad";
            default:
                return string.Empty;

        }
    }
}
