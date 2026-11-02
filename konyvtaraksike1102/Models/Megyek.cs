using System;
using System.Collections.Generic;

namespace konyvtaraksike1102.Models;

public partial class Megyek
{
    public int Id { get; set; }

    public string MegyeNev { get; set; } = null!;

    public virtual ICollection<Telepulesek>? Telepuleseks { get; set; } = new List<Telepulesek>();
}
