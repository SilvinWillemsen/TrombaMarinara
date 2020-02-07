using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class DampingFinger : MonoBehaviour
{
    public AudioMixer masterMixer;

    float minPos = 0.0f;
    float maxPos = 0.5f;
    

    float curPos = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        masterMixer.GetFloat("DampFinger", out curPos);

        // if up (Y) button is pressed
        if (curPos > minPos)
        {

        }
        // else if down (X) button is pressed
        
    }
}
