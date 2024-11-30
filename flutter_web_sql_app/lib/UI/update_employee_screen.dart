import 'package:flutter/material.dart';
import '../data/employee_model.dart';
import '../business_logic/employee_service.dart';

class UpdateEmployeeScreen extends StatelessWidget {
  final Employee employee;
  final EmployeeService employeeService = EmployeeService();

  UpdateEmployeeScreen({required this.employee});

  final TextEditingController _enameController = TextEditingController();
  final TextEditingController _jobController = TextEditingController();
  final TextEditingController _salController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    // Pre-fill the text fields with the existing employee data
    _enameController.text = employee.ename;
    _jobController.text = employee.job;
    _salController.text = employee.sal.toString();

    return Scaffold(
      appBar: AppBar(
        title: Text('Update Employee'),
      ),
      body: Padding(
        padding: EdgeInsets.all(16.0),
        child: Column(
          children: [
            TextField(
              controller: _enameController,
              decoration: InputDecoration(labelText: 'Name'),
            ),
            SizedBox(height: 10),
            TextField(
              controller: _jobController,
              decoration: InputDecoration(labelText: 'Job'),
            ),
            SizedBox(height: 10),
            TextField(
              controller: _salController,
              decoration: InputDecoration(labelText: 'Salary'),
              keyboardType: TextInputType.number,
            ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: () async {
                try {
                  // Update the employee object with the new data
                  employee.ename = _enameController.text;
                  employee.job = _jobController.text;
                  employee.sal = double.tryParse(_salController.text) ?? employee.sal;

                  // Call the service to update the employee
                  await employeeService.updateEmployee(employee);

                  // Show success message
                  ScaffoldMessenger.of(context).showSnackBar(
                    SnackBar(content: Text('Employee updated successfully!')),
                  );

                  // Navigate back to the previous screen
                  Navigator.pop(context);
                } catch (error) {
                  // Show error message
                  ScaffoldMessenger.of(context).showSnackBar(
                    SnackBar(content: Text('Error updating employee: $error')),
                  );
                }
              },
              child: Text('Update'),
            ),
          ],
        ),
      ),
    );
  }
}
