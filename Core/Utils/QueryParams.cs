namespace Core.Utils
{
    public class QueryParams
	{
		public List<string>? SortFields { get; set; }

		public bool Ascending { get; set; } = true;

		private string? _search;

		private int _pageSize = 10;

		private int _pageIndex = 1;

		private const int MaxPageSize = 50;

		private const int MinPageIndex = 1;

		public int PageIndex
		{
			get => _pageIndex;

			set => _pageIndex = (value < MinPageIndex) ? MinPageIndex : value;
		}

		public int PageSize
		{
			get => _pageSize;

			set => _pageSize = (value > MaxPageSize) ? MaxPageSize : GetSizeValue(value);
		}
		public string? Search
		{
			get => _search;
			set => _search = value?.ToLower();
		}

		private int GetSizeValue(int val)
		{
			return (val < 0) ? 1 : val;
		}
	}
}
