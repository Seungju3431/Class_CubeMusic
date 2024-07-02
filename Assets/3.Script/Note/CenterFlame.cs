using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
    /*
     ù ��° ��Ʈ�� ������ ��, �뷡�� �÷��� �ϴ� ����
     -> ��Ʈ�� �浹 ���� ��
     �浹 : Ʈ���Ÿ� ���
     */

    private AudioSource source;
    private bool isStart = false;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isStart)
        {
            if (col.CompareTag("Note"))
            {
                source.Play();
                isStart = true;
            }
        }
    }
}
