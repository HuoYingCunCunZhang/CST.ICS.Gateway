using Autofac;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace CST.ICS.Gateway
{
    internal class AutofacModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //注册Engine
            Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("CST.ICS.Gateway"));
            builder.RegisterAssemblyTypes(assembly)
            .Where(cc => cc.Name.EndsWith("Engine"))
            .SingleInstance()
            .AsImplementedInterfaces();

            //注册文件提供程序
            builder.RegisterType<PhysicalFileProvider>().SingleInstance().As<IFileProvider>().WithParameter(new TypedParameter(typeof(string), AppDomain.CurrentDomain.BaseDirectory)); 

            //注册消息层
            builder.Register<>
            //注册业务命令层
        }
    }
}
