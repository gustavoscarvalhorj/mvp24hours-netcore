//=====================================================================================
// Developed by Kallebe Lins (https://github.com/kallebelins)
//=====================================================================================
// Reproduction or sharing is free! Contribute to a better world!
//=====================================================================================
using Mvp24Hours.Application.MySql.Test.Support.Enums;
using Mvp24Hours.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mvp24Hours.Application.MySql.Test.Support.Entities
{
    public class Contact : EntityBase<Contact, int>
    {
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ContactType Type { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
