using System;
using System.Collections.Generic;

namespace SIGESHA.Models.Entities.DBSIGESHA;

public partial class Sha4
{
    public double? Id { get; set; }

    public string? Codigo { get; set; }

    public int? Sha3Id { get; set; }

    public int? Sha2Id { get; set; }

    public int? Sha1Id { get; set; }

    public string? F6 { get; set; }

    public string? F7 { get; set; }

    public int IdPk { get; set; }
}
