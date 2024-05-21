using System;
using System.Collections.Generic;

namespace PPB_Storage_API.Models;

public partial class Command
{
    public int Id { get; set; }

    public int Number { get; set; }

    public DateTime Date { get; set; }

    public bool Ready { get; set; }

    public bool Delivered { get; set; }
}
