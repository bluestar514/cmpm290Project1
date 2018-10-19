using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node : MonoBehaviour {

    public GameObject nodeBodyObj;
    public GameObject nameObj;
    public GameObject freqObj;

    public wordData wordData;

	void Start () {
        
        nameObj.GetComponent<TextMesh>().text = wordData.getWord();
        freqObj.GetComponent<TextMesh>().text = ""+wordData.getFreq();

        nodeBodyObj.transform.localScale *= wordData.getFreq()/10;
	}

}
