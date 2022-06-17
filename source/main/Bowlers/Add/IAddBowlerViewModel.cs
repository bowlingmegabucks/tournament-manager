using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Bowlers.Add;

internal interface IViewModel
{
    Guid Id { get; set; }

    string FirstName { get; set; }

    string MiddleInitial { get; set; }

    string LastName { get; set; }

    string Suffix { get; set; }

    string StreetAddress { get; set; }

    string CityAddress { get; set; }

    string StateAddress { get; set; }

    string ZipCode { get; set; }

    string EmailAddress { get; set; }

    string PhoneNumber { get; set; }

    string USBCId { get; set; }

    DateOnly? DateOfBirth { get; set; }

    Models.Gender? Gender { get; set; }
}