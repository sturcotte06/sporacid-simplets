namespace Sporacid.Simplets.Webapp.Core.Models.Sessions
{
    using System;

    public class SessionToken : AbstractModel
    {
        public String SessionKey { get; set; }
        public DateTime EmitDateTime { get; set; }
        public TimeSpan ValidityInterval { get; set; }
    }
}