namespace Sporacid.Simplets.Webapp.Services.Services.Public.Impl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Sporacid.Simplets.Webapp.Core.Security.Authorization;
    using Sporacid.Simplets.Webapp.Services.Database.Dto.Description;
    using Sporacid.Simplets.Webapp.Tools.Collections;
    using Sporacid.Simplets.Webapp.Tools.Reflection;
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath)]
    public class DescriptionController : BaseService, IDescriptionService
    {
        /// <summary>
        /// Describes the api entities. This can be used to discover what entities are used in the api.
        /// </summary>
        /// <returns>An enumeration of all available api entities.</returns>
        [HttpGet, Route("describe-entities")]
        [CacheOutput(ServerTimeSpan = (Int32)CacheDuration.Maximum, ClientTimeSpan = (Int32)CacheDuration.Maximum)]
        public IEnumerable<ApiEntityDescriptionDto> DescribeApiEntities()
        {
            // Do not mind the ugliness of this method. The goal is not readability, nor performance.
            // The goal is to get the relevent informations of the api for a end user. Keep always in mind that the result
            // is cached for a very long time, since it will not change at runtime.

            var dtoNamespaces = new[]
            {
                "Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs",
                "Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo",
                "Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace",
                "Sporacid.Simplets.Webapp.Services.Database.Dto.Description"
            };
            
            // Get all data transfer object types from above assemblies.
            var assembly = Assembly.GetExecutingAssembly();
            var dtoTypes = assembly.GetTypes().Where(type => type.IsClass && type.Namespace != null && dtoNamespaces.Contains(type.Namespace));

            // Describe all dto types.
            var entityDescriptionDtos = new List<ApiEntityDescriptionDto>();
            foreach (var dtoType in dtoTypes)
            {
                // Describe all properties of the type.
                var apiEntityPropertyDescriptionDtos = new List<ApiEntityPropertyDescriptionDto>();
                foreach (var dtoTypeProperty in dtoType.GetProperties())
                {
                    var apiEntityPropertyDescriptionDto = new ApiEntityPropertyDescriptionDto
                    {
                        Name = dtoTypeProperty.Name,
                        Type = dtoTypeProperty.PropertyType.Name
                    };

                    // Describe main component model constraints.
                    // I don't think we need to describe all constraints, it's just to give a general idea.
                    var constraints = new List<String>();
                    foreach (var dtoTypeFieldAttr in dtoTypeProperty.GetCustomAttributes())
                    {
                        if (dtoTypeFieldAttr is RequiredAttribute)
                            constraints.Add("Required");

                        var strLen = dtoTypeFieldAttr as StringLengthAttribute;
                        if (strLen != null)
                        {
                            constraints.Add(String.Format("MinLength{0})", strLen.MinimumLength));
                            constraints.Add(String.Format("MaxLength({0})", strLen.MaximumLength));
                        }

                        var range = dtoTypeFieldAttr as RangeAttribute;
                        if (range != null)
                        {
                            constraints.Add(String.Format("MinValue({0})", range.Minimum));
                            constraints.Add(String.Format("MaxValue({0})", range.Maximum));
                        }

                        var regex = dtoTypeFieldAttr as RegularExpressionAttribute;
                        if (regex != null)
                            constraints.Add(String.Format("Regex(\"{0}\")", regex.Pattern));
                    }

                    apiEntityPropertyDescriptionDto.Constraints = constraints.OrderBy(c => c);
                    apiEntityPropertyDescriptionDtos.Add(apiEntityPropertyDescriptionDto);
                }

                entityDescriptionDtos.Add(new ApiEntityDescriptionDto
                {
                    Name = dtoType.Name,
                    Properties = apiEntityPropertyDescriptionDtos.OrderBy(p => p.Name)
                });
            }

            return entityDescriptionDtos.OrderBy(e => e.Name);
        }

        /// <summary>
        /// Describes the api methods. This can be used to discover what operations are available.
        /// </summary>
        /// <returns>An enumeration of all available api methods.</returns>
        [HttpGet, Route("describe-api")]
        [CacheOutput(ServerTimeSpan = (Int32)CacheDuration.Maximum, ClientTimeSpan = (Int32)CacheDuration.Maximum)]
        public IEnumerable<ApiModuleDescriptionDto> DescribeApiMethods()
        {
            // Do not mind the ugliness of this method. The goal is not readability, nor performance.
            // The goal is to get the relevent informations of the api for a end user. Keep always in mind that the result
            // is cached for a very long time, since it will not change at runtime.

            // Describe all methods by logical module.
            var moduleDescriptionDtos = new List<ApiModuleDescriptionDto>();
            GlobalConfiguration.Configuration.Services.GetApiExplorer().ApiDescriptions.GroupBy(d =>
            {
                // Group all method descriptions by module.
                var moduleAttr = d.ActionDescriptor.ControllerDescriptor.ControllerType.GetAllCustomAttributes<ModuleAttribute>().FirstOrDefault();
                return moduleAttr != null ? moduleAttr.Name : null;
            }).ForEach(module =>
            {
                var apiMethodDescriptionDtos = new List<ApiMethodDescriptionDto>();
                module.ToList().ForEach(apiDescription =>
                {
                    var apiDescriptionDto = new ApiMethodDescriptionDto
                    {
                        Name = apiDescription.ActionDescriptor.ActionName,
                        Documentation = apiDescription.Documentation,
                        HttpMethod = apiDescription.HttpMethod.Method,
                        Route = apiDescription.Route.RouteTemplate,
                        Parameters = apiDescription.ParameterDescriptions.Select(parameterDescription => new ApiMethodParameterDescriptionDto
                        {
                            Name = parameterDescription.Name,
                            Documentation = parameterDescription.Documentation,
                            Type = parameterDescription.ParameterDescriptor != null ? parameterDescription.ParameterDescriptor.ParameterType.Name : null,
                            IsOptional = parameterDescription.ParameterDescriptor != null && parameterDescription.ParameterDescriptor.IsOptional
                        }).OrderBy(p => p.Name),
                        Response = new ApiResponseDescriptionDto
                        {
                            Type = apiDescription.ResponseDescription.DeclaredType != null ? apiDescription.ResponseDescription.DeclaredType.Name : "void",
                            Documentation = apiDescription.ResponseDescription.Documentation
                        }
                    };

                    var actionDescriptor = apiDescription.ActionDescriptor as ReflectedHttpActionDescriptor;
                    if (actionDescriptor != null)
                    {
                        var serviceType = apiDescription.ActionDescriptor.ControllerDescriptor.ControllerType;
                        var method = serviceType.GetMethod(actionDescriptor.MethodInfo.Name);
                        if (method != null)
                        {
                            var requiredClaimsAttr = method.GetAllCustomAttributes<RequiredClaimsAttribute>().FirstOrDefault();
                            apiDescriptionDto.RequiredClaims = requiredClaimsAttr != null ? requiredClaimsAttr.RequiredClaims : Claims.None;
                        }
                    }

                    apiMethodDescriptionDtos.Add(apiDescriptionDto);
                });

                moduleDescriptionDtos.Add(new ApiModuleDescriptionDto
                {
                    Name = module.Key,
                    Methods = apiMethodDescriptionDtos.OrderBy(d => d.Route + d.HttpMethod)
                });
            });

            return moduleDescriptionDtos.OrderBy(m => m.Name);
        }
    }
}