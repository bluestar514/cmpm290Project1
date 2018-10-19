using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Setup : MonoBehaviour {

    public List<wordData> wordList;
    public GameObject wordNodePrefab;
    public Transform nodeField;

	void Awake () {
        LoadJSON();

        sortWordList(wordList);

        List<GameObject> wordNodeList = new List<GameObject>(); 
        //foreach(wordData word in wordList){
            wordNodeList.Add(createWordNode(wordList[0]));
        print("Making second node");
            wordNodeList.Add(createWordNode(wordList[1]));
        //}

	}
	
    void LoadJSON(){
        TextAsset jsonText =  Resources.Load("theTraitorsTale") as TextAsset;
        wordListWrapper wordListWrapper = JsonUtility.FromJson<wordListWrapper>(jsonText.text);

        wordList = wordListWrapper.wordList;
    }

    void sortWordList(List<wordData> listOWordData){
        listOWordData.Sort((a, b) => (b.getFreq().CompareTo(a.getFreq())));
    }

    GameObject createWordNode(wordData word){
        int maxRange = 10;
        Vector3 pos = pickRandomPoint(maxRange);
        GameObject nodeObj = Instantiate(wordNodePrefab, pos, new Quaternion(), nodeField);
        nodeObj.name = nodeObj.name.Replace("(Clone)", word.ToString());
        nodeObj.GetComponent<node>().wordData = word;


        float nodeSize = nodeObj.GetComponent<node>().scaleCorrectly();
        
        while(nodeObj.GetComponent<node>().colliding()){
            maxRange += 10;
            nodeObj.transform.SetPositionAndRotation(pickRandomPoint(maxRange), new Quaternion());
            
        }

        return nodeObj;   
    }


    bool collides(Vector3 center, float radius){
        print(center);
        print(radius);
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        print(hitColliders.Length);
        
        if(hitColliders.Length > 0){
            return true;
        }else{
            return false;
        }
    }

    Vector3 pickRandomPoint(float max){
        Vector3 point = new Vector3();
        point.x = (Random.value - .5f)*max;
        point.y = (Random.value - .5f)*max;
        point.z = (Random.value - .5f)*max;

        print("new pos" + point);
        return point;
    }
}
