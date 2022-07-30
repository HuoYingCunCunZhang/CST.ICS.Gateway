using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace CST.ICS.Gateway.Common
{
    public class ReflectionHelper
    {
        /// <summary>
        /// 根据前缀后缀查询所有依赖的程序集
        /// 注意：必须使用引用放到依赖项中才能使用
        /// </summary>
        /// <param name="perfix">要匹配的程序集前缀，例如："GDZ"</param>
        /// <param name="suffix">要匹配的程序集后缀，例如："Service"</param>
        /// <returns></returns>
        public static IList<Assembly> GetAssemblies(string perfix = "", string suffix = "")
        {
            var assemblyList = new List<Assembly>();
            DependencyContext dependencyContext = DependencyContext.Default;
            IEnumerable<CompilationLibrary> libs = dependencyContext.CompileLibraries
                .Where(lib => !lib.Serviceable && lib.Type != "package" && lib.Name.StartsWith(perfix) && lib.Name.EndsWith(suffix));
            foreach (var lib in libs)
            {
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                assemblyList.Add(assembly);
            }
            return assemblyList.ToArray();
        }

        /// <summary>
        /// 根据前缀后缀查询所有依赖的程序集
        /// 注意：必须使用引用放到依赖项中才能使用
        /// </summary>
        /// <param name="perfix">要匹配的程序集前缀，例如："GDZ"</param>
        /// <param name="suffix">要匹配的程序集后缀，例如："Service"</param>
        /// <returns></returns>
        public static IList<Assembly> GetAllAssemblies(string perfix = "", string suffix = "")
        {
            var assemblyList = new List<Assembly>();
            var dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var libs = dirInfo.GetFiles().Where(f => f.Name.StartsWith(perfix) && f.Name.EndsWith(suffix + ".dll"));
            foreach (var lib in libs)
            {
                Assembly assembly = Assembly.LoadFrom(lib.FullName);
                assemblyList.Add(assembly);
            }
            return assemblyList.ToArray();
        }
    }
}
