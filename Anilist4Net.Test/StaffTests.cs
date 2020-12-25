using System.Threading.Tasks;
using Anilist4Net.Enums;
using NUnit.Framework;
using static NUnit.Framework.Assert;

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
		}
	}
}