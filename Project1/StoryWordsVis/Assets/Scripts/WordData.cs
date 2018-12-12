using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WordListWrapper{
    public List<WordData> wordList;
}

[System.Serializable]
public class WordData  {
    public WordFreq wordFreq;
    public List<PosData> posList;

    public WordData(){
        posList = new List<PosData>();
    }

    public WordData(WordFreq wf){
        wordFreq = wf;
        posList = new List<PosData>();
    }

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
public class PosData {
    public WordFreq posFreq;
    public List<WordFreq> followedBy;

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
public class WordFreq{
    public string word;
    public string pos;
    public int freq;

    public override string ToString() {
        return "[" + word + ", " + pos + ":" + freq + "]";
    }
}

public enum PosCat{
    noun,
    verb,
    adjective,
    adverb,
    punctuation,
    other
}

public class PosCatHelpers{

    static public PosCat getSimplePos(PosData pos){
        return getSimplePos(pos.getPOS());
    }    

    static public PosCat getSimplePos(string pos){
        switch(pos){
                case "NN":
                case "NNS":
                case "NNP":
                case "NNPS":
                case "PRP":
                    return PosCat.noun;
                case "VB":
                case "VBD":
                case "VBG":
                case "VBN":
                case "VBP":
                case "VBZ":
                case "WP":
                case "WP$":
                case "WRB":
                case "WDT":
                    return PosCat.verb;
                case "JJ":
                case "JJR":
                case "JJS":
                    return PosCat.adjective;
                case "RB":
                case "RBR":
                case "RBS":
                    return PosCat.adverb;
                case ".":
                case ",":
                case "'":
                case "\"":
                case "`":
                case "``":
                    return PosCat.punctuation;      
                case "DT":
                case "CC":
                case "IN":
                case "POS":
                case "PRP$":
                case "TO":
                default:
                    return PosCat.other;
                    
        }
    }

    static public Color getColor(PosCat pos){
        switch(pos){
            case PosCat.noun:
                return Color.red;
            case PosCat.verb:
                return Color.green;
            case PosCat.adjective:
                return Color.magenta;
            case PosCat.adverb:
                return Color.yellow;
            case PosCat.punctuation:
                return Color.blue;
            case PosCat.other:
                return Color.black;
            default:
                return Color.white;
        }
    }

}