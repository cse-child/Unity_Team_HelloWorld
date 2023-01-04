using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateUI : MonoBehaviour
{
    public TextMeshProUGUI maxHpText;
    public TextMeshProUGUI maxMpText;
    public TextMeshProUGUI curExpText;
    public TextMeshProUGUI curAtkText;
    public TextMeshProUGUI curDefText;
    PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        maxHpText = transform.Find("MaxHP").GetComponent<TextMeshProUGUI>();
        maxMpText = transform.Find("MaxMP").GetComponent<TextMeshProUGUI>();
        curExpText = transform.Find("EXP").GetComponent<TextMeshProUGUI>();
        curAtkText = transform.Find("ATK").GetComponent<TextMeshProUGUI>();
        curDefText = transform.Find("DEF").GetComponent<TextMeshProUGUI>();
        playerState = FindObjectOfType<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        maxHpText.text = playerState.maxHP.ToString();
        maxMpText.text = playerState.maxMP.ToString();
        curExpText.text = playerState.curExp.ToString();
        curAtkText.text = playerState.curAtk.ToString();
        curDefText.text = playerState.curDef.ToString();
    }
}
