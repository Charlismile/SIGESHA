using System;
using System.Collections.Generic;

namespace SIGESHA.Models.Entities.DBSIGESHA;

public partial class Sha5
{
    public double? Id { get; set; }

    public string? Codigo { get; set; }

    public int? Sha4Id { get; set; }

    public int? Sha3Id { get; set; }

    public int? Sha2Id { get; set; }

    public int? Sha1Id { get; set; }

    public int IdPk { get; set; }
}
