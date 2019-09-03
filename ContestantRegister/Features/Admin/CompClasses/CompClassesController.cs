﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContestantRegister.Controllers._Common;
using ContestantRegister.Controllers._Common.Commands;
using ContestantRegister.Controllers._Common.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContestantRegister.Data;
using ContestantRegister.Models;
using ContestantRegister.Utils;
using Microsoft.AspNetCore.Authorization;
using ContestantRegister.Controllers.CompClasses.Queries;
using ContestantRegister.Controllers.CompClasses;
using ContestantRegister.Cqrs.Features._Common.Commands;
using ContestantRegister.Domain;
using ContestantRegister.Infrastructure.Cqrs;

namespace ContestantRegister.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class CompClassesController : CrudController<CompClass, CompClassListItemViewModel, CompClass,
        GetMappedEntitiesQuery<CompClass, CompClassListItemViewModel>, GetEntityByIdQuery<CompClass>, GetEntityByIdForDeleteQuery<CompClass>,
        CreateMappedEntityCommand<CompClass, CompClass>, EditMappedEntityCommand<CompClass, CompClass>, DeleteEntityByIdCommand<CompClass>>
    {
        public CompClassesController(IHandlerDispatcher dispatcher, IMapper mapper) : base(dispatcher, mapper)
        {            
        }

        protected override async Task FillViewDataDetailFormAsync(CompClass item = null)
        {
            var areas = await HandlerDispatcher.ExecuteQueryAsync(new GetAreasForCompClassQuery());
            ViewData["Area"] = new SelectList(areas, "Id", "Name", item?.AreaId);
        }           
    }
}
