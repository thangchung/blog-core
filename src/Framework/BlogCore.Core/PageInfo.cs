using System.Collections.Generic;

namespace BlogCore.Core
{
    public class PageInfo : ValueObjectBase
    {
        public PageInfo(int currentPage, int totalPage)
        {
            if (currentPage <= 0)
                throw new ValidationException("CurrentPage could not be less than zero.");

            if (totalPage <= 0)
                throw new ValidationException("TotalPage could not be less than zero.");

            CurrentPage = currentPage - 1;
            TotalPage = totalPage;
        }

        public int TotalPage { get; private set; }
        public int CurrentPage { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CurrentPage;
            yield return TotalPage;
        }
    }
}