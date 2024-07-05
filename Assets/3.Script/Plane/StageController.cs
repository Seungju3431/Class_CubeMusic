using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private GameObject Stage;
    private Transform[] Stage_Plate;

    [SerializeField] private float Offset_Y = -3f;
    [SerializeField] private float Plate_Speed = 10f;

    //현재 밟은 플레이트의 수
    [SerializeField] private int Step_count = 0;
    //모든 플레이트의 수
    [SerializeField] private int totalPlate_count = 0;


    private void Start()
    {
        Setting_Stage();
    }
    

    
    private void Setting_Stage()
    {
        //초기 2 설정
        Step_count = 2;

        //초기 세팅
        Stage_Plate = Stage.GetComponent<Stage>().Plate;
        totalPlate_count = Stage_Plate.Length;

        for (int i = 0; i < totalPlate_count; i++)
        { //                                ┌>자신이 활성화 되어있는지 확인
            if (!Stage_Plate[i].gameObject.activeSelf)
            {
                Stage_Plate[i].position =
                    new Vector3(Stage_Plate[i].position.x,
                    Stage_Plate[i].position.y + Offset_Y,
                    Stage_Plate[i].position.z);
            
            }
        }
    
    }

    public void ShowNextPlate()
    {
        if (Step_count < totalPlate_count)
        {
            StartCoroutine(MovePlate_co(Step_count++));
        }
    }

    private IEnumerator MovePlate_co(int index)
    {
        Stage_Plate[index].gameObject.SetActive(true);

        //목적지까지의 목표 포지션을 만들기
        Vector3 destpos =
            new Vector3(Stage_Plate[index].position.x,
            Stage_Plate[index].position.y - Offset_Y,
            Stage_Plate[index].position.z);

        while (Vector3.SqrMagnitude(Stage_Plate[index].position -
            destpos) >= 0.001f)
        {
            Stage_Plate[index].position =
                    Vector3.Lerp(Stage_Plate[index].position,
                    destpos,
                    Plate_Speed * Time.deltaTime);
            yield return null;
        }
        Stage_Plate[index].position = destpos;
    
    }
}
