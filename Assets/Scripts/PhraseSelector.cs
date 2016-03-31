using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Phrase {

	public readonly string Quote;
	public string Source {
		get {
			// TODO: italics?

			if (IsGood(author) && IsGood(work)) {
				return string.Format("{0}, {1}", author, work);
			}  

			if (IsGood(author)) {
				return author;
			}

			return "";
		}
	}

	private string author;
	private string work;

	public Phrase(string quote, string author, string work) {
		this.Quote = quote;
		this.author = author;
		this.work = work;
	}

	private bool IsGood(string s) {
		return s != null && s != "";
	}

}

public class PhraseSelector : Singleton<PhraseSelector> {

	readonly Phrase[] Phrases = new Phrase[] {
		new Phrase("It is a capital mistake to theorize before one has data. Insensibly one begins to twist facts to suit theories, instead of theories to suit facts.", 
		"Sherlock Holmes", 
		"A Study in Scarlet"), 
		new Phrase("Make America Great Again!", 
		"Donald Trump", 
		""),
		new Phrase("You see, but you do not observe.", 
		"Sherlock Holmes", 
		"The Adventures of Sherlock Holmes (1892)"),
		new Phrase("Sometimes it's the very people who no one imagines anything of who do the things no one can imagine.", 
		"Alan Turing", 
		""),
		new Phrase("You can torture us, and bomb us, or burn our districts to the ground. But do you see that? Fire is catching... If we burn... you burn with us!", 
		"Katniss Everdeen", 
		"The Hunger Games: Catching Fire"),
		new Phrase("It is the things we love most that destroy us.", 
		"President Snow", 
		"The Hunger Games: Mockingjay"),
		new Phrase("Greed, for lack of a better word, is good.", 
		"Gordon Gekko", 
		"Wall Street (1987)"),
		new Phrase("I am not a number, I am a free man.", 
			"Number Six", 
			"The Prisoner"),
		new Phrase("In the face of overwhelming odds, I'm left with only one option, I'm gonna have to science the shit out of this.", 
			"Mark Watney", 
			"The Martian (2015)"),
		new Phrase("You're waiting for a train, a train that will take you far away. You know where you hope this train will take you, but you don't know for sure. But it doesn't matter. How can it not matter to you where that train will take you?", 
		"Mal", 
			"Inception (2010)"),
		new Phrase("Gentleman, you had my curiosity, now you have my attention.", 
			"Calvin Candie", 
			"Django Unchained (2012)"),
		new Phrase("A million dollars isn't cool. You know what's cool? A billion dollars.", 
		"Sean Parker", 
			"The Social Network (2010)"),
		new Phrase("Congratulations San Francisco, you've ruined pizza! First the Hawaiians, and now YOU!", 
		"Anger", 
			"Inside Out (2015)"),
		new Phrase("May the odds be ever in your favor.", 
		"Katniss Everdeen", 
			"The Hunger Games (2012)"),
		new Phrase("Carpe diem.", 
		"Horace", 
			"Odes (23 BC)"),
		new Phrase("One should use common words to say uncommon things.", 
			"Arthur Schopenhauer", 
		"The Art of Literature"),
		new Phrase("Perfection is achieved, not when there is nothing more to add, but when there is nothing left to take away.", 
			"Antoine de Saint-Exupery", 
		""),
		new Phrase("Be the change that you wish to see in the world.", 
			"Mahatma Gandhi", 
		""),
		new Phrase("A delayed game is eventually good, but a rushed game is forever bad.", 
			"Shigeru Miyamoto", 
		""),
		new Phrase("Necessity is the mother of invention.", 
			"Plato", 
			"The Republic, Book II, 369c"),
		new Phrase("I would be the least among men with dreams and the desire to fulfil them, rather than the greatest with no dreams and no desires.", 
			"Kahlil Gibran", 
		""),
		new Phrase("All men dream: but not equally. Those who dream by night in the dusty recesses of their minds wake in the day to find that it was vanity:"
			+ " but the dreamers of the day are dangerous men, for they may act their dreams with open eyes, to make it possible.", 
			"T. E. Lawrence", 
		""),
		// Sun Tzu
		new Phrase("Appear weak when you are strong, and strong when you are weak.", 
		"Sun Tzu", 
		"The Art of War"),
		new Phrase("All warfare is based on deception. Hence, when we are able to attack, we must seem unable; when using our forces, we must appear inactive; when we are near, we must make the enemy believe we are far away; when far away, we must make him believe we are near.", 
		"Sun Tzu", 
		"The Art of War"),
		// Ender's Game
		new Phrase("In the moment when I truly understand my enemy, understand him well enough to defeat him, then in that very moment I also love him. I think it’s impossible to really understand somebody, what they want, what they believe, and not love them the way they love themselves. And then, in that very moment when I love them.... I destroy them.", 
			"Andrew Wiggin", 
		"Ender's Game"),
		new Phrase("Remember, the enemy's gate is down.", 
			"Andrew Wiggin", 
		"Ender's Game"),
		new Phrase("I am your enemy, the first one you've ever had who was smarter than you. There is no teacher but the enemy. No one but the enemy will tell you what the enemy is going to do. No one but the enemy will ever teach you how to destroy and conquer. Only the enemy shows you where you are weak."
			+ " Only the enemy tells you where he is strong. And the rules of the game are what you can do to him and what you can stop him from doing to you. I am your enemy from now on. From now on I am your teacher.",
			"Mazer Rackham", 
		"Ender's Game"),
		// Lao Tzu
		new Phrase("Knowing others is intelligence;\nknowing yourself is true wisdom.\nMastering others is strength; \nmastering yourself is true power.", 
		"Lao Tzu", 
		"Tao Te Ching"),
		new Phrase("Those who know do not speak. Those who speak do not know.", 
			"Lao Tzu", 
		"Tao Te Ching"),
		new Phrase("Care about what other people think and you will always be their prisoner.", 
		"Lao Tzu", 
		"Tao Te Ching"),
		// Steve Jobs
		new Phrase("Here’s to the crazy ones. The rebels. The troublemakers. The ones who see things differently. While some may see them as the crazy ones, we see genius. Because the people who are crazy enough to think they can change the world, are the ones who do.", 
		"Steve Jobs", 
		""),
		// V for Vendetta
		new Phrase("Remember, remember, the Fifth of November.", 
		"V", 
			"V for Vendetta (2005)"),
		// Einstein
		new Phrase("There are only two ways to live your life. One is as if nothing is a miracle. The other is as if everything is.", 
		"Albert Einstein", 
		""),
		// Henry Ford
		new Phrase("If I had asked people what they wanted, they would have said faster horses.", 
		"Henry Ford", 
		""),
		// Eleanor Roosevelt
		new Phrase("Great minds discuss ideas, average minds discuss events, small minds discuss people.", 
			"Eleanor Roosevelt", 
		""),
		// John Dalberg-Acton
		new Phrase("Power tends to corrupt, and absolute power corrupts absolutely. Great men are almost always bad men.", 
			"Lord John Dalberg-Acton", 
		""),
		// John Milton
		new Phrase("The mind is its own place, and in itself can make a heaven of hell, a hell of heaven.", 
			"John Milton", 
		""),
		// Joan Rivers TODO: include professions
		new Phrase("Listen, I wish I could tell you it gets better, but it doesn’t get better. You get better.", 
			"Joan Rivers", 
		""),
		// Seneca
		new Phrase("It is not because things are difficult that we do not dare; it is because we do not dare that they are difficult.", 
			"Lucius Annaeus Seneca", 
		""),
		// Dark Knight Rises
		new Phrase("Some men just want to watch the world burn.", 
			"Alfred Pennyworth", 
			"The Dark Knight (2008)"), // Alfred
		new Phrase("He's the hero Gotham deserves, but not the one it needs right now. So we'll hunt him. Because he can take it. Because he's not our hero. He's a silent guardian. A watchful protector.", 
			"Lt. James Gordon", 
			"The Dark Knight (2008)"),
		new Phrase("You either die a hero or live long enough to see yourself become the villain.", 
		"Harvey Dent", 
			"The Dark Knight (2008)"),
		new Phrase("Why so serious?", 
		"The Joker", 
			"The Dark Knight (2008)"),
		// Alias Turbo
		new Phrase("Judge not, that you be not judged. For with what judgment you judge, you will be judged; and with the measure you use, it will be measured back to you.", 
			"Matthew 7:1-3", 
		""),
		new Phrase("If I have seen farther than others it is because I have stood on the shoulders of giants.", 
		"Isaac Newton", 
		""), //- I. Newton
		new Phrase("No one saves us but ourselves. No one can and no one may. We ourselves must walk the path.", 
			"Gautama Buddha", 
		""), // - Buddha
		new Phrase("He who asks is a fool for five minutes, but he who does not ask remains a fool forever.", 
			"Chinese Proverb", 
		""), // Chinese Proverb
		new Phrase("War doesn't decide who's right. War decides who's left.", 
			"Bertrand Russell", 
		""),
		new Phrase("The human sacrificed himself, to save the Pokemon. I pitted them against each other, but not until they set aside their differences did I see the true power they all share deep inside. I see now that the circumstances of one's birth are irrelevant; it is what you do with the gift of life that determines who you are.", 
		"Mewtwo", 
			"Pokémon: The First Movie - Mewtwo Strikes Back (1998)"), // Mewtwo
		new Phrase("If most of us are ashamed of shabby clothes and shoddy furniture, let us be more ashamed of shabby ideas and shoddy philosophies.", 
		"Albert Einstein", 
		""),
		new Phrase("A wet man does not fear the rain.", 
			"Anonymous", 
		"")
	};

