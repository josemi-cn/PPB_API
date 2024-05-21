using System;
using System.Collections.Generic;

namespace PPB_Storage_API.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Barcode { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string Name { get; set; } = null!;

    public float Price { get; set; }
}
