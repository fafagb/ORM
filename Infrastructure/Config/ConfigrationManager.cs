using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Linq;
namespace Infrastructure.Config {
    /// <summary>
    /// 固定读取根目录下面的appsettings.json
    /// </summary>
    public class ConfigrationManager {
        //有了IOC再去注入--容器单例

        //静态构造函数先于属性声明
        static ConfigrationManager () {
            var builder = new ConfigurationBuilder ()
                .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile ("appsettings.json");

            IConfigurationRoot configuration = builder.Build ();
            _SqlConnectionStringWrite = configuration["ConnectionStrings:Write"];
            _SqlConnectionStringRead = configuration.GetSection ("ConnectionStrings").GetSection ("Read").GetChildren ().Select (s => s.Value).ToArray ();
        }
        private static string _SqlConnectionStringWrite = null;
        public static string SqlConnectionStringWrite {
            get {
                return _SqlConnectionStringWrite;
            }
        }

        private static string[] _SqlConnectionStringRead = null;
        public static string[] SqlConnectionStringRead {
            get {
                return _SqlConnectionStringRead;
            }
        }
    }
}