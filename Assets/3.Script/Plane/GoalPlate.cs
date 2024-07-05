using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlate : MonoBehaviour
{
    /*
     ��������
    1. ���â ��Ÿ���� �ϱ� -> result
    2. note�� ���̻� ������ �ʵ��� ���� -> notemanager
    3. audio�� finish ���� �ֱ�
    4. player �������� ���ϰ� �ϱ� -> isCanpresskey
     */

    private Result result;
    private NoteManager notemanager;

    private void Start()
    {
        result = FindObjectOfType<Result>();
        notemanager = FindObjectOfType<NoteManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            result.show_Result();
            notemanager.Remove_note();
            AudioManager.instance.PlaySFX("Finish");
            PlayerController.isCanpressKey = false;
        }
    }
}
