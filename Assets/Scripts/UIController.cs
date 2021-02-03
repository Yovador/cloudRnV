using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    private GameObject dialogueBox;
    private Text textComponent;
    private GameManager gameManager;
    [SerializeField] private float timeBetweenLetter;


    void Start()
    {
        dialogueBox = GameObject.Find("DialogueBox");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        textComponent = GameObject.Find("Text").GetComponent<Text>();
        HideDialogBox();
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Return") && gameManager.GetGameStatus() != 0 )
        {
            gameManager.ResetSelected();
        }
        if (Input.GetButtonDown("Interract") && gameManager.GetGameStatus() == 2)
        {
            Debug.Log("Quitting");
            gameManager.ResetSelected();
        }
    }

    public void ShowDialogBox()
    {
        dialogueBox.SetActive(true);
    }

    public IEnumerator DisplayDialog(string text)
    {
        StopAllCoroutines();
        string currentTxt = "";
        textComponent.text = currentTxt;
        foreach(char letter in text)
        {
            yield return new WaitForSecondsRealtime(timeBetweenLetter);
            currentTxt = currentTxt + letter;
            textComponent.text = currentTxt;
        }

    }

    public void HideDialogBox()
    {
        dialogueBox.SetActive(false);
    }

    public bool GetActiveDialogBox()
    {
        return dialogueBox.activeSelf;
    }

}
