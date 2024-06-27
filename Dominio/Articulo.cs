using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [DisplayName("Marca")]
        public Marca IdMarca { get; set; }
        [DisplayName("Categoria")]
        public Categoria IdCategoria { get; set; }
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; }



    }
}
