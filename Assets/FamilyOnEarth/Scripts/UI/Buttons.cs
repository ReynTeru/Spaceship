using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    public InputAction PauseAction;
    [FormerlySerializedAs("PauseOanel")] public GameObject PausePanel;
    bool bPaused = false;

    private void Start()
    {
        PauseAction.Enable();
        PauseAction.performed += ctx => Pause();
        
        if (PausePanel)
        {
            PausePanel.SetActive(false);
        }

    }

    public void Pause()
    {
        if (bPaused)
        {
            bPaused = false;
            Resume();
            
            return;
        }
        else if (!bPaused)
        {

            bPaused = true;
            Cursor.lockState = CursorLockMode.None;
            if (PausePanel)
            {
                PausePanel.SetActive(true);
            }

            Time.timeScale = 0;
            
        }
        
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (PausePanel)
        {
            PausePanel.SetActive(false);
        }

        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        if (PausePanel)
        {
            PausePanel.SetActive(false);
        }
        
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
