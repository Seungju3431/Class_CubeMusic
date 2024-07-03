using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text Score_Text;
    [SerializeField] private Animator ani;

    private string Key = "Score";

    private int Current_Score = 0;
    private int Defult_Score = 10;

    [SerializeField] private float[] weight; //����ġ

    private void Awake()
    {
        ani = GetComponent<Animator>();
        Score_Text = transform.GetChild(0).GetComponent<Text>();

    }

    public void AddScore(int index)
    {

        //            ��> �Ҽ��� �ڸ� ���ֱ� ���� int ����ȯ
        int _score = (int)(Defult_Score * weight[index]);

        Current_Score += _score;
        Score_Text.text = string.Format("{0:#,##0}", Current_Score); //1000�� �Ѱ��� ��
        ani.SetTrigger(Key);
    }
}
