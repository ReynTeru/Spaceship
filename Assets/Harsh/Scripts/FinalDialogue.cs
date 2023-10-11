using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalDialogue : MonoBehaviour
{
public TextMeshProUGUI textComponent;
   public string lines;
   public float textSpeed;
   public bool bDialougeStopped = true;
    void Start()
    {
       textComponent.text = String.Empty;
    }

    // Update is called once per frame
    public void StartDialogue()
    {
       textComponent.text = String.Empty;
       bDialougeStopped = false;
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
