using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThereminAudioHandler : MonoBehaviour
{
    public GameObject PitchAntennaContactPoint;
    public GameObject PitchAntenna;
    public GameObject VolumeAntena;
    public GameObject RightContactPoint;
    public GameObject ContactPoint;

    public GameObject RightHand;
    public GameObject LeftHand;

    float pitch = 1.0f;
    float volume = 0.0f;

    private Vector3 pitchCoordinates;
    private GameObject nearestHand;

    public bool DrawLine;
    private bool calibrating = true;
    private int stage = 0;
    private LineRenderer linePitch;

    void Start()
    {
        //CalibrateHands();

        // Add a Line Renderer to the GameObject
        linePitch = this.gameObject.AddComponent<LineRenderer>();
        // Set the width of the Line Renderer
        linePitch.SetWidth(0.001F, 0.001F);
        // Set the number of vertex fo the Line Renderer
        linePitch.SetVertexCount(2);
        // Create a default material and give it a color
        linePitch.material.color = Color.cyan;

    }

    // Update is called once per frame
    void Update()
    {
        MovePitchContactPoint();

        linePitch.enabled = DrawLine;

        //print(OVRInput.GetDown(OVRInput.Button.One));

    }

    float CalculatePitch()
    {
        //something with distance and normalization
        //returns the pitch, over 5 octaves

        float maxPitch = 3.0f;
        float minPitch = 0.05f;

        if (NearestPoint().activeSelf)
        {
            pitch = Vector3.Distance(NearestPoint().transform.position - nearestHand.GetComponent<Renderer>().bounds.extents, PitchAntennaContactPoint.transform.position);
            GetComponent<AudioSource>().pitch = 3 - Map(pitch,0.1f,0.5f,0.01f,3.0f);
        }

        

        return (pitch);

    }

    float CalculateAmplitude(){
        //something with distance and normalization
        //returns the volume, maybe some smoothing so it does not cause audible jitter.
        if (ContactPoint.activeSelf){
            volume = Vector3.Distance(ContactPoint.transform.position, VolumeAntena.transform.position);
            GetComponent<AudioSource>().volume = Map(volume,0.13f, 0.5f, 0, 1);
            //print(volume);
            
        }

        if (Input.GetKeyDown("space"))
        {
            print("pitch is " + volume);
        }
        return (volume);
    }

    void CalibrateHands() {
        //Something to run when the app starts, to calibrate the theremin hands position relative to the body and antennae
        float ampMin;
        float ampMax;
        float pitchMin;
        float pitchMax;

        while (calibrating) { 
            if (Input.GetKeyDown("N"))
            {
                switch (stage)
                {
                    case 0:
                        ampMin = Vector3.Distance(ContactPoint.transform.position, VolumeAntena.transform.position);
                        stage++;
                        print(stage);
                        break;
                    case 1:
                        ampMax = Vector3.Distance(ContactPoint.transform.position, VolumeAntena.transform.position);
                        stage++;
                        break;
                    case 2:
                        pitchMin = Vector3.Distance(NearestPoint().transform.position - nearestHand.GetComponent<Renderer>().bounds.extents, PitchAntennaContactPoint.transform.position);
                        stage++;
                        print(stage);
                        break;
                    case 3:
                        pitchMax = Vector3.Distance(NearestPoint().transform.position - nearestHand.GetComponent<Renderer>().bounds.extents, PitchAntennaContactPoint.transform.position);
                        stage++;
                        print(stage);
                        break;
                    default:
                        print("calibration completed");
                        calibrating = false;
                        break;
                }
            }

        }
    }

    GameObject NearestPoint()
    {
        //check for the closest hand to the antenna = swap the hand if they cross. use sqrMagnitude

        Vector3 rightHandDistance = RightContactPoint.transform.position - PitchAntennaContactPoint.transform.position;
        Vector3 leftHandDistance = ContactPoint.transform.position - PitchAntennaContactPoint.transform.position;

        if (rightHandDistance.sqrMagnitude < leftHandDistance.sqrMagnitude) {
            nearestHand = RightHand;
            return (RightContactPoint);
        }
        else
        {
            nearestHand = LeftHand;
            return (ContactPoint);

        }
        
        }

    void MovePitchContactPoint() {
        // Something that moves an object along the pitch antenna, in order to be closes to the NearestPitchPoint()
        float topBound = PitchAntenna.GetComponent<Renderer>().bounds.center.y + PitchAntenna.GetComponent<Renderer>().bounds.extents.y; //needs a scallar
        float bottomBound = PitchAntenna.GetComponent<Renderer>().bounds.center.y -PitchAntenna.GetComponent<Renderer>().bounds.extents.y;
        // Take only the Y(vertical) coordinate from the hand, and store it in a new vector3    
        pitchCoordinates = new Vector3 (PitchAntennaContactPoint.transform.position.x, NearestPoint().transform.position.y, PitchAntennaContactPoint.transform.position.z);

        // bound the movement to the newly found limits
        if (pitchCoordinates.y < bottomBound)
            pitchCoordinates.y = bottomBound;
        else if (pitchCoordinates.y > topBound)
            pitchCoordinates.y = topBound;
        // Move the object 
        PitchAntennaContactPoint.transform.position = pitchCoordinates;

        linePitch.SetPosition(1, NearestPoint().transform.position);
        linePitch.SetPosition(0, PitchAntennaContactPoint.transform.position);

    }

    void FadeAudio()
    {
        //As the name says, fade audio out when any of the hands are disconnected

    }

    float Map(float value, float low1, float high1, float low2, float high2)
    {
        return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
    }
}
