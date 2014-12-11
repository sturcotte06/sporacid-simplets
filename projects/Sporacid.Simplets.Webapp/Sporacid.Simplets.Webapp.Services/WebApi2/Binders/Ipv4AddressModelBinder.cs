namespace Sporacid.Simplets.Webapp.Services.WebApi2.Binders
{
    using System.Web.Http.Controllers;
    using System.Web.Http.ModelBinding;
    using Sporacid.Simplets.Webapp.Tools.Strings;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    public class Ipv4AddressModelBinder : IModelBinder
    {
        /// <summary>
        /// Binds the model to a value by using the specified controller context and binding context.
        /// </summary>
        /// <returns>
        /// true if model binding is successful; otherwise, false.
        /// </returns>
        /// <param name="actionContext">The action context.</param>
        /// <param name="bindingContext">The binding context.</param>
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var ipv4Address = actionContext.Request.Headers.Host;
            if (ipv4Address.IsNullOrEmpty())
            {
                return false;
            }

            bindingContext.Model = ipv4Address;
            return true;
        }
    }
}