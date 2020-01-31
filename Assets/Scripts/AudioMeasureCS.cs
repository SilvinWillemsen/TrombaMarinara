using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class AudioMeasureCS : MonoBehaviour
{
    public float RmsValue;
    public float normalizedRMS;
    public float DbValue;
    public float PitchValue;
    public int MIDI;
    public float MIDI_rest;
    float rmsMin;
    float rmsMax;

    private const int QSamples = 1024;
    private const float RefValue = 0.1f;
    private const float Threshold = 0.02f;

    float[] _samples;
    private float[] _spectrum;
    private float _fSample;

    private AudioSource audio;

    void Start()
    {
        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }

        audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 10, 44100);
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;

        audio = this.GetComponent<AudioSource>();

        audio.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { } 
        audio.Play(); // play audio through audio source(so it can be analyzed). Latency not measured
    }

    void Update()
    {
        AnalyzeSound();
    }

    void AnalyzeSound()
    {
        GetComponent<AudioSource>().GetOutputData(_samples, 0); // fill array with samples
        int i;
        float sum = 0;

      //#### RMS, DB, Amplitude calculations
        for (i = 0; i < QSamples; i++)
        {
            sum += _samples[i] * _samples[i]; // sum squared samples
        }
        RmsValue = Mathf.Sqrt(sum / QSamples); // rms = square root of average
        DbValue = 20 * Mathf.Log10(RmsValue / RefValue); // calculate dB
        if (DbValue < -160) DbValue = -160; // clamp it to -160dB min
                                            // get sound spectrum
        if (RmsValue > rmsMax)  //get min and max RMS values
            rmsMax = RmsValue;
        if (RmsValue < rmsMin)
            rmsMin = RmsValue;
        
        normalizedRMS = (RmsValue + rmsMin) / (rmsMax - rmsMin); //normalize RMS values

        if (normalizedRMS > 1) normalizedRMS = 1.0f; //clamp it to 0-1 (reduces some jitter)
        if (normalizedRMS < 0.001) normalizedRMS = 0.0f;
        //Debug.Log("RMS Min = " + rmsMin + ",RMS Max = " + rmsMax + ", normalizedRMS = " + normalizedRMS);

        //#### Pitch detectrion calculations
        GetComponent<AudioSource>().GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        var maxN = 0;
        for (i = 0; i < QSamples; i++)
        { // find max 
            if (!(_spectrum[i] > maxV) || !(_spectrum[i] > Threshold))
                continue;

            maxV = _spectrum[i];
            maxN = i; // maxN is the index of max
        }
        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < QSamples - 1)
        { // interpolate index using neighbours
            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
            var dR = _spectrum[maxN + 1] / _spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        PitchValue = freqN * (_fSample / 2) / QSamples; // convert index to frequency
        MIDI = Mathf.RoundToInt( 69 + 12 * Mathf.Log(PitchValue / 440, 2)); // convert to midi
        MIDI_rest = 69 + 12 * Mathf.Log(PitchValue / 440, 2) - MIDI; // distance from nearest midi note
        


        //Debug.Log("Number of Microphones: " + " " + Microphone.devices.Length + ", sound dB = " + DbValue + ", pitch = " + PitchValue);
    }
}