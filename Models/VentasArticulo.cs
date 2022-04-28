namespace marcatel_api.Models
{
    public class VentasArticuloModel
    {
        public int Id {get; set;}
        public string Articulo {get; set;}
        public string Descripcion {get; set;}
        public decimal Cantidad {get; set;}
        public decimal Total {get; set;}
        public string ClaveProveedor {get; set;}
        public int IdSucursal {get; set;}
        public string Fecha {get; set;}
        public decimal ArtnCostoUnitario {get; set;}
        public decimal ArtnUltimoPrecio {get; set;}


    }
}