using Microsoft.EntityFrameworkCore;
using MvcCoreEF.Data;
using MvcCoreEF.Models;

namespace MvcCoreEF.Rrepositories
{
    public class RepositoryHospital
    {

        private HospitalContext context;

        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            var consulta = from datos in this.context.Hospitales
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<Hospital> FindHospitalAsync(int idHospital)
        {
            var consulta = from datos in this.context.Hospitales
                           where datos.IdHospital == idHospital
                           select datos;

            return await consulta.FirstOrDefaultAsync();

        }

        public async Task InsertHospitalAsync(int idHospital, string nombre, string direccion, string telefono, int camas)
        {
            Hospital hospital = new Hospital();

            hospital.IdHospital = idHospital;
            hospital.Nombre = nombre;
            hospital.Direccion = direccion;
            hospital.Telefono = telefono;
            hospital.Camas = camas;

            this.context.Hospitales.AddAsync(hospital);

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteHospitalAsync(int idHospital)
        {
            Hospital hospital = await this.FindHospitalAsync(idHospital);
            this.context.Hospitales.Remove(hospital);

            await this.context.SaveChangesAsync();
        }

        public async Task UpdateHospitalAsync(int idHospital, string nombre, string direccion, string telefono, int camas)
        {
            Hospital hospital = await this.FindHospitalAsync(idHospital);
            hospital.Nombre = nombre;
            hospital.Direccion = direccion;
            hospital.Telefono = telefono;
            hospital.Camas = camas;

            await this.context.SaveChangesAsync();

        }
    }
}
