using Dotnetstore.WebAPI.Intranet.Interfaces;
using Dotnetstore.WebAPI.Intranet.Services;

IStartupHelperService startupHelperService = new StartupHelperService();

var (builder, serviceCollection) = startupHelperService.GetServices(args);
startupHelperService.BuildServices(ref serviceCollection);

var app = builder.Build();
startupHelperService.SetRuntime(ref app);