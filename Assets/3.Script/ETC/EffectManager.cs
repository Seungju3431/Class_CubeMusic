using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{

    // animator -> 트리거 Hit 호출

    [SerializeField] private Animator Notehit_animator;
    [SerializeField] private Animator Judgement_animator;
    [SerializeField] private Image Jugement_img;

    private string Key = "Hit";

    [Header("Perfect -> Cool -> Good -> Bad")]
    [SerializeField] private Sprite[] Judgement_sprite; //HitEffect 소스이미지가 스프라이트여서 스프라이트 배열

    private void Awake()
    {
        //Notehit_animator = transform.GetChild(0).GetComponent<Animator>();
        //(Notehit_animator = null 상태였다가
        //transform.GetChild(0).GetComponent<Animator>(); 이후에 null이 없어지고 들어감
        //생성 파괴가 이루어져 리소스를 잡아먹기 때문에 가비지컬렉터의 수집 대상이 됨.
        //그래서 결론은 TryGetComponent가 GetComponet보다 빠르다.
        //위 코드 간략하게 
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
