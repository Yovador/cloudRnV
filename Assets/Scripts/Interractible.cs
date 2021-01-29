using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interractible : MonoBehaviour
{
    protected UIController uiController;
    [SerializeField] protected Dictionary<string, AudioClip> dialogDic = new Dictionary<string, AudioClip>() ;
    protected GameManager gameManager;
    protected AudioSource audioSource;
    [SerializeField] protected AudioClip audioClip;
    [SerializeField] protected Material highlightMaterial;
    protected MeshRenderer meshRenderer;
    protected int highlightCooldown = 0;
    [SerializeField] protected int highlightCooldownMax = 2;


    public virtual void Start()
    {

        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(highlightCooldown >= highlightCooldownMax)
        {
            Highlight(false);
        }
        else
        {
            highlightCooldown++;
        }
    }
    void Highlight(bool status)
    {
        if(status)
        {
            meshRenderer.material = highlightMaterial;
        }
        else
        {
            meshRenderer.material = null;
        }
    }

    public void StartHighlight()
    {
        Debug.Log("Highlight");
        highlightCooldown = 0;
        Highlight(true);
    }

    public virtual void OnInterraction()
    {
        Debug.Log("I " + gameObject.name + " have been hit ! " + gameManager.GetGameStatus());

        if(gameManager.GetSelectedObject() == gameObject)
        {
            Debug.Log("Selected");
            gameManager.ResetSelected();

        }
        else
        {
            Debug.Log("Not Selected");
            gameManager.SetSelectedObject(gameObject);
            Dialog();
        }

    }

    protected virtual void Dialog()
    {
        foreach (KeyValuePair<string, AudioClip> pair in dialogDic)
        {
            StartCoroutine(uiController.ShowDialogBox(pair.Key));
            audioSource.clip = pair.Value;
            audioSource.time = 0;
            audioSource.Play();
        }
    }

}
