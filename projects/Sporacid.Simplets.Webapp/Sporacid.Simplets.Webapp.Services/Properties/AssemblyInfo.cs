// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
using System.Reflection;
using System.Runtime.InteropServices;
using log4net.Config;
using Sporacid.Simplets.Webapp.Services;

[assembly: AssemblyTitle("Sporacid.Simplets.Webapp.Services")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("Sporacid.Simplets.Webapp.Services")]
[assembly: AssemblyCopyright("Copyright © Microsoft 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM

[assembly: Guid("00bdbb73-7f00-460a-9f84-604d61a82cf4")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: XmlConfigurator(ConfigFile = @".\Resources\log4net.xml", Watch = true)]

// Postsharp aspects.
// [assembly: Trace(AttributeTargetAssemblies = "Sporacid.Simplets.Webapp.Core|Sporacid.Simplets.Webapp.Services",
//    AttributeTargetTypeAttributes = MulticastAttributes.Public,
//    AttributeTargetMemberAttributes = MulticastAttributes.Public,
//    AttributeTargetElements = MulticastTargets.Method)]

// Bootstrap activator methods.
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start", Order = 0)]
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AutoMapperConfig), "InitializeAutoMapper", Order = 1)]
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SecurityConfig), "BootstrapSecurityContext", Order = 2)]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop", Order = 0)]