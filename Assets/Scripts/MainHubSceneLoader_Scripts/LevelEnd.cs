using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public MainHubData MainHubData;
    public int CurrentLevel;
    // Start is called before the first frame update
    void Start()
    {
        MainHubData.level = 1;
    }
}
