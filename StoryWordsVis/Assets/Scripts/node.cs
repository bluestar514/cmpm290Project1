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

        scaleCorrectly();
	}


    public float scaleCorrectly(){
        nodeBodyObj.transform.localScale *= wordData.getFreq()/50;

        return nodeBodyObj.GetComponent<SphereCollider>().radius*nodeBodyObj.transform.localScale.x;
    }


    public bool colliding(){
        //Transform parent = transform.parent;

        //Collider[] hitColliders = Physics.OverlapSphere(nodeBodyObj.transform.position, 
        //                            nodeBodyObj.GetComponent<SphereCollider>().radius*nodeBodyObj.transform.localScale.x);
        //int i = 0;
        //while (i < hitColliders.Length){
        //    if(hitColliders[i] == nodeBodyObj.GetComponent<Collider>()) continue;
        //    else return true;
        //}

        //return false;

        Bounds myBound = nodeBodyObj.GetComponent<Collider>().bounds;
        foreach(Transform sibling in parent) {
            if(sibling == transform) continue;
            else {
                Bounds siblingBound = sibling.GetComponent<node>().nodeBodyObj.GetComponent<Collider>().bounds;
                if(myBound.Intersects(siblingBound)) {
                    print("Intersecting");
                    return true;
                }
                if(siblingBound.Contains(transform.position)) {
                    print("I am inside existing");
                    return true;
                }
                if(myBound.Contains(sibling.position)) {
                    print("Existing is inside me");
                    return true;
                }

            }
        }
        print("No collision");
        return false;
    }
}
