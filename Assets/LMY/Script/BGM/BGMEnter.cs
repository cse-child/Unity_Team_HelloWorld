using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ʈ���� ������� ����� : ��ũ��Ʈ#2. Ʈ������
// ������ ������ �� Ʈ���ſ� ������ ��������� ���
public class BGMEnter : MonoBehaviour
{
    // Inspector ������ ǥ���� ������� �̸�
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
