using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCBehaviour : Interractible
{

    protected override void SetDialogAndAudioClip()
    {
        dialogFinal = objData.name + " : " + objData.dialog[currentDialog].text;
        audioclipFinal = objData.dialog[currentDialog].audioclip;
    }


}
