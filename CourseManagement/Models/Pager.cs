using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagement.Models
{
    public class Pager
    {
        public int TotalItem { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public Pager()
        {

        }

        public Pager(int totalItem, int page, int pageSize = 5)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItem / (decimal)pageSize);

            int currentPage = page;

            int startPage = currentPage - 2;

            int endPage = currentPage + 2;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 5)
                {
                    startPage = endPage - 4;
                }
            }

            this.TotalItem = totalItem;
            this.PageSize = pageSize;
            this.StartPage = startPage;
            this.EndPage = endPage;
            this.CurrentPage = currentPage;
            this.TotalPages = totalPages;
        }

        public static List<T> GetDataPerPage<T>(List<T> modelList, int pg, ref Pager pager) where T : BaseViewModel
        {
            const int pageSize = 5;
            if (pg < 1)
            {
                pg = 1;
            }
            int techCount = modelList.Count;
            pager = new Pager(techCount, pg, pageSize);
            int techSkip = (pg - 1) * pageSize;

            var data = modelList.Skip(techSkip).Take(pager.PageSize).ToList();
            return data;
        }
    }
}
