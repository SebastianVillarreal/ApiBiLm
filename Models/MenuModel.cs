using System.Collections.Generic;
namespace marcatel_api.Models
{
    public class MenuModel
    {
        
        public int Id { get; set; }
        
        public string Nombre { get; set; }
        
        public int Categoria { get; set; }

        
        public virtual List<ModuloModel> Modulos { get; set; }
        
        public virtual MenuModel Menu2 { get; set; }
    }

    public class ModuloModel
    {
        
        public string Nombre {get; set;}
        
        public int Id { get; set; }
        
        public int Categoria { get; set; }
        
        public string Descripcion { get; set; }
        
        public string Tema { get; set; }
        
        public string Ruta { get; set; }
        
        public string Icono { get; set; }
        
        public string NombreCategoria { get; set; }

    }
}