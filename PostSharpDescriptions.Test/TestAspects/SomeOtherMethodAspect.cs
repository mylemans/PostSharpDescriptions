using System;
using System.Reflection;
using PostSharp.Aspects;

namespace Mylemans.PostSharp.Descriptions.Test.TestAspects
{
    /// <summary>
    /// Aspect to demonstrate descriptions on methods
    /// </summary>
    [Serializable]
    public sealed class SomeOtherMethodAspect : OnMethodBoundaryAspect
    {
        public override void OnSuccess(MethodExecutionArgs args)
        {
            throw new Exception("So thats what I do. I throw exceptions!");
        }

        public override bool CompileTimeValidate(MethodBase method)
        {
            PostSharpDescription.Add<SomeOtherMethodAspect>(
               "(SomeOtherMethodAspect) Description applied on method '" + method.Name + "'", method);


            return true;
        }
    }
}