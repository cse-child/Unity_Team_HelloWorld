using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour
{
    float curHP;
    int maxHP;
    string monsterName;

    public RectTransform curHpImage;
    public Text nameText;

    // Start is called before the first frame update
    void Start()
    {
        curHpImage = transform.Find("CurHP").GetComponent<RectTransform>();
        nameText = transform.Find("Name").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        curHpImage.sizeDelta = new Vector2(350.0f / maxHP * curHP, 40.0f);
        nameText.text = monsterName;
    }

    public void SetMonster(float curHP, int maxHP, string name)
    {
        this.curHP = curHP;
        this.maxHP = maxHP;
        this.monsterName = name;
    }
}
