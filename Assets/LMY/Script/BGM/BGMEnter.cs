using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 트리거 배경음악 재생기 : 스크립트#2. 트리거측
// 마을에 진입할 때 트리거에 닿으면 배경음악을 재생
public class BGMEnter : MonoBehaviour
{
    // Inspector 영역에 표시할 배경음악 이름
    public string bgmName = "";

    private GameObject CamObject;

    void Start()
    {
        CamObject = GameObject.Find("MainCamera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            CamObject.GetComponent<PlayMusicOperator>().PlayBGM(bgmName);
    }
}
