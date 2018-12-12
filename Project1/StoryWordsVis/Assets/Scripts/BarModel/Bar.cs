using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

    public WordData wordData;


    public void SizeCorrectly(){
        Vector3 newScale = transform.localScale;
        newScale.y = wordData.getFreq();

        transform.localScale = newScale;

        Vector3 newPos = transform.position;
        newPos.y = newScale.y/ 2;

        transform.position = newPos;
    }
}
