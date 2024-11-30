import 'dart:convert';
import 'package:http/http.dart' as http;
import '../data/employee_model.dart';

class EmployeeService {
  final String apiUrl = "https://localhost:5000/api/Employee";

  Future<void> fetchEmployees() async {
  try {
    final response = await http.get(Uri.parse('http://localhost:5000/api/Employee/getEmployee'));
    if (response.statusCode == 200) {
      // Handle successful response
      print(response.body);
    } else {
      print('Error: ${response.statusCode}');
    }
  } catch (e) {
    print('Exception: $e');
  }
}

  Future<List<Employee>> getEmployees() async {
    final response = await http.get(Uri.parse('https://localhost:5000/api/Employee/getEmployee'));
    if (response.statusCode == 200) {
      List<dynamic> body = jsonDecode(response.body);
      return body.map((e) => Employee.fromJson(e)).toList();
    } else {
      throw Exception('Failed to load employees');
    }
  }

  Future<void> updateEmployee(Employee employee) async {
    final response = await http.put(
      Uri.parse('http://localhost:5000/api/Employee/updateEmployee'),
      headers: {"Content-Type": "application/json"},
      body: json.encode(employee.toJson()),
    );

    if (response.statusCode == 200) {
      print('Employee updated successfully');
    } else {
      throw Exception('Failed to update employee');
    }
  }
  Future<void> deleteEmployee(int empno) async {
    final response = await http.delete(
      Uri.parse('http://localhost:5000/api/Employee/deleteEmployee/$empno'),
    );

    if (response.statusCode == 200) {
      print('Employee deleted successfully');
    } else {
      throw Exception('Failed to delete employee');
    }
  }
}
