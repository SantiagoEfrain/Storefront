using System;
using System.Collections.Generic;

namespace Storefront.DATA.EF.Models
{
    public partial class MobileSuit
    {
        public int ModelId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }
        public int UniverseId { get; set; }
        public decimal? Price { get; set; }
        public int ScaleId { get; set; }
        public int StockStatusId { get; set; }
        public int StockAmount { get; set; }
        public string? Msimage { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ModelScale Scale { get; set; } = null!;
        public virtual StockStatus StockStatus { get; set; } = null!;
        public virtual Universe Universe { get; set; } = null!;
    }
}
