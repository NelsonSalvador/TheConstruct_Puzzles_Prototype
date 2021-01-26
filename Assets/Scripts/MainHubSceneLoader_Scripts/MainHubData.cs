using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "MainHubState", menuName = "CurrentLevel")]
public class MainHubData : ScriptableObject
{
    public int level;
}
