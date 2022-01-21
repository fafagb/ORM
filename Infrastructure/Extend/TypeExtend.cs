using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Infrastructure.Attributes;
using Infrastructure.Attributes.Filter;

namespace Infrastructure.Extend {
    public static class TypeExtend {
        public static string GetPropAttuibuteMapping<T> (this T t) where T : MemberInfo {

            //foreach (var prop in t.GetType ().GetProperties ()) {
                if (t.IsDefined (typeof (BaseAttribute), true)) {
                    var attribute = t.GetCustomAttribute<BaseAttribute> ();
                    return attribute.GetMappingName ();
                } else {
                    return t.Name;
                }
            //}
          //  return default;
        }
        /// <summary>
        /// 过滤掉主键返回全部属性
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetFilterProp (this Type t) {

            return t.GetProperties ().Where (x => !x.IsDefined (typeof (FilterAttribute), true));

        }

    }

}