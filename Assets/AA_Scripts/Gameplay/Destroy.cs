using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float delaySec=2;

    // This method starts a coroutine to destroy the GameObject with a delay
    public void DestroyGameObjectWithDelay()
    {
        StartCoroutine(DestroyAfterDelay(delaySec)); // Wait for 2 seconds before destroying
    }

    // Coroutine to handle the delay
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    
    //insta destroy
    public void DestroyGameObject()
    {
        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}
