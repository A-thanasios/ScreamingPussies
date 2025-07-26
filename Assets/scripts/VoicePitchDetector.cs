using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VoicePitchDetector : MonoBehaviour
{
    public static VoicePitchDetector Instance { get; private set; }
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float up = 500;
    [SerializeField] private string selectedMic;
    [SerializeField] private float[] spectrum = new float[1024];
    
    public event EventHandler<float> OnPitchDetected;
    
    // Parameters
    [SerializeField] private float minFreq = 80f;     // Ignore below this frequency
    [SerializeField] private float threshold = 0.001f; // Minimum amplitude to count as a peak

    private int minIndex;
    private float detectedFreq;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one VoicePitchDetector in scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (StartMicrophone()) return;

        // Wait until a microphone starts recording
        while (!(Microphone.GetPosition(selectedMic) > 0)) { }

        StartAudioSource();
        
        minIndex = GetMinIndex();
    }



    private void Update()
    {
        AnalyzeFrequencySpectrum();
        
        detectedFreq = GetDetectedFrequency();
        
        if (detectedFreq > 0)
            OnPitchDetected?.Invoke(this, detectedFreq);
        
        //DebugMethod();
    }


    private bool StartMicrophone()
    {
        if (Microphone.devices.Length > 0)
        {
            selectedMic = Microphone.devices[0];
            Debug.Log("Using mic: " + selectedMic);
        }
        else
        {
            Debug.LogError("No microphone detected!");
            return true;
        }

        audioSource.clip = Microphone.Start(
            selectedMic, 
            true,
            1,
            AudioSettings.outputSampleRate);
        audioSource.loop = true;
        return false;
    }
    
    private void StartAudioSource()
    {
        audioSource.Play();
        audioSource.mute = false;
    }
    private void AnalyzeFrequencySpectrum()
    {
        //fills the spectrum with fft data
        //0=left audio channel
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
    }
    
    // Convert minFreq to index
    // if minFreq is not change during runtime, minIndex is const
    private int GetMinIndex() => Mathf.FloorToInt(minFreq * spectrum.Length * 2 / AudioSettings.outputSampleRate);
    
    private float GetDetectedFrequency()
    {
        float freq = -1f;

        for (int i = minIndex; i < spectrum.Length - 1; i++)
        {
            // Simple peak detection: a bin is higher than its neighbors and above threshold
            if (!(spectrum[i] > threshold) || !(spectrum[i] > spectrum[i - 1]) ||
                !(spectrum[i] > spectrum[i + 1])) continue;
            
            freq = i * AudioSettings.outputSampleRate / 2f / spectrum.Length;
            break; // Stop at the first valid peak
        }

        return freq;
    }
    
    private void DebugMethod()
    {
        // if (detectedFreq > 0)
        //     Debug.Log(detectedFreq);
        if (detectedFreq < 100) ;
        else if (detectedFreq < up)
            Debug.Log("GO LEFT");
        else if (detectedFreq < up + 100)
            Debug.Log("GO RIGHT");
        else if (detectedFreq < up + 200)
        {
            Debug.Log("GO UP");
        }
        else if (detectedFreq > up + 201)
        {
            Debug.Log("GO DOWN");
        }
    }
    
}