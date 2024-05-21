using System;
using System.Collections.Generic;

namespace PPB_Storage_API.Models;

public partial class PermissionsRole
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int PermissionId { get; set; }
}
