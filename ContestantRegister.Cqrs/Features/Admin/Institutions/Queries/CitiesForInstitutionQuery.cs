﻿using System.Collections.Generic;
using ContestantRegister.Framework.Cqrs;
using ContestantRegister.Models;

namespace ContestantRegister.Cqrs.Features.Admin.Institutions.Queries
{
    public class CitiesForInstitutionQuery : IQuery<List<City>>
    {
    }
}
