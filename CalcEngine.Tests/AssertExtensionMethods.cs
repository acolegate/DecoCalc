using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcEngine.Tests
{
    [ExcludeFromCodeCoverage]
    public static class ExceptionAssert
    {
        public static TException Throws<TException>(Action action, string expectedMessage = null) where TException : Exception
        {
            try
            {
                action();
                Assert.Fail("Did not throw exception");

                // Will never be hit as the previous line (Assert.Fail) should throw an exception.
                return null;
            }
            catch (TException e)
            {
                Assert.AreSame(e.GetType(), typeof(TException), "Exception type did not match");

                if (expectedMessage != null)
                {
                    Assert.AreEqual(expectedMessage, e.Message, "Exception message did not match");
                }

                return e;
            }
        }

        public static TException Throws<TException>(Func<Task> action, string expectedMessage = null) where TException : Exception
        {
            try
            {
                Task task = action();
                task.Wait();

                Assert.Fail("Did not throw exception");

                // Will never be hit as the previous line (Assert.Fail) should throw an exception.
                return null;
            }
            catch (AggregateException aggregateException)
            {
                TException actualException = aggregateException.InnerException as TException;

                if (actualException == null)
                {
                    Assert.Fail("Expecting {0} but got {1}", typeof(TException), aggregateException.InnerException.GetType());
                }

                Assert.AreSame(actualException.GetType(), typeof(TException), "Exception type did not match");

                if (expectedMessage != null)
                {
                    Assert.AreEqual(expectedMessage, actualException.Message, "Exception message did not match");
                }

                return actualException;
            }
        }
    }
}