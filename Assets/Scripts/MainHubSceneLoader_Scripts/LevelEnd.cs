using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    private MainHubData MainHubData;
    [SerializeField]
    private int CurrentLevel;
    // Start is called before the first frame update
    void Start()
    {
        MainHubData.level = 1;
    }
}
