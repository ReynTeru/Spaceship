using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DoorManager : MonoBehaviour
{
    public DoorInteract[] Doors;
    [FormerlySerializedAs("AudioListeners")] public AudioSource[] audioSources;
    float AudioTransitionTime1 = 1.0f;
    float AudioTransitionTimer1 = 0.0f;
    float AudioTransitionTime2 = 1.0f;
    float AudioTransitionTimer2 = 0.0f;
    bool bAudioTransition1 = false;
    bool bAudioTransition2 = false;
    
    public TextMeshProUGUI textComponent;
    
    DoorInteract CurrentDoor;
    int CurrentDoorIndex = 0;
    int NumClipboards = 0;
    int NumClipboardAdded = 0;
    
    bool bStartAudio = false;
    float StartAudioTimer = 0.0f;
    float StartAudioTime = 0.5f;
    
    void Start()
    {
        CurrentDoor = Doors[0];
        audioSources[0].Play();

        NumClipboards = CurrentDoor.Clipboards.Length;
    }

    private void Update()
    {
        if (bStartAudio)
        {
            StartAudioTimer += Time.deltaTime;
            if (StartAudioTimer >= StartAudioTime)
            {
                StartAudio();
                bStartAudio = false;
                StartAudioTimer = 0.0f;
            }
        }

        if (bAudioTransition1)
        {
            AudioTransitionTimer1 += Time.deltaTime;
            if (AudioTransitionTimer1 >= AudioTransitionTime1)
            {
                audioSources[0].Stop();
                bAudioTransition1 = false;
                AudioTransitionTimer1 = 0.0f;
            }
            else
            {
                audioSources[0].volume = Mathf.Lerp(0.5f, 0,  AudioTransitionTimer1 / AudioTransitionTime1);
                audioSources[1].volume =
                    Mathf.Lerp(0, 0.5f,  AudioTransitionTimer1 / AudioTransitionTime1);
            }
            
        }
        if (bAudioTransition2)
        {
            AudioTransitionTimer2 += Time.deltaTime;
            if (AudioTransitionTimer2 >= AudioTransitionTime2)
            {
                audioSources[1].Stop();
                bAudioTransition2 = false;
                AudioTransitionTimer2 = 0.0f;
            }
            else
            {
                audioSources[1].volume = Mathf.Lerp(0.5f, 0,  AudioTransitionTimer2 / AudioTransitionTime2);
                audioSources[2].volume =
                    Mathf.Lerp(0, 0.35f,  AudioTransitionTimer2 / AudioTransitionTime2);
            }
            
        }
    }
    
    void UpdateDoor()
    {
        if (CurrentDoorIndex < Doors.Length - 1)
        {
            CurrentDoorIndex++;
            CurrentDoor = Doors[CurrentDoorIndex];
            NumClipboards = CurrentDoor.Clipboards.Length;
            NumClipboardAdded = 0;
            UpdateUI();
            bStartAudio = true;
   
        }
        else
        {
            textComponent.text = String.Empty;
            CurrentDoor = null;
            NumClipboards = 0;
            NumClipboardAdded = 0;
            UpdateUI();
        }
    }
    
    public void DoorOpened()
    {
        if (CurrentDoor)
        {
            UpdateDoor();
        }
    }
    
    public void AddClipboard()
    {
        if (CurrentDoor)
        {
            NumClipboardAdded++;
        }
        UpdateUI();
    }
    
    void UpdateUI()
    {
        textComponent.text = NumClipboardAdded + "/" + NumClipboards;
    }

    public void StartAudio()
    {
        if (CurrentDoorIndex == 1)
        {
            audioSources[1].Play();
            audioSources[1].volume = 0;
            bAudioTransition1 = true;

        }
        else if (CurrentDoorIndex == 2)
        {
         
            audioSources[2].Play();
            audioSources[2].volume = 0;
            bAudioTransition2 = true;
        }
    }
}
