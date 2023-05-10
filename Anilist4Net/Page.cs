namespace Anilist4Net
{
    public class Page
    {
        /// <summary>
        /// The media for the requested page
        /// </summary>
        public Media[] Media { get; set; }

        /// <summary>
        /// Information about the current page
        /// </summary>
        public PageInfo PageInfo { get; set; }
    }

    public class PageInfo
    {
        /// <summary>
        /// Is there a next page
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// The number of the current page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// The total number of entries
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// The number of entries per page
        /// </summary>
        public int PerPage { get; set; }
    }

    public class PageResponse
    {
        /// <summary>
        /// The page that contains the data
        /// </summary>
        public Page Page { get; set; }
    }
}