﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Domain.Contests
{
    public class ContestCopy
    {
       [Required]
       public Guid Id { get; set; }

       [Required]
       public string Name { get; set; }
    }
}