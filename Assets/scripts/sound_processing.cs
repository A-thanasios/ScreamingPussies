using UnityEngine;

[RequireComponent(typeof(AudioSource))]
//ensures theres an audio source attached, if not should create one and prevent runtime err
public class VoicePitchDetector : MonoBehaviour
/*MonoBehaviour is the base class for all Unity scripts, allowing attachment
to GameObjects and providing access to events like Start and Update*/
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float up = 500;
    
    public string selectedMic;
    //stores the name of the mic, should be autoselected
    public float[] spectrum = new float[1024];
    //array for storing fft results
    private AudioSource audioSource;
    //will capture, loop and playback the mic input

    void Start()
    {
        if (Microphone.devices.Length > 0)
        //get available mics
        {
            selectedMic = Microphone.devices[0];
            Debug.Log("Using mic: " + selectedMic);
            //picks the first (position 0) found mic
        }
        else
        {
            Debug.LogError("No microphone detected!");
            return;
        }

        audioSource = GetComponent<AudioSource>();
        //grabs the audiosource

        // Start recording from mic
        audioSource.clip = Microphone.Start(
            selectedMic, 
            true,
            1,
            AudioSettings.outputSampleRate);
        //true=loop recording
        //1=recording 1 sec audio
        //AudioSettings.outputSampleRate=match system sample rate (usually 44100 or 48000 Hz)
        //sends the input to audioSource.clip
        audioSource.loop = true;

        // Wait until mic starts recording
        while (!(Microphone.GetPosition(selectedMic) > 0)) { }

        audioSource.Play();
        audioSource.mute = false;
    }

    void Update()
    //called once every frame
    {
        // Analyze frequency spectrum
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
        //fills the spectrum with fft data
        //0=left audio channel
        /*FFTWindow.BlackmanHarris = a windowing function that smooths 
        the edges of the signal to reduce noise/artifacts in the FFT*/

        // Parameters
    float minFreq = 80f;     // Ignore below this frequency
    float threshold = 0.001f; // Minimum amplitude to count as a peak

    // Convert minFreq to index
    int minIndex = Mathf.FloorToInt(minFreq * spectrum.Length * 2 / AudioSettings.outputSampleRate);

    float detectedFreq = -1f;

    for (int i = minIndex; i < spectrum.Length - 1; i++)
    {
        // Simple peak detection: a bin is higher than its neighbors and above threshold
        if (spectrum[i] > threshold && spectrum[i] > spectrum[i - 1] && spectrum[i] > spectrum[i + 1])
        {
            detectedFreq = i * AudioSettings.outputSampleRate / 2 / spectrum.Length;
            break; // Stop at first valid peak
        }
    }
    
    float freq = detectedFreq;
    float upThresh = up;

    if (detectedFreq > 0)
            Debug.Log(detectedFreq);
    if (freq <
        // Interpret frequency as commands - ???
        100)
    {
    }
    else if (freq < up)
    {
        Debug.Log("GO UP");
        rb.position += Vector2.up * moveSpeed;
    }
    else if (freq > up + 1)
    {
        Debug.Log("GO DOWN");
        rb.position += Vector2.down * moveSpeed;
    }
    }
}