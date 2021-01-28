﻿using System.Collections;
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
    private Dictionary<string, List<AudioClip>> ambientDic = new Dictionary<string, List<AudioClip>>() ;
    private string currentAmbient = "Outside";


    private void Start()
    {
        collectibles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Collectibles"));
        audioSource = GetComponent<AudioSource>();

        ambientDic.Add("Outside", ambientOutside);
        ambientDic.Add("Inside", ambientInside);

        PlayRandomAmbient(ambientDic[currentAmbient]);

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

    public void ChangeGameStatus(bool state)
    {
        gameStatus = state;
    }

    public void ChangeAmbient(string ambient)
    {
        if(currentAmbient != ambient)
        {
            currentAmbient = ambient;
            PlayRandomAmbient(ambientDic[currentAmbient]);
        }
    }

    public bool GetGameStatus()
    {
        return gameStatus;
    }

    public void Victory()
    {
        Debug.LogWarning("VICTOIRE");
    }


    private void PlayRandomAmbient(List<AudioClip> ambient)
    {
        
        audioSource.clip = ambient[Random.Range(0, ambient.Count - 1)];
        audioSource.Play();
    }
}
