using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Employee
  {
    private int _employeeId;
    private string _employeeName;
    public Employee(string employeeName, int employeeId = 0)
    {
      _employeeName = employeeName;
      _employeeId = employeeId;
    }

    public int GetEmployeeId()
    {
      return _employeeId;
    }
    public string GetEmployeeName()
    {
      return _employeeName;
    }

    public override bool Equals(System.Object otherEmployee)
    {
      if(!(otherEmployee is Employee))
      {
        return false;
      }
      else
      {
        Employee newEmployee = (Employee) otherEmployee;
        bool areNamesEqual = this.GetEmployeeName().Equals(newEmployee.GetEmployeeName());
        bool areIdsEqual = this.GetEmployeeId().Equals(newEmployee.GetEmployeeId());
        return (areNamesEqual && areIdsEqual);
      }
    }

    public override int GetHashCode()
    {
      string AllHash = this.GetEmployeeName() + this.GetHashCode();
      return AllHash.GetHashCode();
    }


    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO employees (employee_name) VALUES (@employeeName);";

      MySqlParameter employeeName = new MySqlParameter();
      employeeName.ParameterName = "@employeeName";
      employeeName.Value = this._employeeName;
      cmd.Parameters.Add(employeeName);
      //
      cmd.ExecuteNonQuery();
      _employeeId = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Employee> GetAll()
    {
      List<Employee> allEmployee = new List<Employee> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM employees;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int employeeid = rdr.GetInt32(0);
        string employeename = rdr.GetString(1);
        Employee newEmployee = new Employee(employeename, employeeid);
        allEmployee.Add(newEmployee);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allEmployee;
    }

    public static Employee Find(int employeeId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM employees WHERE employee_Id = (@employeeId);";

      MySqlParameter searchflightId = new MySqlParameter();
       searchflightId.ParameterName = "@searchflightId";
       searchflightId.Value = employeeId;
       cmd.Parameters.Add(searchflightId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int employeeid = 0;
      string employeename = "";

      while (rdr.Read())
      {
        employeeid = rdr.GetInt32(0);
        employeename = rdr.GetString(1);
      }
      Employee foundEmployee = new Employee(employeename, employeeid);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundEmployee;
    }

    public List<Client> GetClients()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE employee_Id = @employeeId;";


      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int employeeId = rdr.GetInt32(2);
        allClients.Add(new Client(name, employeeId, id));
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return allClients;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"
      DELETE FROM employees WHERE employee_Id = @searchId;
      DELETE FROM clients WHERE clients_Id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = @"searchId";
      searchId.Value = _employeeId;
      cmd.Parameters.Add(searchId);;

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM employees;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