	public event System.Action NewPhrase;

	Phrase[] PhrasesShuffled;
	int nextPhraseIndex = 0;

	public int PhraseNumber {
		get {
			if (nextPhraseIndex == 0 && PhrasesShuffled != null)
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

	Phrase[] PickPhrases() {
		var result = new List<Phrase>();

		var phrasesSorted = Phrases.OrderBy(x => x.Quote.Length).ToArray();

		for (int i = 0; i < phrasesSorted.Length; i += 2) {
			if (Random.value > 0.5f || i >= Phrases.Length - 1) {
				result.Add(phrasesSorted[i]);
			} else
				result.Add(phrasesSorted[i + 1]);
		}

		return result.ToArray();
	}

	void OnNewPhrase() {
		if (NewPhrase != null) 
			NewPhrase();
	}

	// Use this for initialization
	void Start () {
		PhrasesShuffled = PickPhrases();
			//Phrases.OrderBy(x => x.Length).ToArray();
//			AsRandom().ToArray();

	}

	public Phrase GetNextPhrase() {
		int numPhrases = PhrasesShuffled.Length;
		OnNewPhrase();

		Phrase phrase = PhrasesShuffled[nextPhraseIndex];
		nextPhraseIndex = (nextPhraseIndex + 1) % PhrasesShuffled.Length;
		return phrase;
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
