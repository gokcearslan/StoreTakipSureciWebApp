using System;
using System.Collections.Generic;

namespace twotableversion.Data;

public partial class Takvim
{
    public int Primarykey { get; set; }

    public int AyId { get; set; }

    public string? AyAdı { get; set; }

    public string Uygulama { get; set; } = null!;
}
