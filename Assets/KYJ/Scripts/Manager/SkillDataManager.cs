using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemDataManager;
//using static UnityEditor.Progress;
//using static System.Progress<>;

public class SkillDataManager : MonoBehaviour
{
    private static SkillDataManager _instance;
    public static SkillDataManager instance
    {
        get
        {
            if(_instance == null )
            {
                GameObject obj = new GameObject("SkillDataManager");
                _instance = obj.AddComponent<SkillDataManager>();
            }
            return _instance;
        }
    }

    public struct SkillData
    {

        public int skillNum;
        public string skillName;
        public int decreaseMP;
        public float coolTime;
        public int damage;
        public int buff;
        public bool learning;
        public int level;
        public Sprite image;
        public string explanation;
    }

    List<SkillData> skills = new List<SkillData>();

    // 하이어라키 확인용
    public List<string> skillNames = new List<string>();
    

    public void Awake()
    {
    }

    public void LoadItemData()
    {
        string temp = System.IO.File.ReadAllText("Assets/KYJ/Resources/TextData/SkillDataTable.csv");
        temp = temp.Replace("\r\n", "\n");
        string[] row = temp.Split("\n");
        for (int i = 1; i < row.Length; i++)
        {
            string[] col = row[i].Split(",");

            SkillData tempData;
            tempData.skillNum = int.Parse(col[0]);
            tempData.skillName = col[1];
            tempData.decreaseMP = int.Parse(col[2]);
            tempData.coolTime = float.Parse(col[3]);
            tempData.damage = int.Parse(col[4]);
            tempData.buff = int.Parse(col[5]);
            tempData.learning = int.Parse(col[6]) == 1 ? true : false;
            tempData.level = int.Parse(col[7]);

            // 현중님 소스 슥 긁어오기 ㅋ
            //바이트 데이터 이미지를 스프라이트로 변환  byte -> texture -> splite
            byte[] byteTexture = System.IO.File.ReadAllBytes(col[8]);
            Texture2D texture = new Texture2D(0, 0);
            texture.LoadImage(byteTexture);
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            tempData.image = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));

            tempData.explanation = col[9];

            skills.Add(tempData);
            skillNames.Add(tempData.skillName);
        }
    }

    public SkillData GetSkillData(int skillNum)
    {
        return skills[skillNum-1];
    }

    public List<SkillData> GetAllSkillData()
    {
        return skills;
    }
}
