using System;
using System.Collections.Generic;
using System.Collections;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Employee
  {
    private int _id;
    private string _name;

    public Employee(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }
    public override bool Equals(System.Object otherEmployee)
    {
      if (!(otherEmployee is Employee))
      {
        return false;
      }
      else
      {
        Employee newEmployee = (Employee) otherEmployee;
        bool idEquality = (this.GetId() == newEmployee.GetId());
        bool nameEquality = (this.GetName() == newEmployee.GetName());
        return (idEquality && nameEquality);
      }
    }
    public string GetName()
    {
      return _name;
    }
    // public void SetName(string newName)
    // {
    //   _name = newName;
    // }

    public int GetId()
    {
      return _id;
    }

    public static List<Employee> GetAll()
    {
      List<Employee> allEmployee = new List<Employee> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM employees;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int employeeId = rdr.GetInt32(0);
        string employeeName = rdr.GetString(1);
        Employee newEmployee = new Employee(employeeName, employeeId);
        allEmployee.Add(newEmployee);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allEmployee;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO employees (name) VALUES (@EmployeeName);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@EmployeeName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      //
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

     MySqlCommand cmd = new MySqlCommand("DELETE FROM employees WHERE id = @StylistId; DELETE FROM employees_specialties WHERE stylist_id = @StylistId;", conn);
     MySqlParameter employeeIdParameter = new MySqlParameter();
     employeeIdParameter.ParameterName = "@EmployeeId";
     employeeIdParameter.Value = this.GetId();

     cmd.Parameters.Add(employeeIdParameter);
     cmd.ExecuteNonQuery();

     if (conn != null)
     {
       conn.Close();
     }
   }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE employees;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Employee Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `employees` WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int employeeId = 0;
      string employeeName = "";

      while (rdr.Read())
      {
        employeeId = rdr.GetInt32(0);
        employeeName = rdr.GetString(1);
      }
      Employee foundEmployee = new Employee(employeeName, employeeId);


      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundEmployee;
    }

    public List<Client> GetClients()
    {
      List<Client> allEmployeeClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE employeeId = @employeeId;";

      MySqlParameter employeeId = new MySqlParameter();
      employeeId.ParameterName = "@employeeId";
      employeeId.Value = this._id;
      cmd.Parameters.Add(employeeId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientEmployeeId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientEmployeeId, clientId);
        allEmployeeClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allEmployeeClients;
    }

    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE employees SET name = @newName WHERE id = @searchId;";

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
