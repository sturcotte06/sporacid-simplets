namespace Sporacid.Simplets.Webapp.Tools
{
    using System;

    /// <summary>
    /// Static utility class that exposes snippet methods to ease 
    /// heavy syntaxes. Those must be used with parsimony.
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public static class Snippets
    {
        /// <summary>
        /// Utility method to express a one-liner try-finally block.
        /// </summary>
        /// <param name="try">The action to try</param>
        /// <param name="finally">The action to do when done</param>
        public static void TryFinally(Action @try, Action @finally)
        {
            try
            {
                @try();
            }
            finally
            {
                @finally();
            }
        }

        /// <summary>
        /// Utility method to express a one-liner try-catch-finally block.
        /// </summary>
        /// <param name="try">The action to try</param>
        /// <param name="catch">The action to do with the exception</param>
        /// <param name="finally">The action to do when done</param>
        public static void TryCatchFinally(Action @try, Action<Exception> @catch, Action @finally)
        {
            try
            {
                @try();
            }
            catch (Exception exception)
            {
                @catch(exception);
            }
            finally
            {
                @finally();
            }
        }

        /// <summary>
        /// Utility method to express a one-liner try-catch-finally block.
        /// </summary>
        /// <param name="try">The action to try</param>
        /// <param name="catch">The action to do with the exception</param>
        /// <param name="finally">The action to do when done</param>
        public static void TryCatchFinally<TException>(Action @try, Action<TException> @catch, Action @finally) where TException : Exception
        {
            try
            {
                @try();
            }
            catch (TException exception)
            {
                @catch(exception);
            }
            finally
            {
                @finally();
            }
        }

        /// <summary>
        /// Utility method to express a one-liner try-catch block.
        /// </summary>
        /// <param name="try">The action to try</param>
        /// <param name="catch">The action to do with the exception</param>
        public static void TryCatch(Action @try, Action<Exception> @catch)
        {
            try
            {
                @try();
            }
            catch (Exception exception)
            {
                @catch(exception);
            }
        }

        /// <summary>
        /// Utility method to express a one-liner try-catch block.
        /// </summary>
        /// <param name="try">The action to try</param>
        /// <param name="catch">The action to do with the exception</param>
        public static void TryCatch<TException>(Action @try, Action<TException> @catch) where TException : Exception
        {
            try
            {
                @try();
            }
            catch (TException exception)
            {
                @catch(exception);
            }
        }

        /// <summary>
        /// Does an action without return and then return a value.
        /// </summary>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="do">The void action to do.</param>
        /// <param name="return">The function that returns something.</param>
        /// <returns>The function return value.</returns>
        public static TReturn DoThenReturn<TReturn>(Action @do, Func<TReturn> @return)
        {
            @do();
            return @return();
        }
    }
}