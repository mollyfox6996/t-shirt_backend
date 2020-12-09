using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.RequestFeatures
{
    public class TShirtParameters : RequestParameters
    {
        public string Category { get; set; }
        public string Gender { get; set; }
    }
}
