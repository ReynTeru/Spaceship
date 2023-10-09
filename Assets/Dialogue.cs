using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
   public TextMeshProUGUI textComponent;
   public string[] lines;
   public float textSpeed;
   public bool bDialougeStopped = true;

   private int index;
    void Start()
    {
       textComponent.text = String.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartDialogue()
    {
       textComponent.text = String.Empty;
       bDialougeStopped = false;
       index = 0;
       StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
       foreach(char c in lines[index])
       {
          textComponent.text += c;
          yield return new WaitForSeconds(textSpeed);
       }
    }
    
    void NextLine()
    {
       if (index < lines.Length - 1)
       {
          index++;
          textComponent.text = String.Empty;
          StartCoroutine(TypeLine());
       }
       else
       {
          textComponent.text = String.Empty;
          bDialougeStopped = true;
       }
    }
    
    public void CallNextLine()
    {
       if (textComponent.text == lines[index])
       {
          NextLine();
       }
       else
       {
          StopAllCoroutines();
          textComponent.text = lines[index];
       }
    }
}
