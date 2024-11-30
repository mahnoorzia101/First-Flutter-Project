class Employee {
  int empno;
  String ename;
  String job;
  int? mgr;
  DateTime hiredate; // Use ISO 8601 formatted date strings
  double sal;
  double? comm;
  int deptno;

  Employee({
    required this.empno,
    required this.ename,
    required this.job,
    this.mgr,
    required this.hiredate,
    required this.sal,
    this.comm,
    required this.deptno,
  });

  // Factory constructor to create an Employee from JSON
  factory Employee.fromJson(Map<String, dynamic> json) {
    return Employee(
      empno: json['empno'],
      ename: json['ename'],
      job: json['job'],
      mgr: json['mgr'],
      hiredate: DateTime.parse(json['hiredate']),
      sal: json['sal'].toDouble(),
      comm: json['comm'],
      deptno: json['deptno'],
    );
  }

  // Method to convert Employee to JSON
  Map<String, dynamic> toJson() {
    return {
      'empno': empno,
      'ename': ename,
      'job': job,
      'mgr': mgr,
      'hiredate': hiredate.toIso8601String(),
      'sal': sal,
      'comm': comm,
      'deptno': deptno,
    };
  }
}
