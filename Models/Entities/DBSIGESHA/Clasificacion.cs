using System;
using System.Collections.Generic;

namespace SIGESHA.Models.Entities.DBSIGESHA;

public partial class Clasificacion
{
    public double CodigoShaId { get; set; }

    public string? CodigoSha { get; set; }

    public string? DescripciónSha { get; set; }

    public string? InstalacionesMinsaCssPanamá { get; set; }

    public string? Observaciones { get; set; }

    public string? F6 { get; set; }

    public int Id { get; set; }
}
