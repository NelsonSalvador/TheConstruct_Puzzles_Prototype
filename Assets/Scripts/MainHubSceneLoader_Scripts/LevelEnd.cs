using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the MainHubData Scriptable Object with the correct Level.
/// </summary>
public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    private MainHubData MainHubData;
    [SerializeField]
    private int CurrentLevel;
    // Start is called before the first frame update
    void Start()
    {
        MainHubData.level = CurrentLevel;
    }
}
