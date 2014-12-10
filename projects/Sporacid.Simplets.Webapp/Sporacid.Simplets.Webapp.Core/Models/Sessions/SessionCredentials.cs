namespace Sporacid.Simplets.Webapp.Core.Models.Sessions
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SessionCredentials : AbstractModel, ICredentials
    {
        private const String UsernameRegex = "^[a-zA-Z]{2}[0-9]{5}$";

        private const String Ipv4Regex = "^([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\."
                                         + "([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\." + "([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\."
                                         + "([01]?\\d\\d?|2[0-4]\\d|25[0-5])$";

        [Required]
        [RegularExpression(Ipv4Regex)]
        public String Ipv4Address { get; set; }

        [RegularExpression(UsernameRegex)]
        public String Username { get; set; }

        [StringLength(25)]
        public String Password { get; set; }
    }
}