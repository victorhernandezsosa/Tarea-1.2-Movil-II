using System.Text.RegularExpressions;
using SQLite;

namespace Tarea_1._2.Controller
{
    public class Empleado
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombres {  get; set; }
        public string Apellidos { get; set;}
        public DateTime FechaNacimiento { get; set; }
        public string Correo {  get; set; }

        [Ignore]
        public string completo => $"{Nombres} {Apellidos} - {FechaNacimiento.ToString("dd/MM/yyyy")}";

        public bool ValidarCorreo()
        {
            Regex regex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
            return regex.IsMatch(Correo);
        }
    }



    public class EmpleBase
    {
        readonly SQLiteAsyncConnection _database;

        public EmpleBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Empleado>().Wait();
        }

        public Task<List<Empleado>> GetEmpleadoAsync() 
        {
            return _database.Table<Empleado>().ToListAsync();
        }

        public Task<int> GuardarEmpleado(Empleado empleado) 
        {
            if (empleado.Id != 0)
            {
                return _database.UpdateAsync(empleado);
            }
            else
            {
                return _database.InsertAsync(empleado);
            }
        }

        public Task<int> DeleteEmpleado(int empleadoId)
        {
            return _database.DeleteAsync<Empleado>(empleadoId);
        }
    }
}
