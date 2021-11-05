﻿using Core.Shared;
using System;

namespace Areawa.Models
{
    public class WebsiteArchiveQuery
    {
        public Guid? PublicId { get; set; }
        public string ShortId { get; set; }

        public SortBy SortBy { get; set; }
        public bool IsSortDescending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
