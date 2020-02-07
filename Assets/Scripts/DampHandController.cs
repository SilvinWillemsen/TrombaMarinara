using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DampHandController : MonoBehaviour
{
    public AudioMixer masterMixer;
    public float speed = 0.01f;

    private float minStringPos = 0.125f;
    private float maxStringPos = 0.5f;

    private float curStringPos = 0.5f;

    private bool upButtonPressed = false;
    private bool downButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        masterMixer.GetFloat("DampFinger", out curStringPos);

        //OVRInput.Get(OVRInput.Button.Two

        if (Input.GetKeyDown("joystick button 2"))
        {
            print("up button pressed!");
            curStringPos = changeStringPos(curStringPos, true);
       }


        if (Input.GetKeyDown("joystick button 3"))// && downButtonPressed == false)
        {
            print("down button pressed!");
            curStringPos = changeStringPos(curStringPos, false);
            
        }

        //print(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch).y);
        curStringPos -= OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch).y * speed;
        if (curStringPos > maxStringPos)
            curStringPos = maxStringPos;
        else if (curStringPos < minStringPos)
            curStringPos = minStringPos;

        masterMixer.SetFloat("DampFinger", curStringPos);
        //masterMixer.SetFloat("DampFingerForce", Mathf.Pow(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch), 7));

        print("first: "+ OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch)+", second: "+ OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) + ", third: " + OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger));
    }

    float changeStringPos(float cur, bool dir)
    {
        //do some wizardry
        if (dir && cur > minStringPos) // +0.001
        {
            for (int i = 2; i < 1.0f / minStringPos; ++i)
            {

                if (1.0f / cur + 0.01f > i && 1.0f / cur - 0.01f < i) // if (1/cur == i) floats are equal
                {
                    return 1.0f / (i + 1);
                }
                else if (1.0f / cur - 0.01f > i && 1.0f / cur + 0.01f < i+1) // if cur is in between two 1/integer points
                {
                    return 1.0f / (i + 1);
                }
            }
        }
        else if (!dir && cur < maxStringPos) // -0.001
        {
            print("reached down function");
            for (int i = 2; i < 1.0f / minStringPos + 0.01f; ++i)
            {
                if (1.0f / cur + 0.01f > i && 1.0f / cur - 0.01f < i) // if (1/cur == i) floats are equal
                {
                    return 1.0f / (i - 1);
                }
                else if (1.0f / cur > i && 1.0f / cur < i + 1) // if cur is in between two 1/integer points
                {
                    return 1.0f / (i);
                }
            }
        }
        print("did not reach forloop");
        return curStringPos;
    }
}
