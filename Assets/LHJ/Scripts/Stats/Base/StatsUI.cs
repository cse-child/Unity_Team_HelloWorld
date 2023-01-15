using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public PlayerState playerState;
    //public GameObject goldUI;

    private GameObject curHP;
    private GameObject maxHP;
    private GameObject curMP;
    private GameObject maxMP;
    private GameObject curExp;
    private GameObject MaxExp;
    private TextMeshProUGUI curLevel;

    public void Start()
    {
        curHP = gameObject.transform.Find("CurHP").gameObject;
        curMP = gameObject.transform.Find("CurMp").gameObject;
        maxHP = gameObject.transform.Find("MaxHP").gameObject;
        maxMP = gameObject.transform.Find("MaxMp").gameObject;
        curExp = gameObject.transform.Find("curExp").gameObject;
        MaxExp = gameObject.transform.Find("MaxExp").gameObject;
        curLevel = gameObject.transform.Find("UICenter").Find("LevelText").GetComponent<TextMeshProUGUI>();
        playerState = FindObjectOfType<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        StateUpdate();
    }

    void StateUpdate()
    {
        //150.0f 부분 max부분으로
        curHP.GetComponent<RectTransform>().sizeDelta = new Vector2(494.0f / playerState.maxHP * playerState.curHp, 38.0f);
        curMP.GetComponent<RectTransform>().sizeDelta = new Vector2(494.0f / playerState.maxMP * playerState.curMp, 38.0f);
        curExp.GetComponent<RectTransform>().sizeDelta = new Vector2(3082.0f / playerState.maxEXP * playerState.curExp, 38.0f);
        curLevel.text = playerState.level.ToString();
       // goldUI.GetComponent<Text>().text = playerState.gold.ToString();
    }

    public void LevelUpUI()
    {

    }
}
