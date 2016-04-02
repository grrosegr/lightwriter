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
				return string.Format("{0},\n{1}", author, work);
			}  

			if (IsGood(author)) {
				return author;
			}

			if (IsGood(work)) {
				return work;
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
		new Phrase("You know, you really don't need a forensics team to get to the bottom of this. If you guys were the inventors of Facebook, you'd have invented Facebook.",
			"Mark Zuckerberg",
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
//		new Phrase("I am your enemy, the first one you've ever had who was smarter than you. There is no teacher but the enemy. No one but the enemy will tell you what the enemy is going to do. No one but the enemy will ever teach you how to destroy and conquer. Only the enemy shows you where you are weak."
//			+ " Only the enemy tells you where he is strong. And the rules of the game are what you can do to him and what you can stop him from doing to you. I am your enemy from now on. From now on I am your teacher.",
//			"Mazer Rackham", 
//		"Ender's Game"),
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
			"Pokémon: The First Movie (1998)"), // Mewtwo
		new Phrase("We do have a lot in common. The same air, the same Earth, the same sky. Maybe if we started looking at what's the same instead of always looking at what's different, ...well, who knows?",
			"Meowth",
			"Pokémon: The First Movie (1998)"),

		new Phrase("If most of us are ashamed of shabby clothes and shoddy furniture, let us be more ashamed of shabby ideas and shoddy philosophies.", 
		"Albert Einstein", 
		""),
		new Phrase("A wet man does not fear the rain.", 
			"Anonymous", 
		""),
		new Phrase("Do not try and bend the spoon. That's impossible. Instead... only try to realize the truth.",
			"The Matrix",
			""),
		new Phrase("You take the blue pill - the story ends, you wake up in your bed and believe whatever you want to believe. You take the red pill - you stay in Wonderland and I show you how deep the rabbit-hole goes.",
			"Morpheus",
			"The Matrix"),
		new Phrase("Have you ever had a dream, Neo, that you were so sure was real? What if you were unable to wake from that dream? How would you know the difference between the dream world and the real world?",
			"Morpheus",
			"The Matrix"),
		new Phrase("Let me tell you why you're here. You're here because you know something. What you know you can't explain, but you feel it. You've felt it your entire life, that there's something wrong with the world. You don't know what it is, but it's there, like a splinter in your mind, driving you mad. It is this feeling that has brought you to me.",
			"Morpheus",
			"The Matrix"),
		// http://www.buzzfeed.com/morenikeadebayo/surprisingly-heartwarming-pokemon-games-best-advice#.klQRz5AJk
		new Phrase("All dreams are but another reality. Never forget...",
			"A Signpost on Southern Island",
			"Pokemon Ruby"),
		new Phrase("Those whose memories fade seek to carve them in their hearts...",
			"A Signpost on Southern Island",
			"Pokemon Ruby"),
		new Phrase("The more wonderful the meeting, the sadder the parting.",
			"Looker on Stark Mountain",
			"Pokémon Diamond and Pearl"),
		new Phrase("The greatest trick the Devil ever pulled was convincing the world he didn't exist.",
			"Verbal",
			"The Usual Suspects (1995)"),
		new Phrase("I'm going to make him an offer he can't refuse.",
			"Vito Corleone",
			"The Godfather (1972"),
		new Phrase("Oh, don't be silly darling, everyone wants this. Everyone wants to be us.",
			"Miranda Priestly",
			"The Devil Wears Prada (2006)"),
		new Phrase("I'm sorry, do you have some prior commitment? Some hideous skirt convention you have to go to?",
			"Emily",
			"The Devil Wears Prada (2006)"),
		new Phrase("Cognitive re-calibration. I hit you really hard in the head.",
			"Natasha Romanoff",
			"The Avengers (2012)"),
		new Phrase("You know, the last time I was in Germany and saw a man standing above everybody else, we ended up disagreeing.",
			"Steve Rogers (Captain America)",
			"The Avengers (2012)"),
		new Phrase("Thanks, but the last time I was in New York I kind of broke... Harlem.",
			"Bruce Banner (Hulk)",
			"The Avengers (2012)"),
		new Phrase("Here's some advice. Stay alive.",
			"Haymitch Abernathy",
			"The Hunger Games"),
		new Phrase("Darkness cannot drive out darkness; only light can do that. Hate cannot drive out hate; only love can do that.",
			"Martin Luther King, Jr.",
			""),
		new Phrase("It's kind of fun to do the impossible.",
			"Walt Disney",
			""),
		new Phrase("Around here, however, we don't look backwards for very long. We keep moving forward, opening up new doors and doing new things, because we're curious...and curiosity keeps leading us down new paths.",
			"Walt Disney",
			""),
		new Phrase("First they ignore you, then they laugh at you, then they fight you, then you win.",
			"Mahatma Gandhi",
			""),
		new Phrase("Stay Hungry, Stay Foolish.",
			"Steve Jobs",
			""),
		new Phrase("We all have secrets: the ones we keep... and the ones that are kept from us.",
			"Peter",
			"The Amazing Spider-Man"),
		new Phrase("Toto, I have a feeling we're not in Kansas any more.",
			"Dorothy",
			"The Wizard of Oz"),
		new Phrase("Pay no attention to that man behind the curtain.",
			"Wizard of Oz",
			"The Wizard of Oz"),
		new Phrase("I feel the need... the need for speed!",
			"Maverick",
			"Top Gun (1986)"),
		new Phrase("These aren't the droids you're looking for.",
			"Ben Obi-Wan Kenobi",
			"Star Wars: Episode IV - A New Hope (1977)"),
		new Phrase("Do. Or do not. There is no try",
			"The Empire Strikes Back",
			"Star Wars: Episode V - The Empire Strikes Back (1980)"),
		new Phrase("I find your lack of faith disturbing.",
			"Darth Vader",
			"Star Wars: Episode IV - A New Hope (1977)"),
		new Phrase("May the Force be with you.",
			"",
			"Star Wars"),
		new Phrase("Open the pod bay doors, HAL.",
			"Dave Bowman",
			"2001: A Space Odyssey (1968)"),
		new Phrase("I will not make any deals with you. I've resigned. I will not be pushed, filed, stamped, indexed, briefed, debriefed, or numbered! My life is my own!",
			"Number Six",
			"The Prisoner (1967)"),
		new Phrase("Who are you talking to right now? Who is it you think you see? Do you know how much I make a year? I mean, even if I told you, you wouldn't believe it. Do you know what would happen if I suddenly decided to stop going into work? A business big enough that it could be listed on the NASDAQ goes belly up. Disappears! It ceases to exist without me. No, you clearly don't know who you're talking to, so let me clue you in. I am not in danger, Skyler. I am the danger. A guy opens his door and gets shot and you think that of me? No. I am the one who knocks!",
			"Walter White",
			"Breaking Bad (2011)"),
		new Phrase("The things you own end up owning you.",
			"Tyler Durden",
			"Fight Club (1999)"),
		new Phrase("Welcome to Fight Club. The first rule of Fight Club is: you do not talk about Fight Club. The second rule of Fight Club is: you DO NOT talk about Fight Club! ",
			"Tyler Durden",
			"Fight Club (1999)"),
		new Phrase("Roads? Where we're going, we don't need roads.",
			"Dr. Emmett Brown",
			"Back to the Future (1985)"),
		// Bartlett's
		new Phrase("Float like a butterfly, sting like a bee.",
			"Muhammad Ali",
			""),
		new Phrase("All animals are equal, but some animals are more equal than others.",
			"George Orwell",
			"Animal Farm (1945)"),
		new Phrase("So, first of all, let me assert my firm belief that the only thing we have to fear is...fear itself — nameless, unreasoning, unjustified terror which paralyzes needed efforts to convert retreat into advance.",
			"Franklin D. Roosevelt",
			"First inauguration"),
		new Phrase("Those who cannot remember the past are condemned to repeat it.",
			"George Santayana",
			""),
		new Phrase("Ask not what your country can do for you, ask what you can do for your country.",
			"John F. Kennedy",
			"Inauguration"),
		new Phrase("If you're going through hell, keep going.",
			"Winston Churchill",
			""),
		new Phrase("This is not the end, it is not even the beginning of the end, but it is perhaps the end of the beginning.",
			"Winston Churchill",
			"1942 Speech - Turning Point of WW2"),
		new Phrase("Democracy is the worst form of government, except for all the others.",
			"Winston Churchill",
			""),
		new Phrase("We shall defend our island, whatever the cost may be, we shall fight on the beaches, we shall fight on the landing grounds, we shall fight in the fields and in the streets, we shall fight in the hills; we shall never surrender.",
			"Winston Churchill",
			""),
		new Phrase("Happiness can be found, even in the darkest of times, if one only remembers to turn on the light.",
			"Albus Dumbledore",
			"Harry Potter and the Prisoner of Azkaban"),
		new Phrase("The cold never bothered me anyway.",
			"Esla",
			"Frozen (2013"),
		new Phrase("I'm bad, and that's good. I will never be good, and that's not bad. There's no one I'd rather be than me.",
			"Wreck-It Ralph",
			"Wreck-It Ralph (2012)"),
		new Phrase("I'm gonna wreck it!",
			"Wreck-It Ralph",
			"Wreck-It Ralph (2012)"),
		new Phrase("She was programmed with the most tragic backstory ever. The one day she didn't do a perimeter check, her wedding day.",
			"Soldier",
			"Wreck-It Ralph (2012)"),			
		new Phrase("A person is smart. People are dumb, panicky dangerous animals and you know it. Fifteen hundred years ago everybody knew the Earth was the center of the universe. Five hundred years ago, everybody knew the Earth was flat, and fifteen minutes ago, you knew that humans were alone on this planet. Imagine what you'll know tomorrow.",
			"Kay",
			"Men in Black (1997)"),
		new Phrase("You'll conform to the identity we give you, eat where we tell you, live where we tell you. From now on you'll have no identifying marks of any kind. You'll not stand out in any way. Your entire image is crafted to leave no lasting memory with anyone you encounter. You're a rumor, recognizable only as deja vu and dismissed just as quickly. You don't exist; you were never even born. Anonymity is your name. Silence your native tongue. You're no longer part of the System. You're above the System. Over it. Beyond it. We're \"them.\" We're \"they.\" We are the Men in Black.",
			"Zed",
			"Men in Black (1997)"),
		new Phrase("People used to call me a monster and for a long time I believed them, but after awhile, you learn to ignore the names people call you and just trust who you are!",
			"Shrek",
			"Shrek (2001)"),
		new Phrase("When everyone is super... no one will be.",
			"Syndrome",
			"The Incredibles (2004)"),
		new Phrase("Valuing life is not weakness... And disregarding it is not strength.",
			"Mirage",
			"The Incredibles (2004)"),
		new Phrase("I never look back, darling. It distracts from the now.",
			"Edna",
			"The Incredibles (2004)"),
		new Phrase("When we hit our lowest point, we are open to the greatest change.",
			"Avatar Aang",
			"The Legend of Korra"),
		new Phrase("Many things that seem threatening in the dark Become welcoming when we shine light on them.",
			"Iroh",
			"The Legend of Korra"),
		new Phrase("What is the most resilient parasite? Bacteria? A virus? An intestinal worm? An idea. Resilient... highly contagious. Once an idea has taken hold of the brain it's almost impossible to eradicate. An idea that is fully formed - fully understood - that sticks; right in there somewhere.",
			"Cobb",
			"Inception (2010)"),
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
//		var quotesOrdered;

		if (Settings.Instance.LongestQuotesOnly)
			return Phrases.OrderByDescending(x => x.Quote.Length).Take(Settings.Instance.QuotesPerGame).ToArray();

		return Phrases.AsRandom().Take(Settings.Instance.QuotesPerGame).ToArray();

//		var result = new List<Phrase>();
//
//		var phrasesSorted = Phrases.OrderBy(x => x.Quote.Length).ToArray();
//
//		for (int i = 0; i < phrasesSorted.Length; i += 2) {
//			if (Random.value > 0.5f || i >= Phrases.Length - 1) {
//				result.Add(phrasesSorted[i]);
//			} else
//				result.Add(phrasesSorted[i + 1]);
//		}
//
//		return result.ToArray();
	}

	void OnNewPhrase() {
		if (NewPhrase != null) 
			NewPhrase();
	}

	// Use this for initialization
	void Start () {
		print(Phrases.Length + " phrases loaded");
		var longestQuote = Phrases.MaxBy(x => x.Quote.Length);
		Debug.LogFormat("Longest quote ({0} chars): {1}", longestQuote.Quote.Length, longestQuote.Quote);

		PhrasesShuffled = PickPhrases();
			//Phrases.OrderBy(x => x.Length).ToArray();
//			AsRandom().ToArray();

	}

	public Phrase GetNextPhrase() {
		OnNewPhrase();

		Phrase phrase = PhrasesShuffled[nextPhraseIndex];
		nextPhraseIndex = (nextPhraseIndex + 1) % PhrasesShuffled.Length;
		return phrase;
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
