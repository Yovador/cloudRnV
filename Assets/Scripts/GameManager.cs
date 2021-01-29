using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameStatus = true; // true : inGame ; false : In Dialogue
    private List<GameObject> collectibles = new List<GameObject>();
    private List<GameObject> collectedCollectibles = new List<GameObject>();
    protected AudioSource audioSource;

    [SerializeField] private List<AudioClip> ambientOutside = new List<AudioClip>();
    [SerializeField] private List<AudioClip> ambientInside = new List<AudioClip>();
    private Dictionary<string, List<AudioClip>> ambientDic = new Dictionary<string, List<AudioClip>>();


    private string currentAmbient = "Outside";
    private UIController uiController;
    private GameObject selectedObject = null;

    private void Start()
    {
        collectibles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Collectibles"));
        audioSource = GetComponent<AudioSource>();

        ambientDic.Add("Outside", ambientOutside);
        ambientDic.Add("Inside", ambientInside);

        PlayRandomAmbient(ambientDic[currentAmbient]);

        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();

    }

    private void Update()
    {
        if(audioSource.time == audioSource.clip.length)
        {
            PlayRandomAmbient(ambientDic[currentAmbient]);
        }
    }

    public List<GameObject> GetCollectibles()
    {
        return collectibles;
    }

    public List<GameObject> GetCollectedCollectibles()
    {
        return collectedCollectibles;
    }

    public void AddToCollectedCollectibles(GameObject collectible)
    {
        collectedCollectibles.Add(collectible);
    }

    public void SetGameStatus(bool state)
    {
        gameStatus = state;
    }
    public bool GetGameStatus()
    {
        return gameStatus;
    }

    public void ChangeAmbient(string ambient)
    {
        if(currentAmbient != ambient)
        {
            currentAmbient = ambient;
            PlayRandomAmbient(ambientDic[currentAmbient]);
        }
    }


    public void Victory()
    {
        Debug.LogWarning("VICTOIRE");
    }

    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }

    public void SetSelectedObject(GameObject obj)
    {
        selectedObject = obj;
    }

    private void PlayRandomAmbient(List<AudioClip> ambient)
    {
        
        audioSource.clip = ambient[Random.Range(0, ambient.Count - 1)];
        audioSource.Play();
    }

    public void ResetSelected()
    {
        uiController.HideDialogBox();
        if(GetSelectedObject() != null)
        {
            if (GetSelectedObject().CompareTag("Collectibles"))
            {
                GetSelectedObject().SetActive(false);
            }
        }
        SetSelectedObject(null);
    }
}
