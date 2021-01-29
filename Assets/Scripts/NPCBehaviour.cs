using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCBehaviour : Interractible
{
    [SerializeField] private string nameNPC = "NPC";


    protected override void Dialog()
    {
        foreach (KeyValuePair<string, AudioClip> pair in dialogDic)
        {
            StartCoroutine(uiController.ShowDialogBox(nameNPC + " : " + pair.Key));
            audioSource.clip = pair.Value;
            audioSource.time = 0;
            audioSource.Play();
        }
    }

}
