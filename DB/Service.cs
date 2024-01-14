using SQLite;

namespace ServiceBors.DB
{
    [Table("service")]
    public class Service
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("serviceName")]
        public string ServiceName { get; set; }

        [Column("serviceDescription")]
        public string ServiceDescription { get; set; }

        [Column("servicePrice")]
        public decimal ServicePrice { get; set; }
    }
}
