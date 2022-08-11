using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using marcatel_api.DataContext;
using marcatel_api.Models;
using System.Collections.Generic;

namespace marcatel_api.Services
{
    public class AdminService
    {
        private  string connection;

        public AdminService(IMarcatelDatabaseSetting settings)
        {
            connection = settings.ConnectionString;
        }

        public List<MenuModel> GetCategorias(int usuario)
        {
            ArrayList parametros = new ArrayList();
            ArrayList par2 = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            List<MenuModel> lista = new List<MenuModel>();
            List<ModuloModel> modulos = new List<ModuloModel>();

            try
            {
                parametros.Add(new SqlParameter { ParameterName = "@pIdUsuario", SqlDbType = SqlDbType.Int, Value = usuario });
                DataSet ds = dac.Fill("sp_get_categorias", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        int categoria = int.Parse(row["id"].ToString());
                        par2.Add(new SqlParameter { ParameterName = "@pIdCategoria", SqlDbType = SqlDbType.Int, Value = categoria });
                        par2.Add(new SqlParameter { ParameterName = "@pIdUsuario", SqlDbType = SqlDbType.Int, Value = usuario });
                        DataSet dsModulos = dac.Fill("sp_get_modulos", par2);
                        if(dsModulos.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow rowModulo in dsModulos.Tables[0].Rows)
                            {
                                modulos.Add(new ModuloModel()
                                {
                                    Id = int.Parse(rowModulo["Id"].ToString()),
                                    Nombre = rowModulo["Nombre"].ToString(),
                                    Categoria = int.Parse(rowModulo["categoria"].ToString()),
                                    Tema = rowModulo["tema"].ToString(),
                                    Icono = rowModulo["icono"].ToString(),
                                    Ruta = rowModulo["nombre_carpeta"].ToString()
                                });
                            }
                            par2 = new ArrayList();
                        }
                        lista.Add(new MenuModel()
                        {
                            Nombre = row["Nombre"].ToString(),
                            Id = int.Parse(row["Id"].ToString()),
                            Modulos = modulos
                        });
                        
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<VentasArticuloModel> GetDimVentasArticulo(int sucursal, string fecha_inicial, string fecha_final)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            
            //filtros.FechaInicial = (filtros.FechaInicial == "") ? "0000-00-00" : filtros.FechaInicial;
            //filtros.FechaFinal = (filtros.FechaFinal == "") ? "0000-00-00" : filtros.FechaFinal;
            var lista = new List<VentasArticuloModel>();
            parametros.Add(new SqlParameter { ParameterName = "@pIdSucursal", SqlDbType = SqlDbType.VarChar, Value = sucursal});
            parametros.Add(new SqlParameter { ParameterName = "@pFechaInicial", SqlDbType = SqlDbType.VarChar, Value = fecha_inicial });
            parametros.Add(new SqlParameter { ParameterName = "@pFechaFinal", SqlDbType = SqlDbType.VarChar, Value = fecha_final });
            try
            {
                DataSet ds = dac.Fill("GetDimVentasArticulo", parametros);
                if(ds.Tables.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new VentasArticuloModel
                        {
                            Articulo = dr["Articulo"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            Cantidad = decimal.Parse(dr["Cantidad"].ToString()),
                            Total = decimal.Parse(dr["Total"].ToString()),
                            ClaveProveedor = dr["ClaveProveedor"].ToString(),
                            IdSucursal = int.Parse(dr["IdSucursal"].ToString()),
                            Fecha = dr["Fecha"].ToString(),
                            ArtnCostoUnitario = decimal.Parse(dr["ARTN_CostoUnitario"].ToString()),
                            ArtnUltimoPrecio = decimal.Parse(dr["ARTN_UltimoPrecio"].ToString()),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return lista;
        }

        public List<TotalesModel> GetTotalVentasCantidad( string fecha_inicial, string fecha_final)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            
            //filtros.FechaInicial = (filtros.FechaInicial == "") ? "0000-00-00" : filtros.FechaInicial;
            //filtros.FechaFinal = (filtros.FechaFinal == "") ? "0000-00-00" : filtros.FechaFinal;
            var lista = new List<TotalesModel>();
            parametros.Add(new SqlParameter { ParameterName = "@pFechaInicial", SqlDbType = SqlDbType.VarChar, Value = fecha_inicial });
            parametros.Add(new SqlParameter { ParameterName = "@pFechaFinal", SqlDbType = SqlDbType.VarChar, Value = fecha_final });
            try
            {
                DataSet ds = dac.Fill("GetTotalVentasCantidadSucursalFechas", parametros);
                if(ds.Tables.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new TotalesModel
                        {
                            IdSucursal = int.Parse(dr["IdSucursal"].ToString()),
                            TotalDinero = decimal.Parse(dr["TotalDinero"].ToString()),
                            TotalUnidades = decimal.Parse(dr["TotalUnidades"].ToString()),
                            TotalCosto = decimal.Parse(dr["TotalCosto"].ToString()),
                            Devolucion = decimal.Parse(dr["devolucionCantidad"].ToString()),
                            DevolucionCosto = decimal.Parse(dr["DevCosto"].ToString()),
                            DevolucionPrecio = decimal.Parse(dr["DevVenta"].ToString()),
                            

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return lista;
        }

        public List<VentasFamiliaModel> GetVentasFamiliaSucursal( string fecha_inicial, string fecha_final, int sucursal)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            
            //filtros.FechaInicial = (filtros.FechaInicial == "") ? "0000-00-00" : filtros.FechaInicial;
            //filtros.FechaFinal = (filtros.FechaFinal == "") ? "0000-00-00" : filtros.FechaFinal;
            var lista = new List<VentasFamiliaModel>();
            parametros.Add(new SqlParameter { ParameterName = "@pFechaInicial", SqlDbType = SqlDbType.VarChar, Value = fecha_inicial });
            parametros.Add(new SqlParameter { ParameterName = "@pFechaFinal", SqlDbType = SqlDbType.VarChar, Value = fecha_final });
            parametros.Add(new SqlParameter { ParameterName = "@pSucursal", SqlDbType = SqlDbType.VarChar, Value = sucursal });
            try
            {
                DataSet ds = dac.Fill("GetVentasFamiliaSucursalFecha", parametros);
                if(ds.Tables.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new VentasFamiliaModel
                        {
                            Nombre = dr["Nombre"].ToString(),
                            Cantidad = decimal.Parse(dr["Cantidad"].ToString()),
                            Venta = decimal.Parse(dr["Venta"].ToString()),
                            Costo = decimal.Parse(dr["Costo"].ToString()),
                            Departamento = dr["NombreDepto"].ToString(),
                            ClaveDepartamento = dr["ClaveDepto"].ToString(),
                            CostoDev = decimal.Parse(dr["CostoDev"].ToString()),
                            CantidadDev = decimal.Parse(dr["CantidadDev"].ToString()),
                            VentaDev = decimal.Parse(dr["VentaDev"].ToString()),
                            Familia = dr["Familia"].ToString()
                            
                            
                            

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return lista;

        }

        public List<VentasFamiliaModel> GetVentasDepartamentoSucursal( string fecha_inicial, string fecha_final, int sucursal)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            
            //filtros.FechaInicial = (filtros.FechaInicial == "") ? "0000-00-00" : filtros.FechaInicial;
            //filtros.FechaFinal = (filtros.FechaFinal == "") ? "0000-00-00" : filtros.FechaFinal;
            var lista = new List<VentasFamiliaModel>();
            parametros.Add(new SqlParameter { ParameterName = "@pFechaInicial", SqlDbType = SqlDbType.VarChar, Value = fecha_inicial });
            parametros.Add(new SqlParameter { ParameterName = "@pFechaFinal", SqlDbType = SqlDbType.VarChar, Value = fecha_final });
            parametros.Add(new SqlParameter { ParameterName = "@pIdSucursal", SqlDbType = SqlDbType.VarChar, Value = sucursal });
            try
            {
                DataSet ds = dac.Fill("GetVentasDepartamentoSucursal", parametros);
                if(ds.Tables.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new VentasFamiliaModel
                        {
                            Nombre = dr["NombreDepto"].ToString(),
                            Cantidad = decimal.Parse(dr["Cantidad"].ToString()),
                            Venta = decimal.Parse(dr["Venta"].ToString()),
                            Costo = decimal.Parse(dr["Costo"].ToString()),
                            ClaveDepartamento = dr["ClaveDepto"].ToString()
                            

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return lista;
        }

        public List<VentasFamiliaModel> GetVentasProveedor( string fecha_inicial, string fecha_final, int sucursal)
        {
            ArrayList parametros = new ArrayList();
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            
            //filtros.FechaInicial = (filtros.FechaInicial == "") ? "0000-00-00" : filtros.FechaInicial;
            //filtros.FechaFinal = (filtros.FechaFinal == "") ? "0000-00-00" : filtros.FechaFinal;
            var lista = new List<VentasFamiliaModel>();
            parametros.Add(new SqlParameter { ParameterName = "@pFechaInicial", SqlDbType = SqlDbType.VarChar, Value = fecha_inicial });
            parametros.Add(new SqlParameter { ParameterName = "@pFechaFinal", SqlDbType = SqlDbType.VarChar, Value = fecha_final });
            parametros.Add(new SqlParameter { ParameterName = "@pIdSucursal", SqlDbType = SqlDbType.VarChar, Value = sucursal });
            try
            {
                DataSet ds = dac.Fill("GetVentasDepartamentoSucursal", parametros);
                if(ds.Tables.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        lista.Add(new VentasFamiliaModel
                        {
                            Nombre = dr["NombreDepto"].ToString(),
                            Cantidad = decimal.Parse(dr["Cantidad"].ToString()),
                            Venta = decimal.Parse(dr["Venta"].ToString()),
                            Costo = decimal.Parse(dr["Costo"].ToString()),
                            ClaveDepartamento = dr["ClaveDepto"].ToString()
                            

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return lista;
        }
        
    }
    

}