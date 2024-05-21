using System;
using System.Collections.Generic;

namespace PPB_Storage_API.Models;

public partial class ProductsCommand
{
    public int Id { get; set; }

    public int CommandId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
}
