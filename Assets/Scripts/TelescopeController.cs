using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeController : Interractible
{
    private string victoryDialog;

    [SerializeField] private AudioClip failureAudioClip;
    private AudioClip successAudioClip;

    public override void Start()
    {
        base.Start();
        //victoryDialog = dialog;
        successAudioClip = audioClip;
    }

    public override void OnInterraction()
    {

        CheckForWin();

        base.OnInterraction();

    }

    private void CheckForWin()
    {

        if (gameManager.GetCollectedCollectibles().Count != gameManager.GetCollectibles().Count)
        {
            audioClip = failureAudioClip;
            //dialog = "Missing Pieces ! " + gameManager.GetCollectedCollectibles().Count + " / " + gameManager.GetCollectibles().Count;
        }
        else
        {
            audioClip = successAudioClip;
            //dialog = victoryDialog;
            gameManager.Victory();
        }

    }
}
