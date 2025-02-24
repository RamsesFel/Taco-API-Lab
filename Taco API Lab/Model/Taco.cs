﻿using System;
using System.Collections.Generic;

namespace Taco_API_Lab.Model;

public partial class Taco
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public float? Cost { get; set; }

    public bool? SoftShell { get; set; }

    public bool? Chips { get; set; }
}
