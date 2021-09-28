using SQLite;

namespace ScanApp.Models
{
    public class CarpetaItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
    }

    public class UsuarioModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LoginTime { get; set; }
        public string Device { get; set; }
    }
    public class InformacionPersonalModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Profesion { get; set; }
        [Unique]
        public string Email { get; set; }
        //------------------------------------//
        public int Hijos { get; set; }
        public string NombreHijos { get; set; }
        public int Mascotas { get; set; }
        public string NombreMascotas { get; set; }
        public string PagaServicios { get; set; }
        public string ViviendaPropia { get; set; }
        public string CreditoViviendaArriendo { get; set; }
        public string Vehiculo { get; set; }
        public string CreditoVehicular { get; set; }
        public string Labora { get; set; }
        public string VinculosLaborales { get; set; }
        public string NombreEmpresasLaborales { get; set; }
        public string EducacionFormal { get; set; }
        public string EntidadesEducativas { get; set; }
        public string EntidadesFinancieras { get; set; }

    }


}
