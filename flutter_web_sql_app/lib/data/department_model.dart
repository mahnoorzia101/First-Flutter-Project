class Department {
  int deptNo;
  String dName;
  String loc;

  Department({
    required this.deptNo,
    required this.dName,
    required this.loc,
  });

  factory Department.fromJson(Map<String, dynamic> json) {
    return Department(
      deptNo: json['deptNo'],
      dName: json['dName'],
      loc: json['loc'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'deptNo': deptNo,
      'dName': dName,
      'loc': loc,
    };
  }
}
