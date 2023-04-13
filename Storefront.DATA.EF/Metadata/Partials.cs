using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storefront.DATA.EF//.Metadata
{

    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category { }
    
    [ModelMetadataType(typeof(MobileSuitMetadata))]
    public partial class MobileSuit { }
    
    [ModelMetadataType(typeof(ModelScaleMetadata))]
    public partial class ModelScale { }
    
    [ModelMetadataType(typeof(StockStatusMetadata))]
    public partial class StockStatus { }
    
    [ModelMetadataType(typeof(UniverseMetadata))]
    public partial class Universe { }





}
