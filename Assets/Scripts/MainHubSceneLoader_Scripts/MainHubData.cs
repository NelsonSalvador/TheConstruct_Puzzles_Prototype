using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
/// <summary>
/// Scriptable object that contains the current level.
/// </summary>
[CreateAssetMenu(fileName = "MainHubState", menuName = "CurrentLevel")]
public class MainHubData : ScriptableObject
{
    // Current Level.
    public int level;
}
