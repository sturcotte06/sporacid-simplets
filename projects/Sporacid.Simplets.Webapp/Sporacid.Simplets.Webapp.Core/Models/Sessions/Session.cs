namespace Sporacid.Simplets.Webapp.Core.Models.Sessions
{
    using System;
    using System.Collections.Generic;

    public class Session : AbstractModel
    {
        public Session()
        {
            this.Data = new Dictionary<string, object>();
        }

        public Dictionary<String, Object> Data { get; private set; }
    }
}