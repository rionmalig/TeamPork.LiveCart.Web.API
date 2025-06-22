using Microsoft.OData.ModelBuilder;
using TeamPork.LiveCart.Model.LiveCart;
using TeamPork.LiveCart.Model.LiveCart.App;

namespace TeamPork.API.LiveCart.Configuration
{
    public static class ODataConfiguration
    {
        public static ODataConventionModelBuilder GetModelBuilder()
        {
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EnableLowerCamelCase();

            modelBuilder.EntitySet<Customer>(nameof(Customer));
            modelBuilder.EntitySet<Item>(nameof(Item));
            modelBuilder.EntitySet<InvoiceItem>(nameof(InvoiceItem));
            modelBuilder.EntitySet<Invoice>(nameof(Invoice));

            return modelBuilder;
        }
    }
}
