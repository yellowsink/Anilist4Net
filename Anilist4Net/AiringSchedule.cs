namespace Anilist4Net
{
	public class AiringSchedule
	{
		/// <summary>
		///     The ID of the airing schedule
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     When the episode airs
		/// </summary>
		public int? AiringAt { get; set; }

		/// <summary>
		///     How long until AiringAt
		/// </summary>
		public int? TimeUntilAiring { get; set; }

		/// <summary>
		///     What episode is airing
		/// </summary>
		public int? Episode { get; set; }

		/// <summary>
		///     The ID of the media airing
		/// </summary>
		public int? MediaId { get; set; }
	}

	public class AiringScheduleResponse
	{
		public AiringSchedule AiringSchedule { get; set; }
	}
}