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
        dialogueBox.SetActive(false);

    }

    void Update()
    {
        if (Input.GetButtonDown("Return") && !gameManager.GetGameStatus() )
        {
            gameManager.ResetSelected();
        }    
    }

    public IEnumerator ShowDialogBox(string text)
    {
        string currentTxt = "";
        textComponent.text = currentTxt;
        gameManager.ChangeGameStatus(false);
        dialogueBox.SetActive(true);
        foreach(char letter in text)
        {
            yield return new WaitForSecondsRealtime(timeBetweenLetter);
            currentTxt = currentTxt + letter;
            textComponent.text = currentTxt;
        }

    }

    public void HideDialogBox()
    {
        gameManager.ChangeGameStatus(true);
        dialogueBox.SetActive(false);
    }

}
