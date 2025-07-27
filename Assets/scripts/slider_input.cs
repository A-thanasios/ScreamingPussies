using UnityEngine;
using UnityEngine.UI;

public class PitchListener : MonoBehaviour
{
    //public Slider Slider;  // Assign in inspector
    //public float maxPitch = 1300f;  // Set according to your expected pitch range

    [SerializeField] private Slider Slider;      // Assign your slider here
    [SerializeField] private Text pitchValueText;     // Assign a UI Text for showing pitch number
    [SerializeField] private float maxPitch = 1300f;  // Max expected pitch for normalization

    // Colors for low and high pitch ranges
    [SerializeField] private Color lowPitchColor = Color.blue;
    [SerializeField] private Color highPitchColor = Color.magenta;

    private Image fillImage;  // The fill area of the slider to tint

    private void Awake()
    {
        // Cache the slider's fill image to change its color
        fillImage = Slider.fillRect.GetComponent<Image>();

        if (fillImage == null)
    {
        Debug.LogError("Fill Rect does not have Image component!");
    }
    else
    {
        Debug.Log("fillImage assigned successfully.");
    }
    }

    private void OnEnable()
    {
        if (VoicePitchDetector.Instance != null)
        {
            VoicePitchDetector.Instance.OnPitchDetected += HandlePitchDetected;
        }
    }

    private void OnDisable()
    {
        if (VoicePitchDetector.Instance != null)
        {
            VoicePitchDetector.Instance.OnPitchDetected -= HandlePitchDetected;
        }
    }

    private void HandlePitchDetected(object sender, float pitch)
    {
        // Do whatever you want with the pitch, for example update a slider
        Slider.value = Mathf.Clamp01(pitch / maxPitch);
        // Debug.Log($"Pitch detected: {pitch}");

        Debug.Log($"Pitch detected: {pitch}");

        float normalized = Mathf.Clamp01(pitch / maxPitch);
        Slider.value = normalized;
        if (pitchValueText)
            pitchValueText.text = $"{pitch:F0} Hz";

        fillImage.color = Color.Lerp(lowPitchColor, highPitchColor, normalized);
    }
}
