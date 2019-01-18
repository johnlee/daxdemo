﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace daxdemo.Data
{
    public interface IDbHandler
    {
        Task<List<Widget>> Read();
        Task Write(Widget widget);
    }
}