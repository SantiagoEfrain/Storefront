using System;
using System.Collections.Generic;

namespace Storefront.DATA.EF.Models
{
    public partial class ModelScale
    {
        public ModelScale()
        {
            MobileSuits = new HashSet<MobileSuit>();
        }

        public int ScaleId { get; set; }
        public int ScaleSize { get; set; }

        public virtual ICollection<MobileSuit> MobileSuits { get; set; }
    }
}
