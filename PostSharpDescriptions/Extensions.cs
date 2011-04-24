using System;
using System.Linq;
using System.Reflection;

namespace Mylemans.PostSharp.Descriptions
{
    /// <summary>
    /// Some handy extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Helper to check if 'declaringType' has 'genericParameterType'
        /// </summary>
        private static bool HasGeneric(Type declaringType, Type genericParameterType)
        {
            try
            {
                if (declaringType.GetGenericArguments().Length > 0)
                {
                    foreach (var a in declaringType.GetGenericArguments())
                    {
                        if (a.Name == genericParameterType.Name)
                            return true;
                    }
                }
            }
            catch
            {
                // Silently ignored
            }
            return false;
        }
        /// <summary>
        /// Returns a PostSharp compatible type reference representation
        /// </summary>
        public static string AsSignature(this Type target)
        {
            return target.AsSignature(true);
        }

        /// <summary>
        /// Returns a PostSharp compatible type reference representation
        /// </summary>
        public static string AsSignature(this Type target, bool fullSignature)
        {
            string signature = target.FullName ?? target.Name;

            if (target.IsNested && fullSignature)
            {
                signature = target.DeclaringType.AsSignature(true) + "+" + target.Name;
            }

            try
            {
                if (target.GetGenericArguments().Length > 0)
                {
                    // var fullsignature = target.GetGenericTypeDefinition().FullName;

                    signature = signature.Substring(0, signature.IndexOf("`"));

                    signature += "{";
                    foreach (var gt in target.GetGenericArguments().Where(a => !fullSignature || a.DeclaringType == target))
                    {
                        if (target.IsNested && HasGeneric(target.DeclaringType, gt) && fullSignature)
                            continue; 
                        if (!signature.EndsWith("{"))
                        {
                            signature += ",";
                        }
                        if (gt.IsGenericParameter)
                        {
                            signature += gt.Name;
                        }
                        else
                        {
                            signature += gt.FullName;
                        }
                    }
                    signature += "}";


                    if (signature.EndsWith("{}"))
                    {
                        signature = signature.Substring(0, signature.Length - 2);
                    }
                }
            }
            catch
            {
                // Silently ignored
            }
            if (target.IsArray)
            {
                signature += "[]";
            }

            return signature;
        }

        /// <summary>
        /// Returns a PostSharp compatible type reference representation
        /// 
        /// Example: Namespace.ClassType::SomeProperty()
        /// </summary>
        public static string AsSignature(this PropertyInfo target)
        {
            return target.DeclaringType.AsSignature() + "::" + target.Name + "()";
        }

        /// <summary>
        /// Returns a PostSharp compatible type reference representation
        /// 
        /// Example: Namespace.ClassType::SomeProperty()
        /// </summary>
        public static string AsSignature(this FieldInfo target)
        {
            return target.DeclaringType.AsSignature() + "::" + target.Name;
        }

        /// <summary>
        /// Returns a PostSharp compatible type reference representation. 
        /// Outputs the full signature, minus the return type. 
        /// 
        /// If you need the return type also @ the signature, just prefix it with the AsSignature of the return type (and a space).
        /// 
        /// <example>
        /// Usage: string reference = someMethodInfo.AsPSReference();
        /// 
        /// Example output: Namespace.DeclaringType::Method{T1, T2}(T1, T2, System.Action{T,T2}, System.Int32)
        /// </example>
        /// </summary>
        public static string AsSignature(this MethodBase target)
        {
            return AsSignature(target, false);
        }

        /// <summary>
        /// Turns .ctor into #ctor
        /// </summary>
        private static string SafeName(this string name)
        {
            if (name.StartsWith("."))
            {
                return "#" + name.Substring(1);
            }

            return name;
        }

        /// <summary>
        /// Returns a PostSharp compatible type reference representation. 
        /// Outputs the full signature, and optionally (if it has any) the return type.
        /// <example>
        /// Usage: string reference = someMethodInfo.AsPSReference();
        /// 
        /// Example output: Namespace.DeclaringType::Method{T1, T2}(T1, T2, System.Action{T,T2}, System.Int32)
        /// </example>
        /// </summary>
        public static string AsSignature(this MethodBase target, bool includeReturnType)
        {
            var methodSignature = target.DeclaringType.AsSignature() + "::" + target.Name.SafeName();

            try
            {
                if (target.GetGenericArguments().Length > 0)
                {
                    methodSignature += "{";
                    foreach (var gt in target.GetGenericArguments())
                    {
                        if (!methodSignature.EndsWith("{"))
                        {
                            methodSignature += ", ";
                        }
                        methodSignature += gt.Name;
                    }
                    methodSignature += "}";
                }
            }
            catch
            {
                // Silently ignored
            }

            methodSignature += "(";

            foreach (var a in target.GetParameters())
            {
                if (!methodSignature.EndsWith("("))
                {
                    methodSignature += ", ";
                }

                methodSignature += AsSignature(a.ParameterType, false);
            }
            methodSignature += ")";

            if (includeReturnType && (target as MethodInfo) != null)
            {
                methodSignature = (target as MethodInfo).ReturnType.AsSignature() + " " + methodSignature;
            }

            return methodSignature;
        }
    }
}
