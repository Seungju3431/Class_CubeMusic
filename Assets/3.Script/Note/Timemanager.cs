using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timemanager : MonoBehaviour
{
    // Ÿ�̹� -> ��Ʈ ���� -> ����� ���� position�� �������� ������ ����
    [Header("Perfect -> Cool -> Good -> Bad")]
    [SerializeField] private RectTransform[] timmingRect;

    private Vector2[] TimeBox; //Vector�� �ٸ� ����
                               //(�ּ�,�ִ밪�� ��� ����,
                               //���ó�� ��ǥ���� ��� ������ �ƴ�)

    [SerializeField] private RectTransform Center;

    public List<GameObject> boxnote_List = new List<GameObject>();

    [SerializeField] private EffectManager effect;

    private void Start()
    {
        effect = FindObjectOfType<EffectManager>();

      //�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�
        TimeBox = new Vector2[timmingRect.Length];

        //�ּ�,�ִ밪
        //�̹����� �߽��� ���� + - (�̹����ʺ� / 2)
        for (int i = 0; i < timmingRect.Length; i++)
        {
            TimeBox[i].Set
             (
                //�ּҰ�
                Center.localPosition.x - (timmingRect[i].rect.width / 2),
                //�ִ밪
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
                //���� TimeBox ���� �ȿ� �ִٸ�
                if (TimeBox[j].x <= notePos_X && notePos_X <= TimeBox[j].y)
                {
                    if (boxnote_List[i].transform.TryGetComponent(out Note n))
                    {
                        n.HiedNote();
                    }
                    //������ �� ��Ȳ
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
