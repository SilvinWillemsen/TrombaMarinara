using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFollower : MonoBehaviour
{
    public GameObject rightHandAnchor; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis((rightHandAnchor.transform.rotation.eulerAngles.x - 318.0f), new Vector3(-1.0f, 0.0f, 0.0f));
        //transform.rotation = Quaternion.AngleAxis((rightHandAnchor.transform.rotation.eulerAngles.y - 318.0f), new Vector3(0.0f, 0.0f, 0.0f));
        //print("controller button: "+OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch));
        print(rightHandAnchor.transform.rotation.eulerAngles);
    }

    Vector3 Calibrate (Vector3 controllerRotation)
    {
        Vector3 callibrationOffset = new Vector3(0,0,0);
        if (OVRInput.Get(OVRInput.Button.One))
            return (callibrationOffset);

        else
            return (callibrationOffset) ;
    }
}
