﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject3.Helpers
{
    public interface ITemplateHelper
    {
        Task<string> GetTemplateHtmlAsStringAsync<T>(
                             string viewName, T model);
    }
}
