using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PhraseSelector : Singleton<PhraseSelector> {

	readonly string[] Phrases = {
		"It is a capital mistake to theorize before one has data.", 
		// Insensibly one begins to twist facts to suit theories, instead of theories to suit facts.";
		"Make America Great Again!",
		"You see, but you do not observe.",
		"Sometimes it's the very people who no one imagines anything of who do the things no one can imagine.",
		"You can torture us, and bomb us, or burn our districts to the ground. But do you see that? Fire is catching... If we burn... you burn with us!",
		"It is the things we love most that destroy us.",
		"Greed, for lack of a better word, is good.",
		"I am not a number, I am a free man.",
		"In the face of overwhelming odds, I'm left with only one option, I'm gonna have to science the shit out of this.",
		"You're waiting for a train, a train that will take you far away. You know where you hope this train will take you, but you don't know for sure. But it doesn't matter. How can it not matter to you where that train will take you?",
		"Gentleman, you had my curiosity, now you have my attention.",
		"A million dollars isn't cool. You know what's cool? A billion dollars.",
		"Congratulations San Francisco, you've ruined pizza! First the Hawaiians, and now YOU!",
		"May the odds be ever in your favor.",
		"Carpe diem.",
		"One should use common words to say uncommon things.",
		"Perfection is achieved, not when there is nothing more to add, but when there is nothing left to take away.",
		"Be the change that you wish to see in the world.",
		"A delayed game is eventually good, but a rushed game is forever bad.",
		"Necessity is the mother of invention.",
		"I would be the least among men with dreams and the desire to fulfil them, rather than the greatest with no dreams and no desires.",
		"All men dream: but not equally. Those who dream by night in the dusty recesses of their minds wake in the day to find that it was vanity:" +
		" but the dreamers of the day are dangerous men, for they may act their dreams with open eyes, to make it possible.",
		// Sun Tzu
		"Appear weak when you are strong, and strong when you are weak.",
		"All warfare is based on deception. Hence, when we are able to attack, we must seem unable; when using our forces, we must appear inactive; when we are near, we must make the enemy believe we are far away; when far away, we must make him believe we are near.",
		// Ender's Game
		"In the moment when I truly understand my enemy, understand him well enough to defeat him, then in that very moment I also love him. I think it’s impossible to really understand somebody, what they want, what they believe, and not love them the way they love themselves. And then, in that very moment when I love them.... I destroy them.",
		"Remember, the enemy's gate is down.",
		"I am your enemy, the first one you've ever had who was smarter than you. There is no teacher but the enemy. No one but the enemy will tell you what the enemy is going to do. No one but the enemy will ever teach you how to destroy and conquer. Only the enemy shows you where you are weak." +
		" Only the enemy tells you where he is strong. And the rules of the game are what you can do to him and what you can stop him from doing to you. I am your enemy from now on. From now on I am your teacher.",
		// Lao Tzu
		"Knowing others is intelligence;\nknowing yourself is true wisdom.\nMastering others is strength; \nmastering yourself is true power.",
		"Those who know do not speak. Those who speak do not know.",
		"Care about what other people think and you will always be their prisoner.",
		// Steve Jobs
		"Here’s to the crazy ones. The rebels. The troublemakers. The ones who see things differently. While some may see them as the crazy ones, we see genius. Because the people who are crazy enough to think they can change the world, are the ones who do."

	};

	string[] PhrasesShuffled;
	int nextPhraseIndex = 0;

	public int PhraseNumber {
		get {
			if (nextPhraseIndex == 0)
				return PhrasesShuffled.Length;
			return nextPhraseIndex;
		}
	}

	public int PhraseCount {
		get {
			if (PhrasesShuffled == null)
				return 0;
			
			return PhrasesShuffled.Length;
		}
	}

	string[] PickPhrases() {
		var result = new List<string>();

		var phrasesSorted = Phrases.OrderBy(x => x.Length).ToArray();

		for (int i = 0; i < phrasesSorted.Length; i += 2) {
			if (Random.value > 0.5f || i >= Phrases.Length - 1) {
				result.Add(phrasesSorted[i]);
			} else
				result.Add(phrasesSorted[i + 1]);
		}

		return result.ToArray();
	}

	// Use this for initialization
	void Start () {
		PhrasesShuffled = PickPhrases();
			//Phrases.OrderBy(x => x.Length).ToArray();
//			AsRandom().ToArray();

	}

	public string GetNextPhrase() {
		string phrase = PhrasesShuffled[nextPhraseIndex];
		nextPhraseIndex = (nextPhraseIndex + 1) % PhrasesShuffled.Length;
		return phrase;
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
