using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelUpUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;

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
        StartCoroutine(EndLevelUp());
        levelText.text = level.ToString();
    }

    IEnumerator EndLevelUp()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
}
