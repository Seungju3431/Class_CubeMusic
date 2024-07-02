using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Timemanager timemanager;

    private void Start()
    {
        timemanager = FindObjectOfType<Timemanager>();
    }

    private void Update()
    {
        //입력 했을 때, 타이밍 판정
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timemanager.Check_Timming();
        }
    }
}
