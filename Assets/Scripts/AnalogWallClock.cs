using System;
using UnityEngine;

public class AnalogWallClock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    void Update()
    {
        DateTime time = DateTime.Now;

        // Second hand (60 seconds = 360°)
        float secondsAngle = (time.Second / 60f) * 360f;

        // Minute hand (includes seconds for smooth movement)
        float minutesAngle = ((time.Minute + time.Second / 60f) / 60f) * 360f;

        // Hour hand (includes minutes for smooth movement)
        float hoursAngle = ((time.Hour % 12 + time.Minute / 60f) / 12f) * 360f;

        secondHand.localRotation = Quaternion.Euler(0f, 0f, secondsAngle);
        minuteHand.localRotation = Quaternion.Euler(0f, 0f, minutesAngle);
        hourHand.localRotation = Quaternion.Euler(0f, 0f, hoursAngle);
    }
}
