using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpDataManager : MonoBehaviour
{
    private static LevelUpDataManager _instance;
    public static LevelUpDataManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("LevelUpDataManager");
                _instance = obj.AddComponent<LevelUpDataManager>();
            }
            return _instance;
        }
    }

    public struct LevelUpData
    {
        public int level;
        public int maxHp;
        public int maxMp;
        public int baseAtk;
        public int baseDef;
    }

    List<LevelUpData> levelUpDatas = new List<LevelUpData>();

    public void Awake()
    {
    }

    public void LoadLevelUpData()
    {
        string temp = System.IO.File.ReadAllText("Assets/KYJ/Resources/TextData/LevelUpDataTable.csv");
        temp = temp.Replace("\r\n", "\n");
        string[] row = temp.Split("\n");
        for (int i = 1; i < row.Length; i++)
        {
            string[] col = row[i].Split(",");

            LevelUpData tempData;
            tempData.level = int.Parse(col[0]);
            tempData.maxHp = int.Parse(col[1]);
            tempData.maxMp = int.Parse(col[2]);
            tempData.baseAtk = int.Parse(col[3]);
            tempData.baseDef = int.Parse(col[4]);

            levelUpDatas.Add(tempData);
            //print("추가된 레벨업데이터: " + tempData.level);
        }
    }

    public LevelUpData GetLevelUpData(int level)
    {
        return levelUpDatas[level - 2];
    }
}
