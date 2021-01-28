using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interractible : MonoBehaviour
{
    protected UIController uiController;
    [SerializeField][TextArea] protected string dialog = "I'm Interractible";
    protected GameManager gameManager;
    protected AudioSource audioSource;
    [SerializeField] protected AudioClip audioClip;



    public virtual void Start()
    {

        uiController = GameObject.Find("Canvas").GetComponent<UIController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();

    }

    public virtual void OnInterraction()
    {
        Debug.Log("I " + gameObject.name + " have been hit ! " + gameManager.GetGameStatus());
        StartCoroutine( uiController.ShowDialogBox(dialog) );


        audioSource.clip = audioClip;
        audioSource.time = 0;
        audioSource.Play();
    }

}
