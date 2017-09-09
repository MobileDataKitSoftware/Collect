using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using SaasKit.Multitenancy;

namespace MobileDataKit.Model
{
    public class CompanyTenantResolver : SaasKit.Multitenancy.ITenantResolver<Company>
    {
        MobileDataKitDataContext mobileDataKitDataContext = null;

        public CompanyTenantResolver(MobileDataKitDataContext _MobileDataKitDataContext)
        {
            mobileDataKitDataContext = _MobileDataKitDataContext;
        }
        public System.Threading.Tasks.Task<SaasKit.Multitenancy.TenantContext<Company>> ResolveAsync(HttpContext context)
        {

            TenantContext<Company> tenantContext = null;

            Company tenant = null;
            if(!string.IsNullOrWhiteSpace(context.Request.Headers["companyid"].ToString()))
           tenant= mobileDataKitDataContext.Companies.Find(context.Request.Headers["companyid"]);

            if (tenant == null)
            {
                tenant = new Company();
                tenant.Email = "default@default.com";
            }
            tenantContext = new TenantContext<Company>(tenant);
            return System.Threading.Tasks.Task.FromResult<SaasKit.Multitenancy.TenantContext<Company>>( tenantContext);
        }
    }
}
