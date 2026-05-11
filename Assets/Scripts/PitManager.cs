using UnityEngine;
using TMPro;

public class PitManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI timerText;

    [Header("Car State")]
    [Tooltip("Drag all 4 wheel objects (the ones with the WheelNut script) here.")]
    public WheelNut[] allWheels; 
    
    [Header("Feedback")]
    public AudioSource successAudio;

    private float currentTime = 0f;
    private bool isTimerRunning = false;
    private bool isPitstopComplete = false;

    void Start()
    {
        StartTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void StartTimer()
    {
        currentTime = 0f;
        isTimerRunning = true;
        isPitstopComplete = false;
        timerText.color = Color.white;
    }

    public void AttemptRelease()
    {
        if (isPitstopComplete) return; 

        if (allWheels == null || allWheels.Length == 0)
        {
            Debug.LogError("CRASH PREVENTED: allWheels array is empty! Assign it in the Inspector.");
            return;
        }
        if (timerText == null)
        {
            Debug.LogError("CRASH PREVENTED: Timer Text is missing! Assign it in the Inspector.");
            return;
        }

        if (AreAllWheelsLocked())
        {
            isTimerRunning = false;
            isPitstopComplete = true;
            
            timerText.color = Color.green; 
            if (successAudio != null) successAudio.Play();
            
            Debug.Log("Pitstop Complete! Time: " + currentTime.ToString("F2"));
        }
        else
        {
            timerText.color = Color.red; 
            Invoke(nameof(ResetTimerColor), 0.5f); 
            Debug.LogWarning("Car dropped! Tires are not secured!");
        }
    }

    private bool AreAllWheelsLocked()
    {
        foreach (var nut in allWheels)
        {
            if (nut == null || !nut.isLocked)
            {
                return false;
            }
        }
        return true;
    }

    private void UpdateTimerDisplay()
    {
        timerText.text = currentTime.ToString("F2");
    }

    private void ResetTimerColor()
    {
        if (isTimerRunning) timerText.color = Color.white;
    }
}