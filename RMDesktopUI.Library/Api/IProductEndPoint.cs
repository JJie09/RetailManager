﻿using System.Collections.Generic;
using System.Threading.Tasks;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.Library.Api
{
    public interface IProductEndPoint
    {
        Task<List<ProductModel>> GetAll();
    }
}