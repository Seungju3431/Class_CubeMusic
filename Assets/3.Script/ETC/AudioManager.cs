using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������ ����ȭ
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
        //�ѤѤѤѤѤѤѤѤѤѤѤѾ���
        Debug.Log($"{name}�� �����ϴ�");
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
                Debug.Log("��� �÷��̾ ��� �� �Դϴ�.");
                return;
            }
        }
        Debug.Log($"PlaySFX -> {name}�� �����ϴ�.");
    }
}
