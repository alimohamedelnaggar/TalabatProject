﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.specification.Products
{
    public class ProductSpecParams
    {
        public string? sort { get; set; }
        private string? search;
        public string? Search { get { return search; } set { search = value.ToLower(); } }
        public int? brandId { get; set; }
        public int? categoryId { get; set; }
        public int pageSize { get; set; } = 5;
        public int pageIndex { get; set; } = 1;
    }
}
