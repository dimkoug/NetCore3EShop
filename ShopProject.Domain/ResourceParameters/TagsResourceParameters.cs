using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain.ResourceParameters
{
    public class TagsResourceParameters
    {
        const int maxPageSize = 20;
            public int PageNumber { get; set; } = 1;
        private int _PageSize = 10;    
        public int PageSize {
            get => _PageSize;
            set => _PageSize = (value > maxPageSize) ? maxPageSize : value; 
        }
        
    }
}
