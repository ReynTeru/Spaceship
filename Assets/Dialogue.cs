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

   private int index;
    void Start()
    {
       textComponent.text = String.Empty;
       StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartDialogue()
    {
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
}
