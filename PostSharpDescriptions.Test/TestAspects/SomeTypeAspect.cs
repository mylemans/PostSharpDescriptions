using System;
using System.Reflection;
using PostSharp.Aspects;

namespace Mylemans.PostSharp.Descriptions.Test.TestAspects
{
    /// <summary>
    /// Aspect to demonstrate descriptions on types (classes)
    /// </summary>
    [Serializable]
    public sealed class SomeTypeAspect : TypeLevelAspect
    {
        public override bool CompileTimeValidate(Type type)
        {
            this.Describe("(SomeTypeAspect) Applied on '" + type.AsSignature(true) + "'", type);
           
            PostSharpDescription.Add<SomeTypeAspect>("(SomeTypeAspect) All your base types are belong to us", type);
            PostSharpDescription.Add<SomeTypeAspect>("(SomeTypeAspect) Last compile time: " + DateTime.Now, type);

            return base.CompileTimeValidate(type);
        }

        #region Implementation of IMethodLevelAspect

        public void RuntimeInitialize(MethodBase method)
        {
        }

        #endregion
    }
}