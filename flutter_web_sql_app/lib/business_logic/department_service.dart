import 'dart:convert';
import 'package:http/http.dart' as http;
import '../data/department_model.dart';

class DepartmentService {
  final String apiUrl = 'https://localhost:5000/api/Department';

  Future<List<Department>> getDepartments() async {
    final response = await http.get(Uri.parse('$apiUrl/getDepartments'));
    if (response.statusCode == 200) {
      final List<dynamic> departmentJson = jsonDecode(response.body);
      return departmentJson.map((json) => Department.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load departments');
    }
  }

    Future<void> updateDepartment(Department department) async {
    final response = await http.put(
      Uri.parse('http://localhost:5000/api/Department/updateDepartment'),
      headers: {"Content-Type": "application/json"},
      body: json.encode(department.toJson()),
    );

    if (response.statusCode == 200) {
      print('Department updated successfully');
    } else {
      throw Exception('Failed to update department');
    }
  }

  Future<void> addDepartment(Department department) async {
    final response = await http.post(
      Uri.parse('$apiUrl/addDepartment'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(department.toJson()),
    );

    if (response.statusCode != 201) {
      throw Exception('Failed to add department');
    }
  }

  Future<void> deleteDepartment(int deptNo) async {
    final response = await http.delete(
      Uri.parse('http://localhost:5000/api/Department/deleteDepartment/$deptNo'),
    );

    if (response.statusCode == 200) {
      print('Department deleted successfully');
    } else {
      throw Exception('Failed to delete department');
    }
  }
}


