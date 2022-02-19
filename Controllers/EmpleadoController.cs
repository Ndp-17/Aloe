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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Aloe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmpleadoController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;

        }


        [HttpGet]
        public JsonResult Get()
        {

            string query = @"

                select NombreEmpleado
                       ,ApellidoEmpleado
                       ,Cedula
                       ,Fecha_de_nacimiento
                       ,Nombre_de_posición
                       ,DepartamentoId 
                 from  Empleado


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
        public JsonResult Post(Empleado dep)
        {
            string query = @"
                           insert into dbo.Empleado
                           values (@NombreEmpleado,@ApellidoEmpleado,@Cedula,@Fecha_de_nacimiento,@Nombre_de_posicion,@DepartamentoId)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ALOE");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NombreEmpleado", dep.NombreEmpleado);
                    myCommand.Parameters.AddWithValue("@ApellidoEmpleado", dep.ApellidoEmpleado);
                    myCommand.Parameters.AddWithValue("@Cedula", dep.Cedula);
                    myCommand.Parameters.AddWithValue("@Fecha_de_nacimiento", dep.Fecha_de_nacimiento);
                    myCommand.Parameters.AddWithValue("@Nombre_de_posicion", dep.Nombre_de_posicion??"");
                    myCommand.Parameters.AddWithValue("@DepartamentoId", dep.DepartamentoId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Agregado Correctamente");
        }

        [HttpPut]
        public JsonResult Put(Empleado dep)
        {
            string query = @"
                           update dbo.Empleado
                           set NombreEmpleado= @NombreEmpleado,
                            ApellidoEmpleado= @ApellidoEmpleado,
                            Cedula= @Cedula,
                            Fecha_de_nacimiento= @Fecha_de_nacimiento,
                            Nombre_de_posición = @Nombre_de_posicion,
                            DepartamentoId= @DepartamentoId
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
                    myCommand.Parameters.AddWithValue("@NombreEmpleado", dep.NombreEmpleado);
                    myCommand.Parameters.AddWithValue("@ApellidoEmpleado", dep.ApellidoEmpleado);
                    myCommand.Parameters.AddWithValue("@Cedula", dep.Cedula);
                    myCommand.Parameters.AddWithValue("@Fecha_de_nacimiento", dep.Fecha_de_nacimiento);
                    myCommand.Parameters.AddWithValue("@Nombre_de_posicion", dep.Nombre_de_posicion);
                    myCommand.Parameters.AddWithValue("@DepartamentoId", dep.DepartamentoId);
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
                           delete from dbo.Empleado
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
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Fotos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }
    }
}
