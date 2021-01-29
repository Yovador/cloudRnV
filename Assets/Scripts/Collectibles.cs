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

            gameManager.AddToCollectedCollectibles(gameObject);
        }
    }

}
