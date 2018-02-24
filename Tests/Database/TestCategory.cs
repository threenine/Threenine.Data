using System.ComponentModel.DataAnnotations;

namespace TestDatabase
{
    public class TestCategory
    {
        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; }
    }
}