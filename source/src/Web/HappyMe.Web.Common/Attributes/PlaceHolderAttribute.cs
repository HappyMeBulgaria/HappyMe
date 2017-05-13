namespace HappyMe.Web.Common.Attributes
{
    using System;

    /*TODO: Change placeholder attributes with Prompt prop of the Display attribute.
     * We can get it with: ModelMetadata.FromLambdaExpression(m => m.Email, ViewData).Watermark */
    public class PlaceHolderAttribute : Attribute
    {
        private readonly string placeholder;

        public PlaceHolderAttribute(string placeholder)
        {
            this.placeholder = placeholder;
        }

        // void IMetadataAware.OnMetadataCreated(ModelMetadata metadata)
        // {
        //    this.OnMetadataCreated(metadata);
        // }

        // public void OnMetadataCreated(ModelMetadata metadata)
        // {
        //    metadata.AdditionalValues["placeholder"] = this.placeholder;
        // }
    }
}
