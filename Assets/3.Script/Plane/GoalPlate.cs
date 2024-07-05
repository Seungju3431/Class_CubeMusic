using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlate : MonoBehaviour
{
    /*
     게임종료
    1. 결과창 나타나게 하기 -> result
    2. note가 더이상 나오지 않도록 설정 -> notemanager
    3. audio에 finish 음향 넣기
    4. player 움직이지 못하게 하기 -> isCanpresskey
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
