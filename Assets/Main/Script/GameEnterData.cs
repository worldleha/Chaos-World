using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnterData : MonoBehaviour
{
    private static GameEnterData instance;

    private static bool isNewGame;

    public static GameEnterData Instance
    {
        get { return instance; }
    }

    public static bool IsNewGame
    {
        get
        {
            return isNewGame;
        }
        set
        {
            isNewGame = value;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }


}
