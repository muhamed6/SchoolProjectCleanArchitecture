﻿using SchoolProject.Data.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entities
{
    public class Instructor : GeneralLocalizableEntity
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            Ins_Subjects = new HashSet<Ins_Subject>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int InsId { get; set; }
        public string? ENameAr { get; set; }
        public string? ENameEn { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }

        public int? DID { get; set; }
        [ForeignKey(nameof(DID))]
        [InverseProperty("Instructors")]
        public Department? Department { get; set; }

        [InverseProperty("Instructor")]
        public Department? DepartmentManager { get; set; }

        [ForeignKey(nameof (SupervisorId))]
        [InverseProperty("Instructors")]
        public Instructor? Supervisor { get; set; }

        [InverseProperty("Supervisor")]
        public virtual ICollection<Instructor> Instructors { get; set; }


        [InverseProperty("Instructor")]
        public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
    }
}
