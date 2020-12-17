using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.RequestFeatures
{
    public class RequestParameters
    {
        const int maxPageSize = 48;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 12;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string OrderBy { get; set; }
    }
}
