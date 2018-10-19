using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour {

    public List<wordData> wordList;
    public GameObject wordNodePrefab;
    public Transform nodeField;

	void Awake () {
        LoadJSON();

        List<GameObject> wordNodeList = new List<GameObject>(); 
        foreach(wordData word in wordList){
            wordNodeList.Add(createWordNode(word));
        }

	}
	
    void LoadJSON(){
        TextAsset jsonText =  Resources.Load("theTraitorsTale") as TextAsset;
        wordListWrapper wordListWrapper = JsonUtility.FromJson<wordListWrapper>(jsonText.text);

        wordList = wordListWrapper.wordList;
    }

    GameObject createWordNode(wordData word){
        GameObject node = Instantiate(wordNodePrefab, pickRandomPoint(), new Quaternion(), nodeField);
        node.name.Replace("(Clone)", word.ToString());
        node.GetComponent<node>().wordData = word;
        return node;   
    }


    Vector3 pickRandomPoint(){
        Vector3 point = new Vector3();
        point.x = (Random.value - .5f)*100;
        point.y = (Random.value - .5f)*100;
        point.z = (Random.value - .5f)*100;
        return point;
    }
}
