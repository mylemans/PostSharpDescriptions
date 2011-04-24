using System;
using Mylemans.PostSharp.Descriptions.Test.TestAspects;

namespace Mylemans.PostSharp.Descriptions.Test
{
    [SomeTypeAspect]
    public class AGenericClass<TType>
    {
        [SomeLocationAspect] 
        public bool Field;

        [SomeMethodAspect]
        public AGenericClass()
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

        #region Nested type: AGenericNestedClass

        [SomeTypeAspect]
        [SomeMethodAspect]
        public class AGenericNestedClass<TNestedType>
        {
            public class AGenericNestedNestedClass<TNestedNestedType>
            {

            }

            [SomeLocationAspect] 
            public bool Field;

            [SomeMethodAspect]
            public AGenericNestedClass()
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