using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleScanWebApi.Entities;

public class Workflow : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
}