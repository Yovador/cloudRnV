using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeController : Interractible
{
    private string dialog = "";
    private string audioclip = "";

    public override void OnInterraction()
    {
        CheckForWin();
        base.OnInterraction();
    }

    protected override void SetDialogAndAudioClip()
    {
        dialogFinal = dialog;
        audioclipFinal = audioclip;
    }

    private void CheckForWin()
    {

        if (gameManager.GetCollectedCollectibles().Count != gameManager.GetCollectibles().Count)
        {
            dialog = objData.dialog[0].text + " " + gameManager.GetCollectedCollectibles().Count + " pièces de téléscope, et il m'en faudrait " + gameManager.GetCollectibles().Count + ". Il va falloir que je continue de chercher...";
            audioclip = objData.dialog[0].audioclip;
            currentDialog = objData.dialog.Length - 1;
        }
        else
        {
            dialog = objData.dialog[1].text;
            audioclip = objData.dialog[0].audioclip;
            currentDialog = objData.dialog.Length - 1;
            gameManager.Victory();
        }

    }
}
