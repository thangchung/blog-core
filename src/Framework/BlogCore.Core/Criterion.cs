using System.Collections.Generic;

namespace BlogCore.Core
{
    public class Criterion : ValueObjectBase
    {
        public Criterion(int currentPage, int pageSize, string sortBy = "", string sortOrder = "")
        {
            if (currentPage <= 0)
                throw new ValidationException("CurrentPage could not be less than zero.");

            if (pageSize <= 0)
                throw new ValidationException("PageSize could not be less than zero.");

            CurrentPage = currentPage - 1;

            if (pageSize < 1 || pageSize > 20)
            {
                PageSize = 20;
            }
            else
            {
                PageSize = pageSize;
            }

            SortBy = sortBy;
            SortOrder = sortOrder;
        }

        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public string SortBy { get; private set; }
        public string SortOrder { get; private set; }

        public Criterion SetCurrentPage(int currentPage)
        {
            if (currentPage <= 0)
                throw new ValidationException("CurrentPage could not be less than zero.");

            CurrentPage = currentPage;
            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CurrentPage;
            yield return PageSize;
            yield return SortBy;
            yield return SortOrder;
        }
    }
}