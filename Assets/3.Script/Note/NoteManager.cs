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
    Queue<GameObject> Q_note = new Queue<GameObject>(); //큐 초기화


    [Header("BPM을 설정하시오.")]
    public int BPM = 0;

    private double current_time = 0d; //float x

    [Header("ETC")]
    [SerializeField] private GameObject notePrefabs;
    [SerializeField] private Transform noteSpawner;

    private bool isnoteActive = true;

    private Timemanager timemanager;
    private ComboManager combo;
    private EffectManager effect;
    private void Awake()
    {

        timemanager = FindObjectOfType<Timemanager>();
        combo = FindObjectOfType<ComboManager>();
        effect = FindObjectOfType<EffectManager>();

    }

    private void Update()
    {
        if (isnoteActive)
        {

            current_time += Time.deltaTime;
            if (current_time > (60d / BPM))
            {
                //GameObject note_ob;
                //        Instantiate(notePrefabs, 
                //        noteSpawner.position, Quaternion.identity);

                //note_ob.transform.SetParent(this.transform); //Note_UI를 부모로


                if (Q_note.Count > 0)
                {

                    var note_ob = Q_note.Dequeue();
                    note_ob.SetActive(true);
                    timemanager.boxnote_List.Add(note_ob);
                }
                else
                {
                    GameObject note_yb =
                    Instantiate(notePrefabs,
                        noteSpawner.position, Quaternion.identity);
                    note_yb.transform.SetParent(this.transform);
                    timemanager.boxnote_List.Add(note_yb);
                }



                //timemanager.boxnote_List.Add(note_ob);

                current_time -= (60d / BPM);
            }
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
                    //Debug.Log("Miss");
                    effect.Judgement_Effect(4);
                    combo.ResetCombo();
                }
            }

            col.gameObject.transform.localPosition = noteSpawner.localPosition;
            col.gameObject.SetActive(false);
            Q_note.Enqueue(col.gameObject);
            timemanager.boxnote_List.Remove(col.gameObject);
            //Destroy(col.gameObject);
        }

    }
    public void Remove_note()
    {
        isnoteActive = false;
        for (int i = 0; i < timemanager.boxnote_List.Count; i++)
        {
            timemanager.boxnote_List[i].SetActive(false);
            Q_note.Enqueue(timemanager.boxnote_List[i]);

        }
        timemanager.boxnote_List.Clear();
    }
}
