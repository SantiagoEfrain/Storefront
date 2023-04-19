using System;
using System.Collections.Generic;

namespace Storefront.DATA.EF.Models
{
    public partial class Category
    {
        public Category()
        {
            MobileSuits = new HashSet<MobileSuit>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDesc { get; set; }

        public virtual ICollection<MobileSuit> MobileSuits { get; set; }
    }
}
