using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interractible : MonoBehaviour
{
    protected UIController uiController;
    protected GameManager gameManager;
    protected AudioSource audioSource;
    [SerializeField] protected Material highlightMaterial;
    private Material defaultMat;
    protected MeshRenderer meshRenderer;
    protected int highlightCooldown = 0;
    [SerializeField] protected int highlightCooldownMax = 2;
    [SerializeField] TextAsset jsonData;
    [SerializeField] GameObject graphicsObj;

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
    protected Dictionary<string, AudioClip> unpackedAudio = new Dictionary<string, AudioClip>();

    void Start()
    {

        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        meshRenderer = graphicsObj.GetComponent<MeshRenderer>();
        defaultMat = meshRenderer.material;
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
        AudioUnpack(false); //Set True to show the import process in the console

    }

    private void AudioUnpack(bool showInConsole)
    {
        foreach (Dialog dialog in objData.dialog)
        {
            if (showInConsole)
            {
                Debug.LogWarning(gameObject.name + " Trying to unpack audioClip at : " + dialog.audioclip);
            }

            unpackedAudio.Add(dialog.audioclip, Resources.Load<AudioClip>(dialog.audioclip));

            if (showInConsole)
            {
                if (unpackedAudio[dialog.audioclip] != null)
                {
                    Debug.LogWarning("Unpacking Successful of : " + dialog.audioclip);
                }
                else
                {
                    Debug.LogError("Unpacking failed of : " + dialog.audioclip);
                }
            }
        }
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

        if (gameManager.GetGameStatus() == 1 && gameManager.GetSelectedObject() == gameObject) //If In Dialog
        {

            if (!uiController.GetActiveDialogBox()) //If Dialog box is not open
            {
                Debug.Log("Start Dialog");
                uiController.ShowDialogBox();

                SetDialogAndAudioClip();
                StartCoroutine(uiController.DisplayDialog(dialogFinal));
                PlayAudio(objData.dialog[currentDialog].audioclip);
                Debug.Log(dialogFinal + " currentDialog : " + currentDialog);

            }
            else
            {
                if (Input.GetButtonDown("Interract"))
                {
                    Debug.Log("NextDialog"); 
                    currentDialog++;
                    SetDialogAndAudioClip();
                    PlayAudio(objData.dialog[currentDialog].audioclip);
                    StartCoroutine(uiController.DisplayDialog(dialogFinal));
                    Debug.Log(dialogFinal + " currentDialog : " + currentDialog); 
                }
            }
        }

        if (currentDialog == objData.dialog.Length-1 && gameManager.GetGameStatus() == 1 && gameManager.GetSelectedObject() == gameObject )
        {
            Debug.Log("Last Dialog of ! + " + gameObject.name);
            StartCoroutine(gameManager.SetGameStatus(2));
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
            meshRenderer.material = defaultMat;
        }
    }

    public void StartHighlight()
    {
        /*Debug.Log("Highlight");*/
        highlightCooldown = 0;
        Highlight(true);
    }

    public void ResetDialog()
    {

        Debug.Log("Reseting Dialog...");

        currentDialog = 0;

    }

    public virtual void OnInterraction()
    {

        gameManager.SetSelectedObject(gameObject);
        StartCoroutine(gameManager.SetGameStatus(1));

    }

    public void PlayAudio(string clipPath)
    {
        audioSource.clip = unpackedAudio[clipPath];
        audioSource.time = 0;
        audioSource.Play();
    }

}
