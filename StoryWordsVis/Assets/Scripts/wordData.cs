using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class wordListWrapper{
    public List<wordData> wordList;
}

[System.Serializable]
public class wordData  {
    public wordFreq wordFreq;
    public List<posData> posList;

    public string getWord(){
        return wordFreq.word;
    }
    public string getCommonPos(){
        return wordFreq.pos;
    }    
    public int getFreq(){
        return wordFreq.freq;
    }

    public void setWord(string word){
        wordFreq.word = word;
    }
    public void setCommonPos(string pos){
        wordFreq.word = pos;
    }
    public void setFreq(int freq){
        wordFreq.freq = freq;
    }

    public override string ToString() {
        return "(" + getWord() + ", " + getCommonPos() + ":" + getFreq() + ")";
    }
}

[System.Serializable]
public class posData {
    public wordFreq posFreq;
    public List<wordFreq> followedBy;

    public string getPOS(){
        return posFreq.word;
    }
    public int getFreq(){
        return posFreq.freq;
    }

    public void setPOS(string pos){
        posFreq.word = pos;
    }
    public void setFreq(int freq){
        posFreq.freq = freq;
    }
}

[System.Serializable]
public class wordFreq{
    public string word;
    public string pos;
    public int freq;
}