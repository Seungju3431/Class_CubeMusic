using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{

    // animator -> Ʈ���� Hit ȣ��

    [SerializeField] private Animator Notehit_animator;
    [SerializeField] private Animator Judgement_animator;
    [SerializeField] private Image Jugement_img;

    private string Key = "Hit";

    [Header("Perfect -> Cool -> Good -> Bad")]
    [SerializeField] private Sprite[] Judgement_sprite; //HitEffect �ҽ��̹����� ��������Ʈ���� ��������Ʈ �迭

    private void Awake()
    {
        //Notehit_animator = transform.GetChild(0).GetComponent<Animator>();
        //(Notehit_animator = null ���¿��ٰ�
        //transform.GetChild(0).GetComponent<Animator>(); ���Ŀ� null�� �������� ��
        //���� �ı��� �̷���� ���ҽ��� ��ƸԱ� ������ �������÷����� ���� ����� ��.
        //�׷��� ����� TryGetComponent�� GetComponet���� ������.
        //�� �ڵ� �����ϰ� 
        transform.GetChild(0).TryGetComponent(out Notehit_animator);

        transform.GetChild(1).TryGetComponent(out Judgement_animator);
        transform.GetChild(1).TryGetComponent(out Jugement_img);

    }

    public void Judgement_Effect(int effect)
    {
        Jugement_img.sprite = Judgement_sprite[effect];
        Judgement_animator.SetTrigger(Key);
    }

    public void NoteHit_Effect()
    {
        Notehit_animator.SetTrigger(Key);
    }

}
