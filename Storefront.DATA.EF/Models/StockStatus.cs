using System;
using System.Collections.Generic;

namespace Storefront.DATA.EF.Models
{
    public partial class StockStatus
    {
        public StockStatus()
        {
            MobileSuits = new HashSet<MobileSuit>();
        }

        public int StockStatusId { get; set; }
        public string StockStatusName { get; set; } = null!;

        public virtual ICollection<MobileSuit> MobileSuits { get; set; }
    }
}
