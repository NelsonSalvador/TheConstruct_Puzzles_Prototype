using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Trigger for Labyrints Cutscene. 
/// </summary>
public class LabiritsTrigger_1 : MonoBehaviour
{
    [SerializeField]
    public PlayableDirector timeline;
    [SerializeField]
    public MeshRenderer trigger;

    private bool played = false;

    // Update is called once per frame
    void Update()
    {
        if (trigger.enabled == true && played == false)
        {
            timeline.Play();
            played = true;
        }
    }
}
