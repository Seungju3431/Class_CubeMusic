using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
    /*
     첫 번째 노트가 지나갈 때, 노래를 플레이 하는 역할
     -> 노트가 충돌 했을 때
     충돌 : 트리거를 사용
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
