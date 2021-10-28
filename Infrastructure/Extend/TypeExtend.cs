using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Infrastructure.Attributes;

namespace Infrastructure.Extend {
    public static class TypeExtend {
        public static string GetPropAttuibuteMapping<T> (this T t) where T : MemberInfo {

            foreach (var prop in t.GetType ().GetProperties ()) {
                if (prop.IsDefined (typeof (BaseAttribute), true)) {
                    var attribute = t.GetCustomAttribute<BaseAttribute> ();
                    return attribute.GetMappingName ();
                } else {
                    return t.Name;
                }
            }
            return default;
        }

        public static IEnumerable<PropertyInfo> GetFilterProp (this Type t) {

            return t.GetProperties ().Where (x => !x.IsDefined (typeof (FieldAttributes), true));

        }

    }

}