using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//데이터 진열화
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { 
            Destroy(gameObject);
        }
        AutoSetting();
    }

    [Header("Audio Clip")]
    [SerializeField] private Sound[] BGM;
    [SerializeField] private Sound[] SFX;
    [Space(50f)]
    [Header("AudioSource")]
    [SerializeField] private AudioSource BGMPlayer;
    [SerializeField] private AudioSource[] SFXPlayer;

    private void AutoSetting()
    {
        BGMPlayer = transform.GetChild(0).GetComponent<AudioSource>();
        SFXPlayer = transform.GetChild(1).GetComponents<AudioSource>();
    }

    public void play_BGM(string name)
    {
        foreach (Sound s in BGM)
        {
            if (s.name.Equals(name))
            {
                BGMPlayer.clip = s.clip;
                BGMPlayer.Play();
                return;
            }
        }
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ없음
        Debug.Log($"{name}이 없습니다");
    }

    public void stopBGM()
    {
        BGMPlayer.Stop();
    }

    public void PlaySFX(string name)
    {
        foreach (Sound s in SFX)
        {
            if (s.name.Equals(name))
            {
                for (int i = 0; i < SFXPlayer.Length; i++)
                {
                    if (!SFXPlayer[i].isPlaying)
                    {
                        SFXPlayer[i].clip = s.clip;
                        SFXPlayer[i].Play();
                        return;
                    }
                }
                Debug.Log("모든 플레이어가 재생 중 입니다.");
                return;
            }
        }
        Debug.Log($"PlaySFX -> {name}이 없습니다.");
    }
}
