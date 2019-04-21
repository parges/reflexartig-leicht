using System;
using System.Collections.Generic;
using System.Text;

namespace rl_contract.Models
{
    public class LoginModel
    {
        /// <summary>
        /// Abteilung
        /// </summary>
        public string Devision { get; set; }
        
        /// <summary>
        /// Nutzerkennung
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Passwort
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Passwort-Hash
        /// </summary>
        public string PasswordHash { get; set; }
    }
}
