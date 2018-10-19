import nltk
import sys
import re
import json

import pprint
pp = pprint.PrettyPrinter(indent=4)
# python preprocessing.py textfile.txt jsonDest.json

txtPATH = sys.argv[1]
jsonPATH = sys.argv[2]

txtFile = open(txtPATH, 'r', encoding="utf8", errors='ignore')

txt = txtFile.read()
txtFile.close()

txt_sentences = nltk.sent_tokenize( txt )
print("First sentence: " + txt_sentences[0] +"\n")

token_sentences = []
posTaged_sentences = []
for sent in txt_sentences:
	tokens = nltk.word_tokenize(sent)
	token_sentences.append( tokens )
	posTaged_sentences.append( nltk.pos_tag(tokens) )

print("Tokenized First Sentence: " + str(token_sentences[0]) +"\n")
print("POS First Sentence: " + str(posTaged_sentences[0]) +"\n")

recombined_txt = []
for sentence in posTaged_sentences:
	for word in sentence:
		formattedWord = (word[0].lower(), word[1])
		recombined_txt.append(formattedWord)

wordFrequency = nltk.FreqDist(word[0] for word in recombined_txt)
mostCommonWords = wordFrequency.most_common(15)#len(wordFrequency))
print("15 most common words: " + str(mostCommonWords) +"\n")


freqGivenPOS = nltk.ConditionalFreqDist( (word[1], word[0]) 
										for word in recombined_txt)
print("15 most common nouns: " + str(freqGivenPOS["NN"].most_common(15)) +"\n")

wordFrequency = nltk.FreqDist(recombined_txt)
mostCommonWords = wordFrequency.most_common(15)#len(wordFrequency))
print("15 most common word/pos pairs: "+ str(mostCommonWords) +"\n")

wordDict = {}
for wordPosPair in wordFrequency.most_common(len(wordFrequency)):
	word = wordPosPair[0][0]
	pos = wordPosPair[0][1]
	freq = wordPosPair[1]
	if word not in wordDict:
		wordDict[word] = {pos:{"freq":freq, "followedBy":[]}}
	else:
		wordDict[word][pos] = {"freq":freq, "followedBy":[]}

#print("\nwordDict: ")
#pp.pprint(wordDict)


bigrams = list( nltk.bigrams(recombined_txt) )
print("bigrams: " +str(bigrams[0]) + "\n")

bigramFrequency = nltk.FreqDist(bigrams)
mostCommonBigrams = bigramFrequency.most_common(len(bigramFrequency))#len(wordFrequency))
print("15 most common bigrams: "+ str(mostCommonBigrams[:15]) +"\n")

mostCommonPunctuationlessBigrams = []
for bigram in bigramFrequency.most_common(len(bigramFrequency)):
	word1 = bigram[0][0][0]
	word2 = bigram[0][1][0]
	if re.search('[a-zA-Z0-9]', word1) and re.search('[a-zA-Z0-9]', word2):
		mostCommonPunctuationlessBigrams.append(bigram)

print("15 most common bigrams: "+ str(mostCommonPunctuationlessBigrams[:15]) +"\n")

for bigramCountPair in mostCommonBigrams:
	bigram = bigramCountPair[0]
	freq = bigramCountPair[1]
	word1 = bigram[0][0]
	pos1 = bigram[0][1]
	word2 = bigram[1]


	wordDict[word1][pos1]["followedBy"].append((word2,freq))

wordList = []

for word in wordDict:
	wordFreq = {"word":word}
	wordData = {"wordFreq":wordFreq}
	posList = []

	freq = 0

	mostFreqPos = "none"
	countOfMostFreqPOS = 0

	for pos in wordDict[word]:
		posFreq = {"word":pos,
				   "freq":wordDict[word][pos]["freq"]}
		posData = {"posFreq":posFreq}
		followedBy = []

		freq += wordDict[word][pos]["freq"]

		if countOfMostFreqPOS < wordDict[word][pos]["freq"]:
			mostFreqPos = pos
			countOfMostFreqPOS = wordDict[word][pos]["freq"]

		for follower in wordDict[word][pos]["followedBy"]:
			followerWord = follower[0][0]
			followerPos = follower[0][1]
			followerFreq = follower[1]
			followedBy.append({ "word": followerWord,
								"pos": followerPos,
								"freq": followerFreq})
		posData["followedBy"] = followedBy

		posList.append(posData)

	wordData["posList"] = posList
	wordData["wordFreq"]["freq"] = freq
	wordData["wordFreq"]["pos"] = mostFreqPos

	wordList.append(wordData)

print("\nwordList: ")
pp.pprint(wordList)

jsonFile = open(jsonPATH, 'w')
jsonFile.write(json.dumps({"wordList":wordList}))
jsonFile.close()