using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : Interractible
{
    private bool collectingSequenceOn = false;
    public override void OnInterraction()
    {
        base.OnInterraction();

        if (!collectingSequenceOn)
        {

            StartCoroutine( CollectingSequence() );
        }
    }

    IEnumerator CollectingSequence()
    {
        collectingSequenceOn = true;

        gameManager.AddToCollectedCollectibles(gameObject);
        yield return new WaitForSecondsRealtime(audioClip.length);
        gameObject.SetActive(false);
        collectingSequenceOn = false;
    }
}
