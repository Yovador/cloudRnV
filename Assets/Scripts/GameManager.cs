using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameStatus = true; // true : inGame ; false : In Dialogue

    public void ChangeGameStatus(bool state)
    {
        gameStatus = state;
    }

    public bool GetGameStatus()
    {
        return gameStatus;
    }

}
