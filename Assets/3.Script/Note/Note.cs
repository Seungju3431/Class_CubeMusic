using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField] private float note_speed = 400f;

    private Image img;

    private void OnEnable()
    {
        img = GetComponent<Image>();
        img.enabled = true;
    }

    public void HiedNote()
    {
        img.enabled = false;
    }

    public bool GetNoteflag()
    {
        return img.enabled;
    }

    private void Update()
    {
        //Canvas안에 자식객체가 
        transform.localPosition +=
            Vector3.right * note_speed * Time.deltaTime;
    }

}
