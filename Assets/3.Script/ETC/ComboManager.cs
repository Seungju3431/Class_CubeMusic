using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    /*
     콤보가 활성화 되는 시점
    1. miss가 아니면 카운트 ++

     콤보가 비활성화 되는 시점
    1.miss가 나온다면 Reset
    2.Bad가 나올때는 콤보를 셈하지 않게
     */

    [SerializeField] private GameObject combo_img;
    [SerializeField] private Text comboText;

    private int current_combo;

    private Animator ani;
    private string Key = "Combo";

    private void Start()
    {
        ani = GetComponent<Animator>();
        combo_img.SetActive(false);
        comboText.gameObject.SetActive(false);
    }

    public void ResetCombo()
    {
        current_combo = 0;
        comboText.text = string.Format("{0:#,##0}", current_combo);
        combo_img.SetActive(false);
        comboText.gameObject.SetActive(false);

    }

    public void Addcombo(int combo = 1)
    {
        current_combo += combo;
        comboText.text = string.Format("{0:#,##0}", current_combo);
        if (current_combo >= 2)
        {
            combo_img.SetActive(true);
            comboText.gameObject.SetActive(true);
            ani.SetTrigger(Key);
        }
    }



}
