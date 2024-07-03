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

    [SerializeField] private float[] weight; //가중치

    private void Awake()
    {
        ani = GetComponent<Animator>();
        Score_Text = transform.GetChild(0).GetComponent<Text>();

    }

    public void AddScore(int index)
    {

        //            ┌> 소수점 자리 없애기 위해 int 형변환
        int _score = (int)(Defult_Score * weight[index]);

        Current_Score += _score;
        Score_Text.text = string.Format("{0:#,##0}", Current_Score); //1000점 넘겼을 시
        ani.SetTrigger(Key);
    }
}
