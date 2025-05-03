using HRMgmtWeb.Services;

namespace HRMgmtWeb.Infrastructure
{
    public class SuperAdminStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var superAdminService = scope.ServiceProvider.GetRequiredService<ISuperAdminService>();
                    superAdminService.EnsureSuperAdminCreatedAsync().Wait();
                }
                next(app);
            };
        }
    }
}
