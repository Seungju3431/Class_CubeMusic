using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] private GameObject UI_object;

    [SerializeField] private Text[] Text_count;
    [SerializeField] private Text Score_Text;
    [SerializeField] private Text MaxCombo_Text;


    private ScoreManager score;
    private ComboManager combo;
    private Timemanager timemanager;

    private void Start()
    {
        score = FindObjectOfType<ScoreManager>();
        combo = FindObjectOfType<ComboManager>();
        timemanager = FindObjectOfType<Timemanager>();
    }

    private void Init()
    {
        for (int i = 0; i < Text_count.Length; i++)
        {
            Text_count[i].text = "0";
        }
        Score_Text.text = "0";
        MaxCombo_Text.text = "0";
    }
    private string stringformat(string s)
    {
        return string.Format("{0:#,##0", s);
    }
    public void show_Result()
    {
        AudioManager.instance.stopBGM();
        Init();
        UI_object.SetActive(true);

        //판정기록
        int[] record_arr = timemanager.Get_JudgmentRecord();
        for (int i = 0;i < record_arr.Length; i++)
        {
            Text_count[i].text =stringformat (record_arr[i].ToString());
        }
        Score_Text.text = stringformat(score.GetScore().ToString());
        MaxCombo_Text.text = stringformat(combo.GetMaxcombo().ToString());
    }




}
