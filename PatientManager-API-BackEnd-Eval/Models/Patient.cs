using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManager_API_BackEnd_Eval.Models;

public partial class Patient
{
    public int Pid { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime BirthDate { get; set; }

    public string? Gender { get; set; }

    public string? MiddleName { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? Zip { get; set; }

    public string? SystemStatus { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateLastUpdate { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int? AgeYears { get; private set; }
}
