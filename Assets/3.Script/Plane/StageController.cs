using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private GameObject Stage;
    private Transform[] Stage_Plate;

    [SerializeField] private float Offset_Y = -3f;
    [SerializeField] private float Plate_Speed = 10f;

    //���� ���� �÷���Ʈ�� ��
    [SerializeField] private int Step_count = 0;
    //��� �÷���Ʈ�� ��
    [SerializeField] private int totalPlate_count = 0;


    private void Start()
    {
        Setting_Stage();
    }
    

    
    private void Setting_Stage()
    {
        //�ʱ� 2 ����
        Step_count = 2;

        //�ʱ� ����
        Stage_Plate = Stage.GetComponent<Stage>().Plate;
        totalPlate_count = Stage_Plate.Length;

        for (int i = 0; i < totalPlate_count; i++)
        { //                                ��>�ڽ��� Ȱ��ȭ �Ǿ��ִ��� Ȯ��
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

        //������������ ��ǥ �������� �����
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
