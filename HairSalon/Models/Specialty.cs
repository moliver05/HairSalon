using System;
using System.Collections.Generic;
using System.Collections;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Specialty
  {
      private string _name;
      private int _id;

      public Specialty(string name, int id = 0)
      {
        _name = name;
        _id = id;

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
          return this.GetId().Equals(newSpecialty.GetId());
        }

      }

      public override int GetHashCode()
      {
        return this.GetId().GetHashCode();
      }

      public string GetName()
      {
        return _name;
      }

      public int GetId()
      {
        return _id;
      }

      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO specialty (name) VALUES (@name);";

        MySqlParameter newName = new MySqlParameter();
        newName.ParameterName = "@name";
        newName.Value = this.GetName();
        cmd.Parameters.Add(newName);

        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }

      }

      public static List<Specialty> GetAll()
      {
        List<Specialty> allSpecialtys = new List<Specialty> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM specialty;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int SpecialtyId = rdr.GetInt32(0);
          string SpecialtyName = rdr.GetString(1);
          Specialty newSpecialty = new Specialty(SpecialtyName, SpecialtyId);
          allSpecialtys.Add(newSpecialty);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allSpecialtys;
      }

      public static Specialty Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM specialty WHERE id = (@searchId);";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int SpecialtyId = 0;
        string SpecialtyName = "";


        while(rdr.Read())
        {
          SpecialtyId = rdr.GetInt32(0);
          SpecialtyName = rdr.GetString(1);

        }
        Specialty newSpecialty = new Specialty(SpecialtyName, SpecialtyId);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newSpecialty;
      }

      public static void DeleteAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"TRUNCATE TABLE specialty;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public void Delete()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        MySqlCommand cmd = new MySqlCommand("DELETE FROM specialty WHERE id = @SpecialtyId; DELETE FROM specialty_employee WHERE specialty_id = @SpecialtyId;", conn);
        MySqlParameter specialtyIdParameter = new MySqlParameter();
        specialtyIdParameter.ParameterName = "@SpecialtyId";
        specialtyIdParameter.Value = this.GetId();

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
        cmd.CommandText = @"UPDATE specialty SET name = @newName WHERE id = @searchId;";

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

      public void UpdateSpecialty(string newSpecialty)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE specialty SET name = @newSpecialty WHERE id = @searchId;";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);

        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@newSpecialty";
        name.Value = newSpecialty;
        cmd.Parameters.Add(name);

        cmd.ExecuteNonQuery();
        _name = newSpecialty;
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
        cmd.CommandText = @"INSERT INTO specialty_employee (specialty_id, employee_id) VALUES (@SpecialtyId, @EmployeeId);";

        MySqlParameter specialtyid = new MySqlParameter();
        specialtyid.ParameterName = "@SpecialtyId";
        specialtyid.Value = _id;
        cmd.Parameters.Add(specialtyid);

        MySqlParameter employee_id = new MySqlParameter();
        employee_id.ParameterName = "@EmployeeId";
        employee_id.Value = newEmployee.GetId();
        cmd.Parameters.Add(employee_id);

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
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT specialty.* FROM employee
        JOIN specialty_employee ON (specialty.id = specialty_employee.specialty_id)
        JOIN employee ON (specialty_employee.employee_id = employee.id)
        WHERE specialty.id = @SpecialtyId;";

        MySqlParameter employeeParameter = new MySqlParameter();
        employeeParameter.ParameterName = "@SpecialtyId";
        employeeParameter.Value = _id;
        cmd.Parameters.Add(employeeParameter);

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        List<Employee> Employee = new List<Employee>{};

        while(rdr.Read())
        {
          int employeeId = rdr.GetInt32(0);
          string employeeName = rdr.GetString(1);

          Employee newEmployee = new Employee(employeeName, employeeId);
          Employee.Add(newEmployee);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return Employee;
      }



    }
  }
