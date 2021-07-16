using System.Linq;
using System.Threading.Tasks;
using Anilist4Net.Enums;
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
        public async Task RolesInMedia()
        {
			// arrange
            var client = new Client();

			// act
            var character = await client.GetCharacterById(215834);

			// assert
            AreEqual(CharacterRole.MAIN, character.Media.Edges.First(x => x.Node.Id == 135381).CharacterRole);
            AreEqual(CharacterRole.SUPPORTING, character.Media.Edges.First(x => x.Node.Id == 129814).CharacterRole);
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
			// why does all this crap change
			/*AreEqual("https://s4.anilist.co/file/anilistcdn/character/large/65645-o6Rjx8ud8gmM.png",
			         character.ImageLarge);
1			AreEqual("https://s4.anilist.co/file/anilistcdn/character/medium/65645-o6Rjx8ud8gmM.png",
			         character.ImageMedium);*/
			Contains(19759, character.MediaIds);
			Contains(20755, character.MediaIds);
			Contains(21170, character.MediaIds);
			Contains(69883, character.MediaIds);
			IsNull(character.ModNotes);
		}

		// Ensure we retrieve all the media pages and not just the first 25
		[Test]
		public async Task NarutoMedia()
        {
			// arrange
			var client = new Client();

			// act
			var character = await client.GetCharacterById(17);

			// assert
			GreaterOrEqual(character.Media.Edges.Length, 52);
			GreaterOrEqual(character.Media.Nodes.Length, 52);
		}
	}
}
