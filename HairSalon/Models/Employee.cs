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

    public Employee(string name, int id = 0)
    {
      _employeeName = name;
      _employeeId = id;
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
      return this.GetEmployeeName().GetHashCode();
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE from employees;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO employees (employee_name) VALUES (@employeeName);";

      cmd.Parameters.Add(new MySqlParameter("@employeeName", _employeeName));

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

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM employees;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Employee newEmployee = new Employee(name, id);
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
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM employees WHERE employee_Id = @employeeId;";

      cmd.Parameters.Add(new MySqlParameter("@employeeId", _employeeId));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int id = 0;
      string name = "";
      while (rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
      }
      Employee foundEmployee = new Employee(name, id);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return foundEmployee;
    }

    public List<Client> GetClients()
    {
      List<Client> storeClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE employee_Id = @employeeId;";

      cmd.Parameters.Add(new MySqlParameter("@employeeId", _employeeId));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int employeeId = rdr.GetInt32(2);
        storeClients.Add(new Client(name, employeeId, id));
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return storeClients;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"
      DELETE FROM employees WHERE employee_Id = @employeeId;
      DELETE FROM clients WHERE employee_Id = @employeeId;";

      cmd.Parameters.Add(new MySqlParameter("@employeeId", _employeeId));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
