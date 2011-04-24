using System;
using Mylemans.PostSharp.Descriptions.Test.TestAspects;

namespace Mylemans.PostSharp.Descriptions.Test
{
    [SomeTypeAspect]
    public class AClass
    {
        [SomeLocationAspect] 
        public bool Field;

        [SomeMethodAspect]
        public AClass()
        {
        }

        [SomeLocationAspect]
        [SomeOtherMethodAspect]
        public bool Property { get; set; }

        [SomeLocationAspect]
        public bool LocationProperty { get; set; }

        [SomeMethodAspect]
        public void GenericMethod<TGenericMethod>()
        {
        }

        [SomeMethodAspect]
        public void GenericMethod<TGenericMethod>(Action<bool, string> a, bool b)
        {
        }

        [SomeMethodAspect]
        public void Method()
        {
        }

        [SomeMethodAspect]
        public void Method(Action<bool, string> a, bool b)
        {
        }

        #region Nested type: ANestedClass

        public class ANestedClass
        {
            [SomeLocationAspect] 
            public bool Field;

            [SomeMethodAspect]
            public ANestedClass()
            {

            }

            [SomeLocationAspect] 
            [SomeMethodAspect] 
            public bool Property { get; set; }

            [SomeMethodAspect]
            public void GenericMethod<TGenericMethod>()
            {
            }

            [SomeMethodAspect]
            public void GenericMethod<TGenericMethod>(Action<bool, string> a, bool b)
            {
            }

            [SomeMethodAspect]
            public void Method()
            {
            }

            [SomeMethodAspect]
            public void Method(Action<bool, string> a, bool b)
            {
            }
        }

        #endregion
    }
}