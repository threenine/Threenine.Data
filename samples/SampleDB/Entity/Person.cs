using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Entity
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(10)] public string Title { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Profile { get; set; }

        public string TagLine { get; set; }
    }
}