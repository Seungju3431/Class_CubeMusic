using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    /*
     �޺��� Ȱ��ȭ �Ǵ� ����
    1. miss�� �ƴϸ� ī��Ʈ ++

     �޺��� ��Ȱ��ȭ �Ǵ� ����
    1.miss�� ���´ٸ� Reset
    2.Bad�� ���ö��� �޺��� ������ �ʰ�
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
