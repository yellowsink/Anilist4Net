namespace Anilist4Net.Connections
{
	internal class MediaEdgeConnection
	{
		public MediaEdge[] Edges { get; set; }
	}

	internal class MediaEdge
	{
		public MediaNodePlaceholder Node         { get; set; }
		public MediaRelationType    RelationType { get; set; }
	}

	internal class MediaNodePlaceholder
	{
		public int Id { get; set; }
	}

	internal class CharacterConnection
	{
		public CharacterEdge[] Edges { get; set; }
	}

	internal class CharacterEdge
	{
		public CharacterNodePlaceholder Node        { get; set; }
		public VoiceActorPlaceholder[]  VoiceActors { get; set; }
	}

	internal class CharacterNodePlaceholder
	{
		public int Id { get; set; }
	}

	internal class VoiceActorPlaceholder
	{
		public int Id { get; set; }
	}

	internal class StaffConnection
	{
		public StaffEdge[] Edges { get; set; }
	}

	internal class StaffEdge
	{
		public StaffNodePlaceholder Node { get; set; }
		public string               Role { get; set; }
	}

	internal class StaffNodePlaceholder
	{
		public int Id { get; set; }
	}

	internal class StudioConnection
	{
		public StudioEdge[] Edges { get; set; }
	}

	internal class StudioEdge
	{
		public StudioNodePlaceholder Node   { get; set; }
		public bool                  IsMain { get; set; }
	}

	internal class StudioNodePlaceholder
	{
		public int Id { get; set; }
	}

	internal class AiringScheduleConnection
	{
		public AiringScheduleNodePlaceholder[] Nodes { get; set; }
	}

	internal class AiringScheduleNodePlaceholder
	{
		public int Id { get; set; }
	}

	internal class MediaTrendConnection
	{
		public MediaTrend[] Nodes { get; set; }
	}

	internal class ReviewConnection
	{
		public ReviewNodePlaceholder[] Nodes { get; set; }
	}

	internal class ReviewNodePlaceholder
	{
		public int Id { get; set; }
	}

	internal class RecommendationConnection
	{
		public RecommendationNode[] Nodes { get; set; }
	}

	internal class RecommendationNode
	{
		public int Id { get; set; }
	}

	internal class UserPlaceholder
	{
		public int Id { get; set; }
	}

	internal class MediaNodeConnection
	{
		public MediaNodePlaceholder[] Nodes { get; set; }
	}
}