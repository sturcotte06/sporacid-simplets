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
    using WebApi.OutputCache.V2;

    /// <authors>Simon Turcotte-Langevin, Patrick Lavallée, Jean Bernier-Vibert</authors>
    /// <version>1.9.0</version>
    [RoutePrefix(BasePath)]
    public class DescriptionService : BaseService, IDescriptionService
    {
        /// <summary>
        /// Describes the api entities. This can be used to discover what entities are used in the api.
        /// </summary>
        /// <returns>An enumeration of all available api entities.</returns>
        [HttpGet, Route("describe-entities")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Maximum)]
        public IEnumerable<ApiEntityDescriptionDto> DescribeApiEntities()
        {
            // Do not mind the ugliness of this method. The goal is not readability, nor performance.
            // The goal is to get the relevent informations of the api for a end user. Keep always in mind that the result
            // is cached for a very long time, since it will not change at runtime.

            var dtoNamespaces = new[]
            {
                "Sporacid.Simplets.Webapp.Services.Database.Dto.Clubs",
                "Sporacid.Simplets.Webapp.Services.Database.Dto.Dbo",
                "Sporacid.Simplets.Webapp.Services.Database.Dto.Userspace"
            };


            // Get all data transfer object types from above assemblies.
            var assembly = Assembly.GetExecutingAssembly();
            var dtoTypes = from type in assembly.GetTypes()
                where type.IsClass
                      && type.Namespace != null
                      && dtoNamespaces.Contains(type.Namespace)
                select type;

            // Describe all dto types.
            foreach (var dtoType in dtoTypes)
            {
                // Describe all properties of the type.
                var apiEntityPropertyDescriptionDtos = new List<ApiEntityPropertyDescriptionDto>();
                foreach (var dtoTypeProperty in dtoType.GetProperties())
                {
                    var apiEntityPropertyDescriptionDto = new ApiEntityPropertyDescriptionDto
                    {
                        PropertyName = dtoTypeProperty.Name,
                        PropertyType = dtoTypeProperty.PropertyType.Name
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
                            constraints.Add(String.Format("MinLength({0})", strLen.MinimumLength));
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
                            constraints.Add(String.Format("Regex({0})", regex.Pattern));
                    }

                    apiEntityPropertyDescriptionDto.Constraints = constraints;
                    apiEntityPropertyDescriptionDtos.Add(apiEntityPropertyDescriptionDto);
                }

                yield return new ApiEntityDescriptionDto
                {
                    EntityName = dtoType.Name,
                    Properties = apiEntityPropertyDescriptionDtos
                };
            }
        }

        /// <summary>
        /// Describes the api methods. This can be used to discover what operations are available.
        /// </summary>
        /// <returns>An enumeration of all available api methods.</returns>
        [HttpGet, Route("describe-methods")]
        [CacheOutput(ServerTimeSpan = (Int32) CacheDuration.Maximum)]
        public IEnumerable<ApiMethodDescriptionDto> DescribeApiMethods()
        {
            // Do not mind the ugliness of this method. The goal is not readability, nor performance.
            // The goal is to get the relevent informations of the api for a end user. Keep always in mind that the result
            // is cached for a very long time, since it will not change at runtime.

            // Describe all service types.
            var apiDescriptions = GlobalConfiguration.Configuration.Services.GetApiExplorer().ApiDescriptions;
            foreach (var apiDescription in apiDescriptions)
            {
                var apiDescriptionDto = new ApiMethodDescriptionDto
                {
                    Documentation = apiDescription.Documentation,
                    HttpMethod = apiDescription.HttpMethod.Method,
                    Route = apiDescription.Route.RouteTemplate,
                    ParameterDescriptions = apiDescription.ParameterDescriptions.Select(parameterDescription => new ApiMethodParameterDescriptionDto
                    {
                        Name = parameterDescription.Name,
                        Documentation = parameterDescription.Documentation,
                        ParameterType =
                            parameterDescription.ParameterDescriptor != null ? parameterDescription.ParameterDescriptor.ParameterType.Name : null,
                        IsOptional =
                            parameterDescription.ParameterDescriptor != null && parameterDescription.ParameterDescriptor.IsOptional
                    }),
                    ResponseDescription = new ApiResponseDescriptionDto
                    {
                        Documentation = apiDescription.ResponseDescription.Documentation,
                        ResponseType = apiDescription.ResponseDescription.DeclaredType != null ? apiDescription.ResponseDescription.DeclaredType.Name : "void"
                    }
                };

                var actionDescriptor = apiDescription.ActionDescriptor as ReflectedHttpActionDescriptor;
                if (actionDescriptor != null)
                {
                    // Hack it up. Technically, when it was written, service classes only had one interface.
                    // It's not pretty (at all), but attribute inheritance on methods does not seem to be very well defined.
                    var serviceType = apiDescription.ActionDescriptor.ControllerDescriptor.ControllerType.GetInterfaces().FirstOrDefault(i => i.Name.Contains("Service"));
                    if (serviceType != null)
                    {
                        var method = serviceType.GetMethod(actionDescriptor.MethodInfo.Name);
                        if (method != null)
                        {
                            var requiredClaimsAttr = method.GetCustomAttributes<RequiredClaimsAttribute>().FirstOrDefault();
                            apiDescriptionDto.RequiredClaims = requiredClaimsAttr != null ? requiredClaimsAttr.RequiredClaims : Claims.None;
                        }
                    }
                }

                yield return apiDescriptionDto;
            }
        }
    }
}