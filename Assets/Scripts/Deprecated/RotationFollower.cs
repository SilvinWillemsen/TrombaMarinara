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
    }


    //temp functions that are not in use now
    Vector3 Calibrate (Vector3 controllerRotation)
    {
        Vector3 callibrationOffset = new Vector3(0,0,0);
        if (OVRInput.Get(OVRInput.Button.One))
            return (callibrationOffset);

        else
            return (callibrationOffset) ;
    }

    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }
}
