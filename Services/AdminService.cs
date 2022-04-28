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


        
    }
    

}