using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interractible : MonoBehaviour
{
    protected UIController uiController;
    [SerializeField] protected string nameInterractible = "InterractibleObject";
    [SerializeField][TextArea] protected string dialog = "I'm Interractible";
    private GameManager gameManager; 


    private void Start()
    {

        uiController = GameObject.Find("Canvas").GetComponent<UIController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        

    }

    public virtual void OnInterraction()
    {
        Debug.Log("I " + gameObject.name + " have been hit ! " + gameManager.GetGameStatus());


        StartCoroutine( uiController.ShowDialogBox(nameInterractible + " : " + dialog) );

    }



}
