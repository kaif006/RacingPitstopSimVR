// using UnityEngine;

// public class PitButtonController : MonoBehaviour
// {
//     [Header("Button Visuals")]
//     public Renderer buttonRenderer;
//     public Material redMaterial;
//     public Material greenMaterial;

//     [Header("Animators")]
//     public Animator buttonAnimator;    // The small animation for the button moving down
//     public Animator carAnimator;       // The animator on your RaceCar
    
//     [Header("Trigger Names")]
//     public string buttonPressTrigger = "Press";
//     public string carArrivalTrigger = "StartArrival";

//     private bool isRed = false;

//     /// <summary>
//     /// This method should be called by the XR Simple Interactable 
//     /// 'Select Entered' event.
//     /// </summary>
//     // public void OnButtonPressed()
//     // {
//     //     // 1. Toggle State
//     //     isRed = !isRed;

//     //     // 2. Update Visuals and Car Logic
//     //     if (isRed)
//     //     {
//     //         if (buttonRenderer != null) buttonRenderer.material = redMaterial;
            
//     //         // Trigger the car to arrive
//     //         if (carAnimator != null)
//     //         {
//     //             carAnimator.SetTrigger(carArrivalTrigger);
//     //             Debug.Log("Button RED: Car is arriving.");
//     //         }
//     //     }
//     //     else
//     //     {
//     //         if (buttonRenderer != null) buttonRenderer.material = greenMaterial;
            
//     //         // Optional: Trigger car to leave
//     //         // carAnimator.SetTrigger("StartExit");
//     //         Debug.Log("Button GREEN: Pit stop complete.");
//     //     }

//     //     // 3. Play the physical button "click" animation
//     //     if (buttonAnimator != null)
//     //     {
//     //         buttonAnimator.SetTrigger(buttonPressTrigger);
//     //     }
//     // }




//     public void OnButtonPressed()
// {
//     // 1. Toggle the state first
//     isRed = !isRed;
//     Debug.Log("Button Clicked! State isRed: " + isRed);

//     // 2. Change the material
//     if (buttonRenderer != null)
//     {
//         buttonRenderer.material = isRed ? redMaterial : greenMaterial;
//     }
//     else
//     {
//         Debug.LogError("Button Renderer is missing on " + gameObject.name);
//     }

//     // 3. Trigger the Car
//     if (isRed)
//     {
//         if (carAnimator != null)
//         {
//             // This is the line that actually starts the animation
//             carAnimator.SetTrigger(carArrivalTrigger);
//             Debug.Log("Button RED: Car Arrival Trigger sent!");
//         }
//         else
//         {
//             // If you see this in console, you forgot to drag the car into the script!
//             Debug.LogError("Car Animator is NOT assigned in the Inspector!");
//         }
//     }

//     // 4. Button physical click animation
//     if (buttonAnimator != null)
//     {
//         buttonAnimator.SetTrigger(buttonPressTrigger);
//     }
// }
// }


















using UnityEngine;

public class PitButtonController : MonoBehaviour
{
    [Header("Button Visuals")]
    public Renderer buttonRenderer;
    public Material redMaterial;
    public Material greenMaterial;

    [Header("Animators")]
    public Animator buttonAnimator;
    public Animator carAnimator;
    
    [Header("Trigger Names")]
    public string buttonPressTrigger = "Press";
    public string carArrivalTrigger = "StartArrival";
    public string carDepartureTrigger = "StartDeparture"; // New Trigger

    private bool isRed = false;

    public void OnButtonPressed()
    {
        isRed = !isRed;
        
        // Update Button Color
        if (buttonRenderer != null)
        {
            buttonRenderer.material = isRed ? redMaterial : greenMaterial;
        }

        if (carAnimator != null)
        {
            if (isRed)
            {
                // Action when button turns RED
                carAnimator.SetTrigger(carArrivalTrigger);
                Debug.Log("Button RED: Triggering Arrival.");
            }
            else
            {
                // Action when button turns GREEN
                carAnimator.SetTrigger(carDepartureTrigger);
                Debug.Log("Button GREEN: Triggering Departure.");
            }
        }
        else
        {
            Debug.LogError("Car Animator is not assigned in the Inspector!");
        }

        // Play physical button click animation
        if (buttonAnimator != null)
        {
            buttonAnimator.SetTrigger(buttonPressTrigger);
        }
    }
}