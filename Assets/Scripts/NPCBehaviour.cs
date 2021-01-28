using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCBehaviour : Interractible
{
    [SerializeField] private string nameInterractible = "NPC";

    public override void Start()
    {
        base.Start();

        dialog = nameInterractible + " : " + dialog;
    }

}
