using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    /*
     BPM��� (120 �̶�� ���� ��)
     �� -> 60�� 
     ��� -> 4����ǥ(1beat)
     
     60/120 -> 0.5�ʿ� 1����
     
     */

    [Header("BPM�� �����Ͻÿ�.")]
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

            note_ob.transform.SetParent(this.transform); //Note_UI�� �θ��

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
