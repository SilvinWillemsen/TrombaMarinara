using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickControl : MonoBehaviour
{
    public int maxClickRate;
    public int minClickRate;
    public int minClickVolume;
    public int maxClickVolume;
    public float minPitch;
    public float maxPitch;


    float clickRate;
    float midiError;
    float midiNote;

    GameObject audioHandler;
    AudioSource clickSource;

    // Use this for initialization
    void Start()
    {
        //Initialize
        StartCoroutine(Click(clickRate));
        audioHandler = GameObject.Find("AudioHandler");
        clickSource = GetComponent<AudioSource>();

        //Get midi data from the real theremin
        midiError = audioHandler.GetComponent<AudioMeasureCS>().MIDI_rest;
        midiNote = audioHandler.GetComponent<AudioMeasureCS>().MIDI;

        //Play audio
        clickSource.Play();
        clickSource.Play(44100);
        Click(ErrorClicks(midiError));
    }

    // Update is called once per frame
    void Update()
    {
        midiError = audioHandler.GetComponent<AudioMeasureCS>().MIDI_rest;
        midiNote = audioHandler.GetComponent<AudioMeasureCS>().MIDI;
    }

    //map a number from an interval, to another interval
    float map(float input, float inMin, float inMax, float outMin, float outMax)
    {
        float slope = (outMax - outMin) / (inMax - inMin);
        return (outMin + slope * (input - inMin));

    }

    float ErrorClicks(float midiError)
    {
        Debug.Log("ErrorClicks called, midiError is: " + midiError);


        float localError;
        if (Mathf.Abs(midiError) > 0.5)
            localError = 0.5f;
        else localError = midiError;

        //convert error to percentage values
        int percentageError = Mathf.RoundToInt(map(localError, -0.5f, 0.5f, 0, 100));

        // return converted percentage error to delay time for clickRate in Hz: delay = 1/desiredClicksPerSecond

        Debug.Log(("Delay time = " + 1/map(percentageError, 0, 100, minClickRate, maxClickRate)));
        return (1/map(percentageError, 0, 100, minClickRate, maxClickRate));
    }
    /*
    IEnumerator Click(float delay)
    {
        Debug.Log("Corutine Started");
        yield return new WaitForSeconds(delay);
        if (delay > 0)
        {
            clickSource = GetComponent<AudioSource>();
            //clickSource.pitch = map(Mathf.RoundToInt(map(Mathf.Abs(midiError), 0, 100)), minPitch, maxPitch);
            clickSource.volume = map(Mathf.Abs(midiError), 0, 0.5f, minClickVolume, maxClickVolume);
            clickSource.Play();
            clickSource.Play(44100);
            Debug.Log("Clicked");
        }
        else
        {
            StartCoroutine(Click(ErrorClicks(midiError)));
            Debug.Log("Corutine Ended");
        }
    }
    */
    IEnumerator Click(float delay)
    {
        Debug.Log("Corutine Started");
        yield return new WaitForSeconds(delay);
      
            clickSource = GetComponent<AudioSource>();
            //clickSource.pitch = map(Mathf.RoundToInt(map(Mathf.Abs(midiError), 0, 100)), minPitch, maxPitch);
            clickSource.volume = map(Mathf.Abs(midiError), 0, 0.5f, minClickVolume, maxClickVolume);
            clickSource.Play();
            clickSource.Play(44100);
            Debug.Log("Clicked");

        StartCoroutine(Click(ErrorClicks(midiError)));
        //StartCoroutine(Click(2));
        Debug.Log("Corutine Ended");
        
    }
}
