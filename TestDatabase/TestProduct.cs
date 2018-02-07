using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDatabase
{
  public  class TestProduct
    {
          
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       [Key]
        public int Id { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            public TestCategory Category { get; set; }
        }
    }

