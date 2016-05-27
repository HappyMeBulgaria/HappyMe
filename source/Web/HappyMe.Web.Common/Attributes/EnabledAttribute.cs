namespace HappyMe.Web.Common.Attributes
{
    using System;
    using System.Web.Mvc;

    public class EnabledAttribute : Attribute, IMetadataAware
    {
        private readonly bool enabled;

        public EnabledAttribute(bool enabled)
        {
            this.enabled = enabled;
        }

        void IMetadataAware.OnMetadataCreated(ModelMetadata metadata)
        {
            this.OnMetadataCreated(metadata);
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["enabled"] = this.enabled;
        }
    }
}
