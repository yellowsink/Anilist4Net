using Anilist4Net.Enums;

namespace Anilist4Net.Connections
{
	public class MediaEdgeConnection
	{
		public MediaEdge[] Edges { get; set; }
	}

	public class MediaEdge
	{
		public MediaNodePlaceholder Node         { get; set; }
		public MediaRelationType    RelationType { get; set; }
    }

	public class MediaNodePlaceholder
	{
		public int Id           { get; set; }
		public MediaTitle Title { get; set; }
		public MediaTypes? Type { get; set; }
	}

	public class CharacterConnection
	{
		public CharacterEdge[] Edges { get; set; }
	}

	public class CharacterEdge
	{
		public CharacterNodePlaceholder Node        { get; set; }
		public VoiceActorPlaceholder[]  VoiceActors { get; set; }
        public CharacterRole? Role                  { get; set; }
        public CharacterRole? CharacterRole         { get; set; }
	}

	public class CharacterNodePlaceholder
	{
		public int Id      { get; set; }
		public MultiLanguageName Name { get; set; }
		public MediaNodeConnection Media { get; set; }
	}

	public class VoiceActorPlaceholder
	{
		public int Id { get; set; }
	}

	public class StaffConnection
	{
		public StaffEdge[] Edges { get; set; }
	}

	public class StaffEdge
	{
		public StaffNodePlaceholder Node { get; set; }
		public string               Role { get; set; }
	}

	public class StaffNodePlaceholder
	{
		public int Id { get; set; }
	}

	public class StudioConnection
	{
		public StudioEdge[] Edges { get; set; }
	}

	public class StudioEdge
	{
		public StudioNodePlaceholder Node   { get; set; }
		public bool                  IsMain { get; set; }
	}

	public class StudioNodePlaceholder
	{
		public int Id { get; set; }
	}

	public class AiringScheduleConnection
	{
		public AiringSchedule[] Nodes { get; set; }
	}

	public class AiringScheduleNodePlaceholder
	{
		public int Id { get; set; }
	}

	public class MediaTrendConnection
	{
		public MediaTrend[] Nodes { get; set; }
	}

	public class ReviewConnection
	{
		public ReviewNodePlaceholder[] Nodes { get; set; }
	}

	public class ReviewNodePlaceholder
	{
		public int Id { get; set; }
	}

	public class RecommendationConnection
	{
		public RecommendationNode[] Nodes { get; set; }
	}

	public class RecommendationNode
	{
		public int Id { get; set; }
	}

	public class UserPlaceholder
	{
		public int Id { get; set; }
	}

	public class MediaNodeConnection
	{
		public MediaNodePlaceholder[] Nodes { get; set; }
		public CharacterEdge[] Edges { get; set; }
    }
}