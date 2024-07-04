using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�̵�")]
    [SerializeField] private float MoveSpeed = 3f;

    //Ű���� �Է°� ���� �÷��̾��� �������� �ٸ�.
    [SerializeField] private Vector3 Input_Direction = new Vector3();//Ű����
    [SerializeField] private Vector3 Dest_pos = new Vector3();//���� �����̴� ����

    [Header("ȸ��")]
    [SerializeField] private float spinSpeed = 270f;
    [SerializeField] private Vector3 Input_rotDirection = new Vector3();
    [SerializeField] private Quaternion Dest_rot = new Quaternion();

    [SerializeField] private Transform Fake_Cube;
    [SerializeField] private Transform Real_Cube;

    private bool isCanMove = true;
    private bool isCanRot = true;

    [Header("ȿ��")]
    [SerializeField] private float EffectPos_Y = 0.25f;
    [SerializeField] private float Effect_speed = 1.5f;

    //�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�
    private bool isFalling = false;
    
    private Rigidbody player_r;

    //�ʱ� ��ġ ����
    private Vector3 Origin_pos;
    //�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�



    private Timemanager timemanager;
    private CameraManager Camera;

    private void Start()
    {
        timemanager = FindObjectOfType<Timemanager>();
        Camera = FindObjectOfType<CameraManager>();
        //�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�
        player_r = GetComponentInChildren<Rigidbody>();
        Origin_pos = transform.position;

    }

    private void Update()
    {
        //�Է� ���� ��, Ÿ�̹� ����
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    timemanager.Check_Timming();
        //}
        Check_Falling();
        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D))
        {
            if (isCanMove && isCanRot)
            {
                AudioManager.instance.PlaySFX("Clap");

                Clac();

                if (timemanager.Check_Timming())
                {
                    StartAction();
                }
            }
        }
    }

    //���� ��� (�÷��̾� ������ ��ǥ���� ���)
    private void Clac()
    {
        //Input�� ���� ������ -> ���⺤��
        Input_Direction.Set
               (
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical")
            );

        //�̵� ��ǥ�� ���
        Dest_pos =
            transform.position +
            new Vector3
            (Input_Direction.x,
            0,
            Input_Direction.z);

        //ȸ�� ��ǥ�� ���
        //�¿� -> z��
        //�յ� -> x��
        Input_rotDirection =
            new Vector3
            (-Input_Direction.z,
            0,
            Input_Direction.x);
        //��ǥ ȸ������ ������ ���� ���ؼ� ������ transform�� ���� ����
        Fake_Cube.RotateAround(transform.position,
            Input_rotDirection,
            spinSpeed);
        Dest_rot = Fake_Cube.rotation;
    }


    private void StartAction()
    {
        StartCoroutine(Move_co());
        StartCoroutine(Spin_co());
        StartCoroutine(Effect_co());
        StartCoroutine(Camera.zoomCam_co());
    }

    private IEnumerator Move_co()
    {
        isCanMove = false;
        //          ��> ũ�� �Ӽ��� ����ϴ� ��� ���� ũ�⸦ ���.
        while (Vector3.SqrMagnitude(transform.position - Dest_pos) >= 0.001f)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, Dest_pos,
                MoveSpeed * Time.deltaTime); //deltaTime : ������ ���� �ð�
            yield return null; //null�� ���� ���� : �ڷ�ƾ �ȿ��� ��Ȯ�� �ð�(�� ������, �� ������)�� ������ֱ� ����

        }
        transform.position = Dest_pos;
        isCanMove = true;
    }

    private IEnumerator Spin_co()
    {
        isCanRot = false;
        //             ��>�ޱ� ��
        while (Quaternion.Angle(Real_Cube.rotation, Dest_rot) > 0.5f)
        {
            Real_Cube.rotation =
                    Quaternion.RotateTowards(Real_Cube.rotation, Dest_rot,
                    spinSpeed * Time.deltaTime);
            yield return null;
        }
        Real_Cube.rotation = Dest_rot;
        isCanRot = true;
    }

    private IEnumerator Effect_co()
    {
        while (Real_Cube.position.y < EffectPos_Y)
        {
            Real_Cube.position += new Vector3(0, Effect_speed * Time.deltaTime, 0);
            yield return null;
        }
        while (Real_Cube.position.y > 0)
        {
            Real_Cube.position -= new Vector3(0,Effect_speed * Time.deltaTime, 0);
            yield return null;
        }
        Real_Cube.localPosition = Vector3.zero;
    }

    private void Check_Falling()
    {
        if (!isFalling && isCanMove)
        {
            if (!Physics.Raycast(transform.position, Vector3.down, 1.1f)) //����Ǵ°� ���ٸ�
            {
                Falling();
                Debug.Log("������");
            }
        }
    }

    private void Falling()
    {

        isFalling = true;
        player_r.useGravity = true;
        player_r.isKinematic = false;
    }

    public void Reset_Falling()
    {
        AudioManager.instance.PlaySFX("Falling");
        isFalling = false;
        player_r.useGravity = false;
        player_r.isKinematic = true;

        transform.position = Origin_pos;
        Real_Cube.localPosition = Vector3.zero;
    }
}
