﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ASBDDS.Shared.Models.Database.DataDb
{
    public class ApplicationRole : IdentityRole<Guid> { }
}
