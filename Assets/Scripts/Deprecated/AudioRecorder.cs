using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    AudioClip demoRecording;
    float[] samples = new float[60 * 44100 * 2];
    int recPos=0;
    bool recording = false;
   
    // Start is called before the first frame update
    void Start()
    {
        demoRecording = AudioClip.Create("demoRecording", 2048, 1, 44100, false);
        samples = new float[demoRecording.samples];
        GameObject.Find("AudioSource").GetComponent<AudioSource>().GetOutputData(samples, 1);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("r"))
        {
            recording = true;
        }
        if (recording)
        {
            recPos = recPos + 2048;
            GameObject.Find("AudioSource").GetComponent<AudioSource>().GetOutputData(samples, 1);
            demoRecording.SetData(samples, 2048);
            print("recording");
        }

        if (Input.GetKeyDown("p"))
        {
            recording = false;
            recPos = 0;
            AudioSource.PlayClipAtPoint(demoRecording, transform.position);
            print("playing");
        }
    }
}
    