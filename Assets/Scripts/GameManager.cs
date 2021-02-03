using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int gameStatus = 0; // 0 : inGame ; 1 : In Dialog ; 2 : Last Dialog
    private List<GameObject> collectibles = new List<GameObject>();
    private List<GameObject> collectedCollectibles = new List<GameObject>();
    protected AudioSource audioSource;

    [SerializeField] private List<AudioClip> ambientOutside = new List<AudioClip>();
    [SerializeField] private List<AudioClip> ambientInside = new List<AudioClip>();
    private Dictionary<string, List<AudioClip>> ambientDic = new Dictionary<string, List<AudioClip>>();


    private string currentAmbient = "Outside";
    private UIController uiController;
    private GameObject selectedObject = null;

    [SerializeField] private float interractionCooldown = 0.3f;

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

        if (Input.GetButtonDown("Interract"))
        {
            Debug.Log("Interract Pressed");
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

    public IEnumerator SetGameStatus(int state)
    {
        yield return new WaitForEndOfFrame();
        gameStatus = state;
        Debug.LogWarning("Current Game Status " + gameStatus);
    }
    public int GetGameStatus()
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
        Debug.Log("Reset");
        uiController.HideDialogBox();
        if(GetSelectedObject() != null)
        {
            GetSelectedObject().GetComponent<Interractible>().ResetDialog();
            if (GetSelectedObject().CompareTag("Collectibles"))
            {
                GetSelectedObject().SetActive(false);
            }
        }
        SetSelectedObject(null);

        StartCoroutine(SetGameStatus(0));

    }

}
