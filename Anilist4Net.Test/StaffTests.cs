using System.Threading.Tasks;
using Anilist4Net.Enums;
using NUnit.Framework;

namespace Anilist4Net.Test
{
	[TestFixture]
	public class StaffTests
	{
		[Test]
		public async Task YuseiMatsuiTest() // Author of Assassination Classroom manga
		{
			var client = new Client();
			var staff  = await client.GetStaffById(98152);

			Assert.AreEqual(98152,                            staff.Id);
			Assert.AreEqual("Yuusei",                         staff.Name.First);
			Assert.AreEqual("Matsui",                         staff.Name.Last);
			Assert.AreEqual("Yuusei Matsui",                  staff.Name.Full);
			Assert.AreEqual("松井優征",                           staff.Name.Native);
			Assert.AreEqual(StaffLanguages.JAPANESE,          staff.Language);
			Assert.AreEqual("https://anilist.co/staff/98152", staff.SiteUrl);
			Assert.Contains(2985,  staff.StaffMediaIds); // just the first 4 that appeared on the query
			Assert.Contains(19759, staff.StaffMediaIds);
			Assert.Contains(20755, staff.StaffMediaIds);
			Assert.Contains(21170, staff.StaffMediaIds);
			Assert.IsEmpty(staff.CharacterIds);
			Assert.IsEmpty(staff.CharacterMediaIds);
			Assert.IsNull(staff.ModNotes);
		}
	}
}