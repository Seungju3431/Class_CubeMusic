using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    /*
     BPM계산 (120 이라는 가정 하)
     분 -> 60초 
     노드 -> 4분음표(1beat)
     
     60/120 -> 0.5초에 1개씩
     
     */

    [Header("BPM을 설정하시오.")]
    public int BPM = 0;

    private double current_time = 0d; //float x

    [Header("ETC")]
    [SerializeField] private GameObject notePrefabs;
    [SerializeField] private Transform noteSpawner;

    private Timemanager timemanager;
    private void Awake()
    {
        timemanager = FindObjectOfType<Timemanager>();   
    }

    private void Update()
    {
        current_time += Time.deltaTime;
        if (current_time > (60d / BPM))
        {
            GameObject note_ob =
                    Instantiate(notePrefabs, 
                    noteSpawner.position, Quaternion.identity);

            note_ob.transform.SetParent(this.transform); //Note_UI를 부모로

            timemanager.boxnote_List.Add(note_ob);

            current_time -= (60d / BPM);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Note"))
        {

            if (col.TryGetComponent(out Note n))
            {
                if (n.GetNoteflag())
                {
                    Debug.Log("Miss");
                }
            }
            timemanager.boxnote_List.Remove(col.gameObject);
            Destroy(col.gameObject);
        }

    }
}
