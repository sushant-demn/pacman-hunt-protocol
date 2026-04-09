using UnityEngine;
using System.Collections; // Required for IEnumerator

public class DelayAction : MonoBehaviour
{
    // Function to assign to your Button's OnClick event
    public void OnButtonClick()
    {
        StartCoroutine(DelayedActionRoutine(0.2f));
    }

    private IEnumerator DelayedActionRoutine(float delay)
    {
        Debug.Log("Button clicked! Waiting...");
        yield return new WaitForSeconds(delay); // Wait for the specified time
        Debug.Log("Delay finished! Executing action.");
        // Your logic here (e.g., Load scene, spawn object)
    }
}
