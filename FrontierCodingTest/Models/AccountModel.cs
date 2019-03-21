using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontierCodingTest.Models
{
    public class AccountModel
    {        
        public int Id { get; set; }

        [JsonIgnore]
        [Display(Name = "Name")]
        public string FullName { get { return $"{LastName}, {FirstName}"; } }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [JsonIgnore]
        [Display(Name = "Phone Number")]
        public string PhoneNumberFormatted { get { return string.IsNullOrWhiteSpace(PhoneNumber) ? "" : $"({PhoneNumber.Substring(0,3)})-{PhoneNumber.Substring(3, 3)}-{PhoneNumber.Substring(6)}"; } }

        [Phone]
        public string PhoneNumber { get; set; }
                
        [DataType(DataType.Currency)]
        [Display(Name = "Ammount Due")]
        public decimal AmountDue { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM'/'dd'/'yyyy}", NullDisplayText = "")]
        [Display(Name = "Payment Due Date")]
        public DateTimeOffset? PaymentDueDate { get; set; }

        [EnumDataType(typeof(AccountStatus))]
        public AccountStatus AccountStatusId { get; set; }

    }
}