namespace marcatel_api.Models
{
    public class VentasFamiliaModel
    {
        public string Nombre { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Venta { get; set; }
        public decimal Costo { get; set; }
        public string Departamento { get; set; }
        public string ClaveDepartamento { get; set; }
        public decimal CantidadDev { get; set; }
        public decimal CostoDev { get; set; }
        public decimal VentaDev { get; set; }
        public string Familia { get; set; }
        public string ClaveProveedor { get; set; }
    }

    public class VentaFamiliaSucursalModel
    {
        public string Sucursal { get; set; }
        public string Departamento { get; set; }
        public string Familia { get; set; }
        public decimal Venta { get; set; }
        public decimal Costo { get; set; }
        public decimal Cantidad { get; set; }
        public string Tipo { get; set; }
    }

    public class VentasFamiliaSirota
    {
        public string Sucursal { get; set; }
        public string Departamento { get; set; }
        public string Familia { get; set; }
        public decimal Venta { get; set; }
        public decimal Sirota { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Costo { get; set; }
    }
}