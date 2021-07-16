using System.Linq;
using System.Threading.Tasks;
using Anilist4Net.Enums;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace Anilist4Net.Test
{
	[TestFixture]
	public class StaffTests
	{
		[TestCase(TestName       = "ID Test")]
		[TestCase(true, TestName = "Search Test")]
		public async Task YuseiMatsuiTest(bool search = false) // Author of Assassination Classroom manga
		{
			var client = new Client();
			var staff  = search ? await client.GetStaffBySearch("yuusei matsui") : await client.GetStaffById(98152);

			AreEqual(98152,                            staff.Id);
			AreEqual("Yuusei",                         staff.Name.First);
			AreEqual("Matsui",                         staff.Name.Last);
			AreEqual("Yuusei Matsui",                  staff.Name.Full);
			AreEqual("松井優征",                           staff.Name.Native);
			AreEqual(StaffLanguages.JAPANESE,          staff.Language);
			AreEqual("https://anilist.co/staff/98152", staff.SiteUrl);
			Contains(2985,  staff.StaffMediaIds); // just the first 4 that appeared on the query
			Contains(19759, staff.StaffMediaIds);
			Contains(20755, staff.StaffMediaIds);
			Contains(21170, staff.StaffMediaIds);
			IsEmpty(staff.CharacterIds);
			IsEmpty(staff.CharacterMediaIds);
			IsNull(staff.ModNotes);
			AreEqual("https://s4.anilist.co/file/anilistcdn/staff/large/3152.jpg", staff.ImageLarge);
            AreEqual("https://s4.anilist.co/file/anilistcdn/staff/medium/3152.jpg", staff.ImageMedium);
		}

        [Test]
        public async Task StaffCharacters()
        {
			// arrange
            var client = new Client();

			// act
			var staff = await client.GetStaffById(101686);

			// assert
			GreaterOrEqual(staff.Characters.Edges.Length, 164);
            var edge = staff.Characters.Edges.First(x => x.Node.Id == 38904);
            AreEqual(MediaTypes.ANIME, edge.Node.Media.Nodes.First(x => x.Id == 9776).Type);
        }		
	}
}