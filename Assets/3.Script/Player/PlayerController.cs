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
        //�Է� ���� ��, Ÿ�̹� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timemanager.Check_Timming();
        }
    }
}
