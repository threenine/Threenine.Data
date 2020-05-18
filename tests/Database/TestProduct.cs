using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDatabase
{
    public class TestProduct
    {
       
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public TestCategory Category { get; set; }

        public int CategoryId { get; set; }
        
        public int Stock { get; set; }
        
        public Nullable<bool> InStock { get; set; }   //SQL Lite does not have bit or boolean datatypes  https://www.sqlite.org/datatype3.html
                                           // Boolean values are stored as integers 0 (false) and 1 (true)
    }
}