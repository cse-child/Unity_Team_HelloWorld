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
                _instance = obj.GetComponent<UISoundControl>();
            }
            return _instance;
        }
    }


    public AudioClip[] sounds;
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundPlay(int num)
    {
        audioSource.clip = sounds[num];
        audioSource.Play();
    }
}
