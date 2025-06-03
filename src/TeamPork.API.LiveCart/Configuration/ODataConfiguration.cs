using Microsoft.OData.ModelBuilder;

namespace TeamPork.API.LiveCart.Configuration
{
    public static class ODataConfiguration
    {
        public static ODataConventionModelBuilder GetModelBuilder()
        {
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EnableLowerCamelCase();



            return modelBuilder;
        }
    }
}
