import 'dart:convert';
import 'package:http/http.dart' as http;
import 'employee_model.dart';

class ApiService {
  final String baseUrl = 'https://localhost:5000/api/Employee';

  Future<List<Employee>> fetchEmployees() async {
    final response = await http.get(Uri.parse('https://localhost:5000/api/Employee/getEmployee'));

    if (response.statusCode == 200) {
      List<dynamic> data = json.decode(response.body);
      return data.map((json) => Employee.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load employees');
    }
  }
}
