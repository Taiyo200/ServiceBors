using SQLite;

namespace ServiceBors.DB
{
    public class LocalDbService
    {
        private const string DB_NAME = "service_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Appointment>();
            _connection.CreateTableAsync<Service>();
            _connection.CreateTableAsync<Partner>();
        }

        public async Task<List<Appointment>> GetAppointments()
        {
            return await _connection.Table<Appointment>().ToListAsync();
        }

        public async Task<Appointment> GetById(int id)
        {
            return await _connection.Table<Appointment>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Appointment appointment)
        {
            await _connection.InsertAsync(appointment);
        }
        public async Task Update(Appointment appointment)
        {
            await _connection.UpdateAsync(appointment);
        }
        public async Task Delete(Appointment appointment)
        {
            await _connection.UpdateAsync(appointment);
        }

        public async Task<List<Service>> GetServices()
        {
            return await _connection.Table<Service>().ToListAsync();
        }

        public async Task<Service> GetServiceById(int id)
        {
            return await _connection.Table<Service>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateService(Service service)
        {
            await _connection.InsertAsync(service);
        }

        public async Task UpdateService(Service service)
        {
            await _connection.UpdateAsync(service);
        }

        public async Task DeleteService(Service service)
        {
            await _connection.DeleteAsync(service);
        }
        public async Task<List<Partner>> GetPartners()
        {
            return await _connection.Table<Partner>().ToListAsync();
        }

        public async Task<Partner> GetPartnerById(int id)
        {
            return await _connection.Table<Partner>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreatePartner(Partner partner)
        {
            await _connection.InsertAsync(partner);
        }

        public async Task UpdatePartner(Partner partner)
        {
            await _connection.UpdateAsync(partner);
        }

        public async Task DeletePartner(Partner partner)
        {
            await _connection.DeleteAsync(partner);
        }

    }
}
