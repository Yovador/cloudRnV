using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interractible : MonoBehaviour
{
    protected UIController uiController;
    protected GameManager gameManager;
    protected AudioSource audioSource;
    [SerializeField] protected Material highlightMaterial;
    protected MeshRenderer meshRenderer;
    protected int highlightCooldown = 0;
    [SerializeField] protected int highlightCooldownMax = 2;
    [SerializeField] TextAsset jsonData;

    protected int currentDialog = 0;
    protected string dialogFinal = "";
    protected string audioclipFinal = "";


    [System.Serializable]
    protected class Dialog
    {
        public string text;
        public string audioclip;
    }

    [System.Serializable]
    protected class ObjData
    {
        public string name;
        public Dialog[] dialog;
    }

    protected ObjData objData;

    void Start()
    {

        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();

        //LOAD DATA FROM JSON
        if (jsonData != null)
        {
            objData = JsonUtility.FromJson<ObjData>(jsonData.text);

            /*Debug.Log(objData.name + " : ");
            foreach(Dialog dialog in objData.dialog)
            {
                Debug.Log(dialog.text + " / " + dialog.audioclip);
            }*/
        }
        SetDialogAndAudioClip();


    }

    private void Update()
    {

        if (highlightCooldown >= highlightCooldownMax)
        {
            Highlight(false);
        }
        else
        {
            highlightCooldown++;
        }

        if (!gameManager.GetGameStatus() && gameManager.GetSelectedObject() == gameObject) //If In Dialog
        {
            Debug.Log(dialogFinal);

            if (!uiController.GetActiveDialogBox()) //If Dialog box is not open
            {
                Debug.Log("Start Dialog");
                uiController.ShowDialogBox();

                SetDialogAndAudioClip();
                StartCoroutine(uiController.DisplayDialog(dialogFinal));
                PlayAudio(objData.dialog[currentDialog].audioclip);
            }
            else
            {
                if (Input.GetButtonDown("Interract") || Input.GetButtonDown("Return") )
                {
                    currentDialog++;
                    Debug.Log("Displaying dialog number : " + currentDialog);

                    SetDialogAndAudioClip();
                    StartCoroutine(uiController.DisplayDialog(dialogFinal));
                }
            }
        }

        if(currentDialog == objData.dialog.Length-1 && !gameManager.GetGameStatus())
        {
            Debug.Log("Last Dialog !");
            gameManager.SetGameStatus(true);
        }

    }

    protected virtual void SetDialogAndAudioClip()
    {
        dialogFinal = objData.dialog[currentDialog].text;
        audioclipFinal = objData.dialog[currentDialog].audioclip;
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
        /*Debug.Log("Highlight");*/
        highlightCooldown = 0;
        Highlight(true);
    }

    public virtual void OnInterraction()
    {
        if (gameManager.GetSelectedObject() == gameObject)
        {
            Debug.Log("Selected");
            gameManager.ResetSelected();
            currentDialog = 0;
        }
        else
        {
            gameManager.SetSelectedObject(gameObject);
            gameManager.SetGameStatus(false);
        }
    }

    public void PlayAudio(string clipPath)
    {
        audioSource.clip = Resources.Load<AudioClip>(clipPath);
        audioSource.time = 0;
        audioSource.Play();
    }

}
