namespace Te4Fest.Web.Api.Areas.HelpPage.ModelDescriptions
{
    using System;
    using System.Reflection;

    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}