using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public PlayerState playerState;
    public GameObject goldUI;

    private GameObject curHP;
    private GameObject maxHP;
    private GameObject curMP;
    private GameObject maxMP;

    public void Start()
    {
        curHP = gameObject.transform.Find("CurHP").gameObject;
        curMP = gameObject.transform.Find("CurMp").gameObject;
        maxHP = gameObject.transform.Find("MaxHP").gameObject;
        maxMP = gameObject.transform.Find("MaxMp").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        StateUpdate();
    }

    void StateUpdate()
    {
        //150.0f 부분 max부분으로
        curHP.GetComponent<RectTransform>().sizeDelta = new Vector2(494.0f / 150.0f * playerState.curHp, 38.0f);
        curMP.GetComponent<RectTransform>().sizeDelta = new Vector2(494.0f / 150.0f * playerState.curMp, 38.0f);
        goldUI.GetComponent<Text>().text = playerState.gold.ToString();
    }
}
