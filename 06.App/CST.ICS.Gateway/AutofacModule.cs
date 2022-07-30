using Autofac;
using CST.ICS.Gateway.Common;
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

            #region 注册Engine
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("开始注册【启动引擎层】...");
            var assemblys = ReflectionHelper.GetAllAssemblies(ConstValue.AssemblyPrefix, ConstValue.AssemblySuffixOfApp);
            foreach (var assembly in assemblys)
            {
                Console.WriteLine("程序集名称：{0}", assembly.FullName);
                builder.RegisterAssemblyTypes(assembly)
                .Where(cc => cc.Name.EndsWith(ConstValue.ClassSuffixOfEngine))
                .SingleInstance()
                .PropertiesAutowired() //支持属性注入
                .AsImplementedInterfaces();
            }
            Console.WriteLine("注册【启动引擎层】完成！");
            #endregion

            #region 注册消息层服务层
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("开始注册【消息服务层】...");
            assemblys = ReflectionHelper.GetAllAssemblies(ConstValue.AssemblyPrefix, ConstValue.AssemblySuffixOfService);
            foreach (var assembly in assemblys)
            {
                Console.WriteLine("程序集名称：{0}", assembly.FullName);
                builder.RegisterAssemblyTypes(assembly)
                .Where(cc => cc.Name.EndsWith(ConstValue.ClassSuffixOfService))
                .SingleInstance()
                //.PropertiesAutowired() //支持属性注入
                .AsImplementedInterfaces();
            }
            Console.WriteLine("注册【消息服务层】完成！");
            #endregion

            #region 注册业务命令层
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("开始注册【业务命令层】...");
            assemblys = ReflectionHelper.GetAllAssemblies(ConstValue.AssemblyPrefix, ConstValue.AssemblySuffixOfCommand);
            foreach (var assembly in assemblys)
            {
                Console.WriteLine("程序集名称：{0}", assembly.FullName);
                builder.RegisterAssemblyTypes(assembly)
                .Where(cc => cc.Name.EndsWith(ConstValue.ClassSuffixOfCommand))
                .SingleInstance()
                .PropertiesAutowired() //支持属性注入
                .AsImplementedInterfaces();
            }
            Console.WriteLine("注册【业务命令层】完成！");
            #endregion

            #region 注册设备层
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("开始注册【设备通讯层】...");
            assemblys = ReflectionHelper.GetAllAssemblies(ConstValue.AssemblyPrefix, ConstValue.AssemblySuffixOfDevice);
            foreach (var assembly in assemblys)
            {
                Console.WriteLine("程序集名称：{0}", assembly.FullName);
                builder.RegisterAssemblyTypes(assembly)
                .Where(cc => cc.Name.EndsWith(ConstValue.ClassSuffixOfDevice))
                .InstancePerDependency() //瞬时生命周期，每次获取都会出现一个新的实例
                //.InstancePerLifetimeScope() 作用域内生命周期
                .AsImplementedInterfaces();
            }
            Console.WriteLine("注册【设备通讯层】完成！");
            #endregion

            #region 注册其它类型

            //注册文件提供程序
            builder.RegisterType<PhysicalFileProvider>().SingleInstance().As<IFileProvider>().WithParameter(new TypedParameter(typeof(string), AppDomain.CurrentDomain.BaseDirectory)); 
            #endregion

            Console.ForegroundColor = ConsoleColor.White;
           
        }
    }
}
