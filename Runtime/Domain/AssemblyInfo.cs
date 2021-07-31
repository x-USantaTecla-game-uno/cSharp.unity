using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyCompany("Uno")]
[assembly: AssemblyProduct("Uno")]
[assembly: AssemblyTitle("Runtime.Domain")]

[assembly: AssemblyVersion("0.0.1")]
[assembly: AssemblyCopyright("(C) 2021 RGV, Raikish")]

[assembly: InternalsVisibleTo("Uno.Tests.Builders")]
[assembly: InternalsVisibleTo("Uno.Domain.Tests")]

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")] //Where the mocks are. 