namespace Sporacid.Simplets.Webapp.Tools.Threading.Pooling.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    /// <author>Simon Turcotte-Langevin</author>
    public class WaitTimeoutException : WaitException
    {
        public WaitTimeoutException()
            : base("There was a timeout while waiting for the result to be computed.")
        {
            
        }
    }
}