﻿using AmplaWeb.Data;
using AmplaWeb.Data.Controllers;
using AmplaWeb.Sample.Models;

namespace AmplaWeb.Sample.Controllers
{
    public class ShiftLogController : RepositoryController<ShiftLogModel>
    {
        public ShiftLogController(IRepository<ShiftLogModel> repository) : base(repository)
        {
        }
    }
}