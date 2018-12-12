using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class BNode : MonoBehaviour {

    public GameObject nodeBodyObj;
    public GameObject nameObj;
    public GameObject freqObj;
    public GameObject connectorHolder;

    public WordData wordData;

    public List<GameObject> followingNodes;
    public List<GameObject> followsNodes;

    public Dictionary<WordFreq, GameObject> connectorDict;

    public GameObject camera;

    void Awake() {
        followingNodes = new List<GameObject>();
        followsNodes = new List<GameObject>();

        connectorDict = new Dictionary<WordFreq, GameObject>();
    }

    void Start() {

        nameObj.GetComponent<TextMesh>().text = wordData.getWord();
        freqObj.GetComponent<TextMesh>().text = "" + wordData.getFreq();

        hideText();
        setColor();

    }

    public float scaleCorrectly() {
        nodeBodyObj.transform.localScale *= wordData.getFreq() * 5;
        //nameObj.transform.localScale *= wordData.getFreq();
        //freqObj.transform.localScale *= wordData.getFreq();

        //transform.localScale *= wordData.getFreq();

        return nodeBodyObj.GetComponent<SphereCollider>().radius * nodeBodyObj.transform.localScale.x;
    }


    public bool colliding() {
        Transform parent = transform.parent;

        Bounds myBound = nodeBodyObj.GetComponent<Collider>().bounds;

        foreach(Transform sibling in parent) {
            if(sibling == transform) continue;
            else {
                Bounds siblingBound = sibling.GetComponent<BNode>().nodeBodyObj.GetComponent<Collider>().bounds;
                if(myBound.Intersects(siblingBound)) {
                    //print("Intersecting");
                    return true;
                }
                if(siblingBound.Contains(transform.position)) {
                    //print("I am inside existing");
                    return true;
                }
                if(myBound.Contains(sibling.position)) {
                    //print("Existing is inside me");
                    return true;
                }

            }
        }
        //print("No collision");
        return false;
    }

    public void setColor() {
        string nltkPos = wordData.getCommonPos();
        PosCat simplePos = PosCatHelpers.getSimplePos(nltkPos);


        nodeBodyObj.GetComponent<MeshRenderer>().material.color = PosCatHelpers.getColor(simplePos);

    }

    public void showText() {
        nameObj.SetActive(true);
        freqObj.SetActive(true);
    }

    public void hideText() {
        nameObj.SetActive(false);
        freqObj.SetActive(false);
    }
}
