using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace rl_contract.Models
{
    /// <summary>
    /// Model für den Benutzer
    /// (PoC, vereinfacht, Klärung der Benutzerverwaltung offen)
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nachname
        /// </summary>
        [Required(ErrorMessage = "Der Nachname muss angegeben werden")]
        [StringLength(50, ErrorMessage = "Der Nachname darf nicht mehr als {1} Zeichen lang sein")]
        [DisplayName("Nachname")]
        public string Nachname { get; set; }

        /// <summary>
        /// Vorname
        /// </summary>
        [StringLength(50, ErrorMessage = "Der Vorname darf nicht mehr als {1} Zeichen lang sein")]
        [DisplayName("Vorname")]
        public string Vorname { get; set; }

        /// <summary>
        ///     zugeordnete Organisationseinheit
        /// </summary>
        public int? OrganisationseinheitId { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        [StringLength(100, ErrorMessage = "Die E-Mail darf nicht mehr als {1} Zeichen enthalten")]
        [DisplayName("E-Mail")]
        [EmailAddress(ErrorMessage = "Bitte geben Sie eine gültige E-Mailadresse an (z.B. max@mustermann.de).")]
        public string EMail { get; set; }


        /// <summary>
        /// Login
        /// </summary>
        [Required(ErrorMessage = "Bitte den Anmeldenamen angeben")]
        [StringLength(50, ErrorMessage = "Der Anmeldename darf nicht mehr als {1} Zeichen haben")]
        [DisplayName("Nutzer")]
        public string Username { get; set; }

        /// <summary>
        /// Passwort
        /// </summary>
        [StringLength(100, ErrorMessage = "Das Passwort muss mindestens {2} Zeichen lang sein", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("Passwort")]
        public string Password { get; set; }
    }
}
