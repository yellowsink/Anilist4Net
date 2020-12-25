using System.Threading.Tasks;
using NUnit.Framework;

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

			Assert.AreEqual(2504,                                                              character.Id);
			Assert.AreEqual("Rinna",                                                           character.FirstName);
			Assert.AreEqual("Sawaki",                                                          character.LastName);
			Assert.AreEqual("Rinna Sawaki",                                                    character.FullName);
			Assert.AreEqual("沢城 凛奈",                                                           character.NativeName);
			Assert.AreEqual("https://s4.anilist.co/file/anilistcdn/character/large/2504.jpg",  character.ImageLarge);
			Assert.AreEqual("https://s4.anilist.co/file/anilistcdn/character/medium/2504.jpg", character.ImageMedium);
			Assert.AreEqual("https://anilist.co/character/2504",                               character.SiteUrl);
			Assert.Contains(2155,  character.MediaIds);
			Assert.Contains(40827, character.MediaIds);
			Assert.IsNull(character.ModNotes);
		}

		[Test]
		public async Task NagisaShiotaBySearchTest() // I'm having trouble hiding my biases here.
		{
			var client    = new Client();
			var character = await client.GetCharacterBySearch("nagisa shiota");

			Assert.AreEqual(65645,           character.Id);
			Assert.AreEqual("Nagisa",        character.FirstName);
			Assert.AreEqual("Shiota",        character.LastName);
			Assert.AreEqual("Nagisa Shiota", character.FullName);
			Assert.AreEqual("潮田渚",           character.NativeName);
			Assert.AreEqual("https://s4.anilist.co/file/anilistcdn/character/large/65645-o6Rjx8ud8gmM.png",
			                character.ImageLarge);
			Assert.AreEqual("https://s4.anilist.co/file/anilistcdn/character/medium/65645-o6Rjx8ud8gmM.png",
			                character.ImageMedium);
			Assert.Contains(19759, character.MediaIds);
			Assert.Contains(20755, character.MediaIds);
			Assert.Contains(21170, character.MediaIds);
			Assert.Contains(69883, character.MediaIds);
			Assert.IsNull(character.ModNotes);
		}
	}
}