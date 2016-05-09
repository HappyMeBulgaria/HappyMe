namespace Te4Fest.Web.Api.Areas.HelpPage.ModelDescriptions
{
    using System.Collections.ObjectModel;

    public class ComplexTypeModelDescription : ModelDescription
    {
        public ComplexTypeModelDescription()
        {
            this.Properties = new Collection<ParameterDescription>();
        }

        public Collection<ParameterDescription> Properties { get; private set; }
    }
}