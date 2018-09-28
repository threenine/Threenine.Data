using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestDatabase
{
    public class TestCategory
    {
        public TestCategory()
        {
            Products = new HashSet<TestProduct>();
        }
        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; }
        
        public ICollection<TestProduct> Products { get; set; }
    }
}