using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace CRUDSQLite.Models
{
   public class Usuario
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Estado { get; set; }

        public string  Ruta { get; set; }

        public string Secuencia { get; set; }

        public string Lectura { get; set; }

        public string LecturaAnterior { get; set; }
    }
}
