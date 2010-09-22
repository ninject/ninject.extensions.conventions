namespace Ninject.Extensions.Conventions.Tests
{
    using System.Reflection;
    using Ninject.Extensions.Conventions.Tests.Fakes;
#if SILVERLIGHT
#if SILVERLIGHT_MSTEST
    using MsTest.Should;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Fact = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
#else
    using UnitDriven;
    using UnitDriven.Should;
    using Fact = UnitDriven.TestMethodAttribute;
#endif
#else
    using Ninject.Tests.MSTestAttributes;
    using Xunit;
    using Xunit.Should;
#endif
    [TestClass]
    public class AssemblyLoadingTests
    {
        [Fact]
        public void SpecifyingBindingGeneratorTypeResolvesCorrectly()
        {
            using (IKernel kernel = new StandardKernel())
            {
                var scanner = new AssemblyScanner();
                scanner.From(Assembly.GetExecutingAssembly());
                scanner.BindWith<DefaultBindingGenerator>();
                kernel.Scan(scanner);
                var instance = kernel.Get<IDefaultConvention>();

                instance.ShouldNotBeNull();
                instance.ShouldBeInstanceOf<DefaultConvention>();
            }
        }

        [Fact]
        public void UsingDefaultConventionsResolvesCorrectly()
        {
            using (IKernel kernel = new StandardKernel())
            {
                var scanner = new AssemblyScanner();
                scanner.From(Assembly.GetExecutingAssembly());
                scanner.BindWithDefaultConventions();
                kernel.Scan(scanner);
                var instance = kernel.Get<IDefaultConvention>();

                instance.ShouldNotBeNull();
                instance.ShouldBeInstanceOf<DefaultConvention>();
            }
        }

        [Fact]
        public void TestBindingGeneratorInLambaSyntax()
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Scan(x =>
                             {
                                 x.From(Assembly.GetExecutingAssembly());
                                 x.BindWith<DefaultBindingGenerator>();
                             });
                var instance = kernel.Get<IDefaultConvention>();

                instance.ShouldNotBeNull();
                instance.ShouldBeInstanceOf<DefaultConvention>();
            }
        }
    }
}