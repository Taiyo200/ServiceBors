using SQLite;

namespace ServiceBors.DB
{
    [Table("partner")]
    public class Partner
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("partnerName")]
        public string PartnerName { get; set; }

        [Column("partnerLocation")]
        public string PartnerLocation { get; set; }

        [Column("partnerType")]
        public string PartnerType { get; set; }
    }
}
