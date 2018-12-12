using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : Manager {
    public GameObject barPrefab;


    void Awake () {
        LoadJSON();

        sortWordList(wordList);

        for(int i=0; i< wordList.Count; i++){
            GameObject bar = Instantiate(barPrefab);
            bar.GetComponent<Bar>().wordData = wordList[i];

            bar.transform.position = new Vector3(Mathf.Cos(Mathf.PI*2 *i/wordList.Count), 0, Mathf.Sin(Mathf.PI*2 *i/wordList.Count))*100;

            bar.GetComponent<Bar>().SizeCorrectly();

            bar.GetComponent<MeshRenderer>().material.color = pickColor(wordList[i].getWord());
        }

    }


    Color pickColor(string word){
        if(word.Length > 9){
            return Color.red;
        }else if(word.Length > 5){
            return Color.yellow;
        }else if(word.Length > 2){
            return Color.green;
        }else{
            return Color.blue;
        }

    }

}
