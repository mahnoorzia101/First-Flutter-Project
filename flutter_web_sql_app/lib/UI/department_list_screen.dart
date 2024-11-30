import 'package:flutter/material.dart';
import '../business_logic/department_service.dart';
import '../data/department_model.dart';
import 'update_department_screen.dart';

class DepartmentListScreen extends StatefulWidget {
  @override
  _DepartmentListScreenState createState() => _DepartmentListScreenState();
}

class _DepartmentListScreenState extends State<DepartmentListScreen> {
  final DepartmentService departmentService = DepartmentService();
  late Future<List<Department>> departmentFuture;

  @override
  void initState() {
    super.initState();
    departmentFuture = departmentService.getDepartments();
  }

  void refreshList() {
    setState(() {
      departmentFuture = departmentService.getDepartments();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Departments'),
      ),
      body: FutureBuilder<List<Department>>(
        future: departmentFuture,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return Center(child: CircularProgressIndicator());
          } else if (snapshot.hasError) {
            return Center(child: Text('Error: ${snapshot.error}'));
          } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return Center(child: Text('No departments found'));
          } else {
            final departments = snapshot.data!;
            return ListView.builder(
              itemCount: departments.length,
              itemBuilder: (context, index) {
                final department = departments[index];
                return Card(
                  margin: EdgeInsets.symmetric(vertical: 8, horizontal: 16),
                  child: ListTile(
                    title: Text(department.dName),
                    subtitle: Text('Location: ${department.loc}'),
                    trailing: Row(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        IconButton(
                          icon: Icon(Icons.edit),
                          onPressed: () {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (context) =>
                                    UpdateDepartmentScreen(department: department),
                              ),
                            ).then((_) => refreshList());
                          },
                        ),
                        IconButton(
                          icon: Icon(Icons.delete),
                          onPressed: () async {
                            await departmentService.deleteDepartment(department.deptNo);
                            refreshList();
                          },
                        ),
                      ],
                    ),
                  ),
                );
              },
            );
          }
        },
      ),
    );
  }
}
