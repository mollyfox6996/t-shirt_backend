using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.RequestFeatures
{
    public class RequestParameters
    {
        private const int MaxPageSize = 48;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 12;

        public int PageSize
        {
            get => _pageSize;

            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
    }
}
