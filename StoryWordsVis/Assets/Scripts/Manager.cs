using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Manager : MonoBehaviour {
    public string storyJsonFile;
    public List<WordData> wordList;
    public GameObject wordNodePrefab;
    public GameObject connectionPrefab;
    public Transform nodeField;
    Dictionary<string, GameObject> wordNodeDict;
    List<GameObject> connectionList;
    Dictionary<string, Dictionary<string, Dictionary<string, GameObject>>> wordConnectionDict;


    Dictionary<string, WordData> cachedWords;

	void Awake () {
        cachedWords = new Dictionary<string, WordData>();

        LoadJSON();

        print("JSON loaded");

        MakeAllNodes();

        MakeAllConnections();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            IncreaseConnectionAlpha(.1f);
        } else if(Input.GetKeyDown(KeyCode.DownArrow)) {
            IncreaseConnectionAlpha(-.1f);
        }
    }

    void LoadJSON(){
        TextAsset jsonText =  Resources.Load(storyJsonFile) as TextAsset;
        WordListWrapper wordListWrapper = JsonUtility.FromJson<WordListWrapper>(jsonText.text);

        wordList = wordListWrapper.wordList;
    }

    void MakeAllNodes(){
        sortWordList(wordList);

        wordNodeDict = new Dictionary<string, GameObject>();
        foreach(WordData word in wordList) {
            if(!wordNodeDict.ContainsKey(word.getWord())){
                GameObject newNode = createWordNode(word);
                wordNodeDict.Add(word.getWord(), newNode);
                
                foreach(PosData pos in word.posList){
                    foreach(WordFreq wordFreq in pos.followedBy){
                        if(!wordNodeDict.ContainsKey(wordFreq.word) && wordFreq.freq < 5){
                            WordData data = GetWordData(wordFreq);
                            if(data.getFreq() < 5) wordNodeDict.Add(data.getWord(), createWordNodeArround(data, newNode));
                        }
                    }
                }

            }
        }
    }

    WordData GetWordData(WordFreq wordFreq){
        if(cachedWords.ContainsKey(wordFreq.word)){
            return cachedWords[wordFreq.word];
        }else{
            foreach(WordData data in wordList){
                
                if(data.getWord() == wordFreq.word){
                    cachedWords.Add(data.getWord(), data);
                    return data;
                }
            }

            return new WordData();
        }
    }

    void sortWordList(List<WordData> listOWordData){
        listOWordData.Sort((a, b) => (b.getFreq().CompareTo(a.getFreq())));
    }

    GameObject InstantiateNode(WordData word, Vector3 initPos){
        GameObject nodeObj = Instantiate(wordNodePrefab, initPos, new Quaternion(), nodeField);
        nodeObj.name = nodeObj.name.Replace("(Clone)", word.ToString());
        nodeObj.GetComponent<Node>().wordData = word;

        nodeObj.GetComponent<Node>().scaleCorrectly();
        return nodeObj;  
    }

    GameObject createWordNode(WordData word){
        int maxRange = 100;
        Vector3 pos = pickRandomPoint(maxRange);

        GameObject nodeObj = InstantiateNode(word, pos);

        while(nodeObj.GetComponent<Node>().colliding()){
            maxRange += 10;
            nodeObj.transform.SetPositionAndRotation(pickRandomPoint(maxRange), new Quaternion());
        }

        return nodeObj;   
    }

    GameObject createWordNodeArround(WordData word, GameObject solarCenter){
        Vector3 solarCenterPos = solarCenter.transform.position;
        float radius = solarCenter.GetComponent<Node>().nodeBodyObj.GetComponent<SphereCollider>().radius + 2;

        Vector3 newPos = pickPointOnSphere(solarCenterPos, radius);

        GameObject nodeObj = InstantiateNode(word, newPos);
        
        while(nodeObj.GetComponent<Node>().colliding()){
            radius += 2;
            nodeObj.transform.SetPositionAndRotation(pickPointOnSphere(solarCenterPos, radius), new Quaternion());
        }

        return nodeObj;
    }

    void MakeAllConnections(){
        wordConnectionDict = new Dictionary<string,Dictionary<string, Dictionary<string, GameObject>>>();

        connectionList = new List<GameObject>();
        foreach(WordData word in wordList){
            print(word.getWord());
            wordConnectionDict.Add(word.getWord(), new Dictionary<string, Dictionary<string, GameObject>>());
            foreach(PosData pos in word.posList){
                print("     "+pos.getPOS());
                wordConnectionDict[word.getWord()].Add(pos.getPOS(), new Dictionary<string, GameObject>());
                foreach(WordFreq following in pos.followedBy){
                    print("          "+following.word);
                    GameObject connection = createConnection(word, pos, following);
                    wordConnectionDict[word.getWord()][pos.getPOS()].Add(following.word + ":" + following.pos, connection);
                    connectionList.Add(connection);
                }
            }
        }
    }

    GameObject createConnection(WordData firstNode, PosData simplePos, WordFreq secondNode){
        GameObject firstNodeObj = wordNodeDict[firstNode.getWord()];
        GameObject secondNodeObj = wordNodeDict[secondNode.word];

        GameObject connector = Instantiate(connectionPrefab, firstNodeObj.GetComponent<Node>().connectorHolder.transform);

        connector.GetComponent<Connection>().setEndPoints(firstNodeObj.gameObject, secondNodeObj);


        firstNodeObj.GetComponent<Node>().followingNodes.Add(secondNodeObj);
        secondNodeObj.GetComponent<Node>().followsNodes.Add(firstNodeObj);

        WordFreq firstWordFreq = new WordFreq();
        firstWordFreq.word = firstNode.wordFreq.word;
        firstWordFreq.pos = simplePos.getPOS();
        firstWordFreq.freq = simplePos.getFreq();

        connector.name = connector.name.Replace("Clone", firstWordFreq.ToString() + "->" + secondNode);

        firstNodeObj.GetComponent<Node>().connectorDict.Add(secondNode, connector);
        secondNodeObj.GetComponent<Node>().connectorDict.Add(firstWordFreq, connector);

        return connector;
    }


    Vector3 pickRandomPoint(float max){
        Vector3 point = new Vector3();
        point.x = (Random.value - .5f)*max;
        point.y = (Random.value - .5f)*max;
        point.z = (Random.value - .5f)*max;

        //print("new pos" + point);
        return point;
    }

    Vector3 pickPointOnSphere(Vector3 center, float radius){
        return Random.onUnitSphere * radius + center;
    }

    void IncreaseConnectionAlpha(float amount){
        foreach(GameObject connection in connectionList){
            float alpha = connection.GetComponent<LineRenderer>().startColor.a;
            connection.GetComponent<Connection>().setLineAlpha(alpha + amount);
        }
    }
}
