using System;
using UnityEngine;
public class NPCcarSound : MonoBehaviour
{
    public Transform player;           // assign the player's Transform in Inspector
    public AudioSource hornSource;     // assign the AudioSource on this car
    public float detectionRadius = 5f; // how close player must be
    public float maxVolume = 1f;       // maximum horn volume
    public float volumeIncreaseSpeed = 1f; // how quickly volume ramps up

    private void Start()
    {
        if (hornSource == null)
        {
            hornSource = GetComponent<AudioSource>();
        }
        // Start playing the horn sound at 0 volume
        hornSource.volume = 0f;
        
    hornSource.loop = true;
        hornSource.Play();
        Debug.Log("playing horn!");
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.Log("PROBLEM!!!");
            return;
        }

        // Calculate distance between player and this NPC car
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRadius)
        {
            // Increase volume as player gets closer
            float targetVolume = Mathf.Lerp(maxVolume, 0f, distance / detectionRadius);
            hornSource.volume = Mathf.MoveTowards(hornSource.volume, targetVolume, volumeIncreaseSpeed * Time.deltaTime);
        }
        else
        {
            // Fade out when player leaves range
            hornSource.volume = Mathf.MoveTowards(hornSource.volume, 0f, volumeIncreaseSpeed * Time.deltaTime);
        }
    }
}
