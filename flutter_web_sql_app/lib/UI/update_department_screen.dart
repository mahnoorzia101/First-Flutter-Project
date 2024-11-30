import 'package:flutter/material.dart';
import '../data/department_model.dart';
import '../business_logic/department_service.dart';

class UpdateDepartmentScreen extends StatelessWidget {
  final Department department;
  final DepartmentService departmentService = DepartmentService();

  UpdateDepartmentScreen({required this.department});

  final TextEditingController _dnameController = TextEditingController();
  final TextEditingController _locController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    // Pre-fill the text fields with the existing department data
    _dnameController.text = department.dName;
    _locController.text = department.loc;

    return Scaffold(
      appBar: AppBar(
        title: Text('Update Department'),
      ),
      body: Padding(
        padding: EdgeInsets.all(16.0),
        child: Column(
          children: [
            TextField(
              controller: _dnameController,
              decoration: InputDecoration(labelText: 'Department Name'),
            ),
            SizedBox(height: 10),
            TextField(
              controller: _locController,
              decoration: InputDecoration(labelText: 'Location'),
            ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: () async {
                try {
                  // Update the department object with the new data
                  department.dName = _dnameController.text;
                  department.loc = _locController.text;

                  // Call the service to update the department
                  await departmentService.updateDepartment(department);

                  // Show success message
                  ScaffoldMessenger.of(context).showSnackBar(
                    SnackBar(content: Text('Department updated successfully!')),
                  );

                  // Navigate back to the previous screen
                  Navigator.pop(context);
                } catch (error) {
                  // Show error message
                  ScaffoldMessenger.of(context).showSnackBar(
                    SnackBar(content: Text('Error updating department: $error')),
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
