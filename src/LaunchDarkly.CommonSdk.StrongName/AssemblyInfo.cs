using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LaunchDarkly.ServerSdk")]
[assembly: InternalsVisibleTo("LaunchDarkly.ServerSdk.Tests")]

// Allow mock/proxy objects in unit tests to access internal classes
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
