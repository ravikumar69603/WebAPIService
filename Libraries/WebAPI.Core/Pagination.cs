namespace WebAPI.Core
{
    public class Pagination
    {
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int IndexOne { get; set; }
        public int IndexTwo { get; set; }
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;
    }
}
