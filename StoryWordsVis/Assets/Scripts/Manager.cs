using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    public string storyJsonFile;
    public List<WordData> wordList;

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void LoadJSON(){
        TextAsset jsonText =  Resources.Load(storyJsonFile) as TextAsset;
        WordListWrapper wordListWrapper = JsonUtility.FromJson<WordListWrapper>(jsonText.text);

        wordList = wordListWrapper.wordList;
    }

    protected void sortWordList(List<WordData> listOWordData){
        listOWordData.Sort((a, b) => (b.getFreq().CompareTo(a.getFreq())));
    }

}
