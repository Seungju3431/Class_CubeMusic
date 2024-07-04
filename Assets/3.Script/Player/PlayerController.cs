using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동")]
    [SerializeField] private float MoveSpeed = 3f;

    //키보드 입력과 실제 플레이어의 움직임이 다름.
    [SerializeField] private Vector3 Input_Direction = new Vector3();//키보드
    [SerializeField] private Vector3 Dest_pos = new Vector3();//실제 움직이는 방향

    [Header("회전")]
    [SerializeField] private float spinSpeed = 270f;
    [SerializeField] private Vector3 Input_rotDirection = new Vector3();
    [SerializeField] private Quaternion Dest_rot = new Quaternion();

    [SerializeField] private Transform Fake_Cube;
    [SerializeField] private Transform Real_Cube;

    private bool isCanMove = true;
    private bool isCanRot = true;

    [Header("효과")]
    [SerializeField] private float EffectPos_Y = 0.25f;
    [SerializeField] private float Effect_speed = 1.5f;

    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    private bool isFalling = false;
    
    private Rigidbody player_r;

    //초기 위치 저장
    private Vector3 Origin_pos;
    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ



    private Timemanager timemanager;
    private CameraManager Camera;

    private void Start()
    {
        timemanager = FindObjectOfType<Timemanager>();
        Camera = FindObjectOfType<CameraManager>();
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        player_r = GetComponentInChildren<Rigidbody>();
        Origin_pos = transform.position;

    }

    private void Update()
    {
        //입력 했을 때, 타이밍 판정
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

    //방향 계산 (플레이어 움직임 목표값을 계산)
    private void Clac()
    {
        //Input에 따른 방향계산 -> 방향벡터
        Input_Direction.Set
               (
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical")
            );

        //이동 목표값 계산
        Dest_pos =
            transform.position +
            new Vector3
            (Input_Direction.x,
            0,
            Input_Direction.z);

        //회전 목표값 계산
        //좌우 -> z축
        //앞뒤 -> x축
        Input_rotDirection =
            new Vector3
            (-Input_Direction.z,
            0,
            Input_Direction.x);
        //목표 회전값을 가지고 오기 위해서 임의의 transform을 먼저 돌림
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
        //          ┌> 크기 속성을 사용하는 대신 제곱 크기를 계산.
        while (Vector3.SqrMagnitude(transform.position - Dest_pos) >= 0.001f)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, Dest_pos,
                MoveSpeed * Time.deltaTime); //deltaTime : 프레임 간의 시간
            yield return null; //null을 쓰는 이유 : 코루틴 안에서 정확한 시간(전 프레임, 앞 프레임)을 만들어주기 위해

        }
        transform.position = Dest_pos;
        isCanMove = true;
    }

    private IEnumerator Spin_co()
    {
        isCanRot = false;
        //             ┌>앵글 비교
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
            if (!Physics.Raycast(transform.position, Vector3.down, 1.1f)) //검출되는게 없다면
            {
                Falling();
                Debug.Log("떨어짐");
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
