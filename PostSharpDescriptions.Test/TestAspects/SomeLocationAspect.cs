using System;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Reflection;

namespace Mylemans.PostSharp.Descriptions.Test.TestAspects
{
    /// <summary>
    /// Aspect to demonstrate descriptions on fields / properties
    /// </summary>
    [Serializable]
    public sealed class SomeLocationAspect : LocationInterceptionAspect
    {
        public override void OnGetValue(LocationInterceptionArgs args)
        {
            throw new Exception("Get()");
        }

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            throw new Exception("Set()");
        }

        public override bool CompileTimeValidate(LocationInfo locationInfo)
        {
            lock (this)
            {
                if (locationInfo.PropertyInfo == null)
                {
                    PostSharpDescription.Add<SomeLocationAspect>(
                        "(SomeLocationAspect) If you 'get' or 'set' me, I'll throw an exception. Good you knew, right? Applied on field: " + locationInfo.FieldInfo.AsSignature(), locationInfo.FieldInfo);
                }
                else
                {
                    MethodInfo getMethod = locationInfo.PropertyInfo.GetGetMethod(true);
                    MethodInfo setMethod = locationInfo.PropertyInfo.GetSetMethod(true);

                    PostSharpDescription.Add<SomeLocationAspect>(
                        "(SomeLocationAspect) If you 'get' or 'set' me, I'll throw an exception. Good you knew, right? Applied on property: " + locationInfo.PropertyInfo.AsSignature(), locationInfo.PropertyInfo);
                
                    if (getMethod != null)
                    {
                        PostSharpDescription.Add<SomeLocationAspect>(
                            "(SomeLocationAspect) If you 'get' me, I'll throw an exception. Good you knew, right? Applied on method '" + getMethod.Name + "'", locationInfo.PropertyInfo, getMethod);
                    }

                    if (setMethod != null)
                    {
                        PostSharpDescription.Add<SomeLocationAspect>(
                           "(SomeLocationAspect) If you 'set' me, I'll throw an exception. Good you knew, right? Applied on method '" + setMethod.Name + "'", locationInfo.PropertyInfo, setMethod);
                    }
                }
                return base.CompileTimeValidate(locationInfo);
            }
        }
    }
}