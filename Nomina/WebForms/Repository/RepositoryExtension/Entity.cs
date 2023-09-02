using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.Repository.RepositoryExtension
{
    public abstract class Entity<TPrimaryKey>
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TPrimaryKey Id { get; set; }

        public object[] GetKeys()
        {
            return new object[] { Id };
        }
    }
}