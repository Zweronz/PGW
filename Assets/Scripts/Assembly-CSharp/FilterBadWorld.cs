using System.Text.RegularExpressions;
using UnityEngine;

public class FilterBadWorld : MonoBehaviour
{
	private const string string_0 = "***";

	private const string string_1 = "\\b({0})(s?)\\b";

	private const RegexOptions regexOptions_0 = RegexOptions.IgnoreCase;

	private static string[] string_2 = new string[222]
	{
		"drugs", "drugz", "alcohol", "penis", "vagina", "sexx", "sexxy", "boobs", "cumshot", "facial",
		"masturbate", "nipples", "orgasm", "slut", "porn", "porno", "pornography", "ass", "arse", "assbag",
		"assbandit", "assbanger", "assbite", "asscock", "assfuck", "asshead", "asshole", "asshopper", "asslicker", "assshole",
		"asswipe", "bampot", "bastard", "beaner", "bitch", "blow job", "blowjob", "boner", "brotherfucker", "bullshit",
		"butt plug", "butt-pirate", "buttfucker", "camel toe", "carpetmuncher", "chink", "choad", "chode", "clit", "cock",
		"cockbite", "cockface", "cockfucker", "cockmaster", "cockmongruel", "cockmuncher", "cocksmoker", "cocksucker", "coon", "cooter",
		"cracker", "cum", "cumtart", "cunnilingus", "cunt", "cunthole", "damn", "deggo", "dick", "dickbag",
		"dickhead", "dickhole", "dicks", "dickweed", "dickwod", "dildo", "dipshit", "dookie", "douche", "douchebag",
		"douchewaffle", "dumass", "dumb ass", "dumbass", "dumbfuck", "dumbshit", "dyke", "fag", "fagbag", "fagfucker",
		"faggit", "faggot", "fagtard", "fatass", "fellatio", "fuck", "fuckass", "fucked", "fucker", "fuckface",
		"fuckhead", "fuckhole", "fuckin", "fucking", "fucknut", "fucks", "fuckstick", "fucktard", "fuckup", "fuckwad",
		"fuckwit", "fudgepacker", "gay", "gaydo", "gaytard", "gaywad", "goddamn", "goddamnit", "gooch", "gook",
		"gringo", "guido", "hard on", "heeb", "hell", "ho", "homo", "homodumbshit", "honkey", "humping",
		"jackass", "jap", "jerk off", "jigaboo", "jizz", "jungle bunny", "kike", "kooch", "kootch", "kyke",
		"lesbian", "lesbo", "lezzie", "mcfagget", "mick", "minge", "mothafucka", "motherfucker", "motherfucking", "muff",
		"negro", "nigga", "nigger", "niglet", "nut sack", "nutsack", "paki", "panooch", "pecker", "peckerhead",
		"penis", "piss", "pissed", "pissed off", "pollock", "poon", "poonani", "poonany", "porch monkey", "porchmonkey",
		"prick", "punta", "pussy", "pussylicking", "puto", "queef", "queer", "queerbait", "renob", "rimjob",
		"sand nigger", "sandnigger", "schlong", "scrote", "shit", "shitcunt", "shitdick", "shitface", "shitfaced", "shithead",
		"shitter", "shittiest", "shitting", "shitty", "skank", "skeet", "slut", "slutbag", "snatch", "spic",
		"spick", "splooge", "tard", "testicle", "thundercunt", "tit", "titfuck", "tits", "twat", "twatlips",
		"twats", "twatwaffle", "va-j-j", "vag", "vjayjay", "wank", "wetback", "whore", "whorebag", "wop",
		"sex", "sexy"
	};

	public static string FilterString(string string_3)
	{
		string[] array = new string[19]
		{
			".", ",", "%", "!", "@", "#", "$", "*", "&", ";",
			":", "?", "/", "<", ">", "|", "-", "_", "\""
		};
		string text = string_3;
		string text2 = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text = text.Replace(array[i], " ");
		}
		text = text.ToLower();
		int num = 0;
		for (int num2 = text.IndexOf(" ", 0); num2 != -1; num2 = ((num > text.Length - 1) ? (-1) : text.IndexOf(" ", num)))
		{
			text2 = ((!scanMatInWold(text.Substring(num, num2 - num))) ? (text2 + string_3.Substring(num, num2 - num + 1)) : (text2 + "***" + string_3.Substring(num2, 1)));
			num = num2 + 1;
		}
		if (num < text.Length)
		{
			text2 = ((!scanMatInWold(text.Substring(num, text.Length - num))) ? (text2 + string_3.Substring(num, text.Length - num)) : (text2 + "***"));
		}
		return text2;
	}

	private static bool scanMatInWold(string string_3)
	{
		if (string_3.Length < 3)
		{
			return false;
		}
		string[] array = string_2;
		int num = 0;
		while (true)
		{
			if (num < array.Length)
			{
				string text = array[num];
				if (text.Equals(string_3))
				{
					break;
				}
				num++;
				continue;
			}
			return false;
		}
		return true;
	}
}
