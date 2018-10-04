using System;
using System.Collections.Generic;
using HairSalon;
using MySql.Data.MySqlClient;
using System.Collections;

namespace HairSalon.Models
{
  public class Specialty
  {
    private int _id;
    private string _name;
    public Specialty(string name, int Id = 0)
    {
      _id = Id;
      _name = name;

    }
    public string GetSpecialtyName()
    {
      return _name;
    }

    public int GetSpecialtyId()
    {
      return _id;
    }

    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = (this.GetSpecialtyId() == newSpecialty.GetSpecialtyId());
        bool nameEquality = (this.GetSpecialtyName() == newSpecialty.GetSpecialtyName());

        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetSpecialtyName().GetHashCode();
    }


    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialties = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties;"; //select from specialties
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int specialtyId = rdr.GetInt32(0);
        string specialtyName = rdr.GetString(1);


        Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
        allSpecialties.Add(newSpecialty);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialties (specialty_name) VALUES (@SpecialtyName);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@SpecialtyName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static Specialty Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `specialties` WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int specialtyId = 0;
      string specialtyName = "";


      while (rdr.Read())
      {
        specialtyId = rdr.GetInt32(0);
        specialtyName = rdr.GetString(1);


      }
      Specialty foundSpecialty = new Specialty(specialtyName, specialtyId);

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundSpecialty;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE specialties";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddEmployee(Employee newEmployee)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO employees_specialties (employee_id, specialty_id) VALUES (@EmployeeId, @SpecialtyId);";

      MySqlParameter employee_id = new MySqlParameter();
      employee_id.ParameterName = "@EmployeeId";
      employee_id.Value = newEmployee.GetId();
      cmd.Parameters.Add(employee_id);

      MySqlParameter specialty_id = new MySqlParameter();
      specialty_id.ParameterName = "@SpecialtyId";
      specialty_id.Value = _id;
      cmd.Parameters.Add(specialty_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Employee> GetEmployee()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT employee_id FROM employees_specialties WHERE specialty_id = @specialtyId;";

      MySqlParameter specialtyIdParameter = new MySqlParameter();
      specialtyIdParameter.ParameterName = "@specialtyId";
      specialtyIdParameter.Value = _id;
      cmd.Parameters.Add(specialtyIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      List<int> employeeIds = new List<int> {};
      while(rdr.Read())
      {
        int employeeId = rdr.GetInt32(0);
        employeeIds.Add(employeeId);
      }
      rdr.Dispose();

      List<Employee> employee = new List<Employee> {};
      foreach (int employeeId in employeeIds)
      {
        var employeeQuery = conn.CreateCommand() as MySqlCommand;
        employeeQuery.CommandText = @"SELECT * FROM employees WHERE id = @EmployeeId;";

        MySqlParameter employeeIdParameter = new MySqlParameter();
        employeeIdParameter.ParameterName = "@EmployeeId";
        employeeIdParameter.Value = employeeId;
        employeeQuery.Parameters.Add(employeeIdParameter);

        var employeeQueryRdr = employeeQuery.ExecuteReader() as MySqlDataReader;
        while(employeeQueryRdr.Read())
        {
          int thisEmployeeId = employeeQueryRdr.GetInt32(0);
          string employeeName = employeeQueryRdr.GetString(1);

          Employee foundEmployee = new Employee(employeeName, thisEmployeeId);
          employee.Add(foundEmployee);
        }
        employeeQueryRdr.Dispose();
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return employee;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = new MySqlCommand("DELETE FROM specialties WHERE id = @SpecialtyId; DELETE FROM employees_specialties WHERE specialty_id = @SpecialtyId;", conn);
      MySqlParameter specialtyIdParameter = new MySqlParameter();
      specialtyIdParameter.ParameterName = "@SpecialtyId";
      specialtyIdParameter.Value = this.GetSpecialtyId();

      cmd.Parameters.Add(specialtyIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE specialties SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);


      cmd.ExecuteNonQuery();
      _name = newName;


      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
    }
    
  }
}
