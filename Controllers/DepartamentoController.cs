using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Aloe.Models;

namespace Aloe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {


        private readonly IConfiguration _configuration;

        public DepartamentoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {

            string query = @"

                select NombreDepartamento,DescripcionDepartamento from Departamento


            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ALOE");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);



        }
        [HttpPost]
        public JsonResult Post(Departamento dep)
        {
            string query = @"
                           insert into dbo.Departamento
                           values (@NombreDepartamento,@DescripcionDepartamento)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ALOE");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NombreDepartamento", dep.NombreDepartamento);
                    myCommand.Parameters.AddWithValue("@DescripcionDepartamento", dep.DescripcionDepartamento);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Agregado Correctamente");
        }

        [HttpPut]
        public JsonResult Put(Departamento dep)
        {
            string query = @"
                           update dbo.Departamento
                           set NombreDepartamento= @NombreDepartamento,
                            DescripcionDepartamento= @DescripcionDepartamento
                            where Id=@Id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ALOE");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", dep.Id);
                    myCommand.Parameters.AddWithValue("@NombreDepartamento", dep.NombreDepartamento);
                    myCommand.Parameters.AddWithValue("@DescripcionDepartamento", dep.DescripcionDepartamento);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Actualizacion Correcta");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Departamento
                            where Id=@Id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ALOE");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Eliminacion Correcta");
        }






    }
}
