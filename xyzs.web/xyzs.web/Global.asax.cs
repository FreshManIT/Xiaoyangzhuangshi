﻿using System.Web.Mvc;
using System.Web.Routing;
using FreshCommonUtility.Dapper;
using xyzs.service;

namespace xyzs.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
            DbLinkTestService.DbLink();
        }
    }
}
