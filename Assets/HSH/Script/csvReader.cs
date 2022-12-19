using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class csvReader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Test();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Test()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/HSH/DataTable/" + "ItemData.csv");

        bool endOfFile = false;
        while (!endOfFile)
        {
            string dataString = sr.ReadLine();
            if(dataString == null)
            {
                endOfFile = true;
                break;
            }
            var dataValues = dataString.Split(',');
            for(int i = 0; i < dataValues.Length; i++)
            {
                Debug.Log("v: " + i.ToString() + " " + dataValues[i].ToString());
            }
        }
    }
}
