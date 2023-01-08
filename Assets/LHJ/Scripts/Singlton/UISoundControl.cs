using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundControl : MonoBehaviour
{
    private static UISoundControl _instance;
    public static UISoundControl instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = GameObject.Find("UI");
                _instance = obj.AddComponent<UISoundControl>();
            }
            return _instance;
        }
    }

    public Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        AudioClip aaa;
       // aaa.
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
