using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinalDialogue : MonoBehaviour
{
public TextMeshProUGUI textComponent;
   public string lines;
   public float textSpeed;
   bool bDialogueStarted = false;
   bool bLineComplete = false;
   
   public InputAction FinishDialogue;
   
   float MainmenuTimer = 0.0f;
   float MainmenuTime = 1f;
   
    void Start()
    {
       textComponent.text = String.Empty;
       FinishDialogue.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (bLineComplete)
        {
            MainmenuTimer += Time.deltaTime;
            if (MainmenuTimer >= MainmenuTime)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }

        if (FinishDialogue.triggered && bDialogueStarted)
        {
            if (bLineComplete)
            {
                
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                textComponent.text = lines;
                bLineComplete = true;
            }
        }
    }
    public void StartDialogue()
    { bDialogueStarted = true;
       textComponent.text = String.Empty;
       StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
       foreach(char c in lines)
       {
          textComponent.text += c;
          yield return new WaitForSeconds(textSpeed);
       }
    }
    
    
}
