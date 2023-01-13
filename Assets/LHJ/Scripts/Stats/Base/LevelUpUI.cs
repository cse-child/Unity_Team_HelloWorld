using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LevelUpUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public Text[] texts = new Text[5];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LevelUp(int level)
    {
        gameObject.SetActive(true);
        levelText.text = level.ToString();
        texts[0].text = "+ " + LevelUpDataManager.instance.GetLevelUpData(level).level.ToString();
        texts[1].text = "+ " + LevelUpDataManager.instance.GetLevelUpData(level).maxHp.ToString();
        texts[2].text = "+ " + LevelUpDataManager.instance.GetLevelUpData(level).maxMp.ToString();
        texts[3].text = "+ " + LevelUpDataManager.instance.GetLevelUpData(level).baseAtk.ToString();
        texts[4].text = "+ " + LevelUpDataManager.instance.GetLevelUpData(level).baseDef.ToString();
        StartCoroutine(EndLevelUp());
    }

    IEnumerator EndLevelUp()
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.SetActive(false);
    }
}
