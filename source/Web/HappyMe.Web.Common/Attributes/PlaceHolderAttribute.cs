namespace HappyMe.Web.Common.Attributes
{
    using System;
    using System.Web.Mvc;

    public class PlaceHolderAttribute : Attribute, IMetadataAware
    {
        private readonly string placeholder;

        public PlaceHolderAttribute(string placeholder)
        {
            this.placeholder = placeholder;
        }

        void IMetadataAware.OnMetadataCreated(ModelMetadata metadata)
        {
            this.OnMetadataCreated(metadata);
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["placeholder"] = this.placeholder;
        }
    }
}
