using Mylemans.PostSharp.Descriptions.Test.TestAspects;

namespace Mylemans.PostSharp.Descriptions.Test
{
    [SomeTypeAspect]
    [SomeMethodAspect]
    public class BasicTestClass
    {
        [SomeLocationAspect] 
        public bool Field;

// ReSharper disable EmptyConstructor
        public BasicTestClass()
// ReSharper restore EmptyConstructor
        {

        }

        [SomeLocationAspect] 
        [SomeMethodAspect] 
        public bool Property
        {
            get;
            [SomeMethodAspect]
            set;
        }

        [SomeMethodAspect]
        public void Method()
        {

        }

        public void AnotherMethod()
        {

        }
    }
}
