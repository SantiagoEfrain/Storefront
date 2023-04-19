using System;
using System.Collections.Generic;

namespace Storefront.DATA.EF.Models
{
    public partial class Universe
    {
        public Universe()
        {
            MobileSuits = new HashSet<MobileSuit>();
        }

        public int UniverseId { get; set; }
        public string UniverseName { get; set; } = null!;
        public string? UniverseDesc { get; set; }

        public virtual ICollection<MobileSuit> MobileSuits { get; set; }
    }
}
