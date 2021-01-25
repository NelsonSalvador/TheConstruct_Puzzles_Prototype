using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cinematic_Subs : MonoBehaviour
{
    public GameObject textox;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SubsSequence());
    }



    IEnumerator SubsSequence()
    {
        yield return new WaitForSeconds(3.58f);
        textox.GetComponent<Text>().text = "Stranger: Hello and welcome to your own dream! Hope you like the view.";
        yield return new WaitForSeconds(4.1f);
        textox.GetComponent<Text>().text = "Stranger: Well then, let’s start checking you up shall we?!";
        yield return new WaitForSeconds(4.3f);
        textox.GetComponent<Text>().text = "Stranger: Ah, don’t bother responding, we can’t hear you.";
        yield return new WaitForSeconds(3.8f);
        textox.GetComponent<Text>().text = "Stranger: Hmm, it doesn’t seem like you are under any trouble, hmm let me just check your vitals and…";
        yield return new WaitForSeconds(4.9f);
        textox.GetComponent<Text>().text = "Stranger: Yeah. Great! You seem to be doing... As well as could be expected from someone in your situation.";
        yield return new WaitForSeconds(7.3f);
        textox.GetComponent<Text>().text = "Stranger: Well, you’re surely feeling a little fuzzy headed, and your short term memory is probably a mess…";
        yield return new WaitForSeconds(7f);
        textox.GetComponent<Text>().text = "Stranger: Hmm, at least those are the expected side effects... but oh well";
        yield return new WaitForSeconds(4f);
        textox.GetComponent<Text>().text = "Stranger: Now then, let me read this short to help you light up your memory:";
        yield return new WaitForSeconds(5f);
        textox.GetComponent<Text>().text = "Stranger: Adam Myers! You were invited to Washington to test the security of The Construct,";
        yield return new WaitForSeconds(4f);
        textox.GetComponent<Text>().text = "Stranger: our newly developed prison that allows us to control the subconscious of the subjects";
        yield return new WaitForSeconds(3.8f);
        textox.GetComponent<Text>().text = "Stranger: and have it trap their conscious mind.";
        yield return new WaitForSeconds(2.1f);
        textox.GetComponent<Text>().text = "Stranger: That’s where you are right now, and where you need to break through.";
        yield return new WaitForSeconds(3f);
        textox.GetComponent<Text>().text = "Stranger: Shall you manage to succeed on escaping, you will be rewarded with the generous sum of an extra 1 million dollars after taxes.";
        yield return new WaitForSeconds(7f);
        textox.GetComponent<Text>().text = "Stranger: And yeah, that’s the summary. ";
        yield return new WaitForSeconds(2f);
        textox.GetComponent<Text>().text = "Stranger: Hope you caught all that!";
        yield return new WaitForSeconds(1.5f);
        textox.GetComponent<Text>().text = "We will now leave you alone to do your thing... Have fun!";
        yield return new WaitForSeconds(4f);
        textox.GetComponent<Text>().text = "";
    }
}
