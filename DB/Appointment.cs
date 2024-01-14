using SQLite;

namespace ServiceBors.DB
{
    [Table("appointment")]
    public class Appointment
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("carplate")]
        public string CarPlate { get; set; }
        [Column("carmodel")]
        public string CarModel { get; set; }
        [Column("service")]
        public string Service { get; set; }
        [Column("issue")]
        public string Issue { get; set; }

    }
}
