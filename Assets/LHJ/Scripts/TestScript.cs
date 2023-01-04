using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject aa;
    private void Awake()
    {
        QuestDataManager.instance.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            QuestDataManager.instance.ClearQuest("qst_005");
        if(Input.GetKeyDown(KeyCode.A))
            QuestDataManager.instance.AddQuest("qst_005");

        if (Input.GetKeyDown(KeyCode.F))
            if(aa.gameObject.activeSelf)
                  aa.gameObject.SetActive(false);
              else
                  aa.gameObject.SetActive(true);
    }
}
