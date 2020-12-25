using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Anilist4Net.Test
{
	[TestFixture]
	public class CharacterTests
	{
		[Test]
		public async Task
			RinnaSawakiByIdTest() // some random character picked purely on whatever query was open at the time
		{
			var client    = new Client();
			var character = await client.GetCharacterById(2504);

			AreEqual(2504,                                                              character.Id);
			AreEqual("Rinna",                                                           character.FirstName);
			AreEqual("Sawaki",                                                          character.LastName);
			AreEqual("Rinna Sawaki",                                                    character.FullName);
			AreEqual("沢城 凛奈",                                                           character.NativeName);
			AreEqual("https://s4.anilist.co/file/anilistcdn/character/large/2504.jpg",  character.ImageLarge);
			AreEqual("https://s4.anilist.co/file/anilistcdn/character/medium/2504.jpg", character.ImageMedium);
			AreEqual("https://anilist.co/character/2504",                               character.SiteUrl);
			Contains(2155,  character.MediaIds);
			Contains(40827, character.MediaIds);
			IsNull(character.ModNotes);
		}

		[Test]
		public async Task NagisaShiotaBySearchTest() // I'm having trouble hiding my biases here.
		{
			var client    = new Client();
			var character = await client.GetCharacterBySearch("nagisa shiota");

			AreEqual(65645,           character.Id);
			AreEqual("Nagisa",        character.FirstName);
			AreEqual("Shiota",        character.LastName);
			AreEqual("Nagisa Shiota", character.FullName);
			AreEqual("潮田渚",           character.NativeName);
			AreEqual("https://s4.anilist.co/file/anilistcdn/character/large/65645-o6Rjx8ud8gmM.png",
			         character.ImageLarge);
			AreEqual("https://s4.anilist.co/file/anilistcdn/character/medium/65645-o6Rjx8ud8gmM.png",
			         character.ImageMedium);
			Contains(19759, character.MediaIds);
			Contains(20755, character.MediaIds);
			Contains(21170, character.MediaIds);
			Contains(69883, character.MediaIds);
			IsNull(character.ModNotes);
		}
	}
}