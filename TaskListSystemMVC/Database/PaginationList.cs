using Microsoft.EntityFrameworkCore;

namespace TaskListSystemMVC.Database
{
    public class PaginationList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage {  get; set; }
        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < TotalPage;

        public PaginationList(List<T> dataList, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(dataList);
        }

        public static PaginationList<T> Create(List<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginationList<T>(items, count, pageIndex, pageSize);
        }

        public static async Task<PaginationList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginationList<T>(items, count, pageIndex, pageSize);
        }
    }
}
