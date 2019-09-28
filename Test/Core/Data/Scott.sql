CREATE TABLE DEPT
       (DEPTNO INT CONSTRAINT PK_DEPT PRIMARY KEY,
        DNAME VARCHAR(14),
        LOC VARCHAR(13) );

CREATE TABLE EMP
       (EMPNO INT CONSTRAINT PK_EMP PRIMARY KEY,
        ENAME VARCHAR(10),
        JOB VARCHAR(9),
        MGR INT,
        HIREDATE DATETIME,
        SAL NUMERIC,
        COMM INT,
        DEPTNO INT CONSTRAINT FK_DEPTNO REFERENCES DEPT);

INSERT INTO DEPT VALUES (10,'ACCOUNTING','NEW YORK');
INSERT INTO DEPT VALUES (20,'RESEARCH','DALLAS');
INSERT INTO DEPT VALUES (30,'SALES','CHICAGO');
INSERT INTO DEPT VALUES (40,'OPERATIONS','BOSTON');

INSERT INTO EMP VALUES  (7369,'SMITH','CLERK',7902,'12-17-1980',800,NULL,20);
INSERT INTO EMP VALUES  (7499,'ALLEN','SALESMAN',7698,'2-20-1981',1600,300,30);
INSERT INTO EMP VALUES  (7521,'WARD','SALESMAN',7698,'2-22-1981',1250,500,30);
INSERT INTO EMP VALUES  (7566,'JONES','MANAGER',7839,'4-2-1981',2975,NULL,20);
INSERT INTO EMP VALUES  (7654,'MARTIN','SALESMAN',7698,'9-28-1981',1250,1400,30);
INSERT INTO EMP VALUES  (7698,'BLAKE','MANAGER',7839,'5-1-1981',2850,NULL,30);
INSERT INTO EMP VALUES  (7782,'CLARK','MANAGER',7839,'6-9-1981',2450,NULL,10);
INSERT INTO EMP VALUES  (7788,'SCOTT','ANALYST',7566,'07-13-87',3000,NULL,20);
INSERT INTO EMP VALUES  (7839,'KING','PRESIDENT',NULL,'11-17-1981',5000,NULL,10);
INSERT INTO EMP VALUES  (7844,'TURNER','SALESMAN',7698,'9-8-1981',1500,0,30);
INSERT INTO EMP VALUES  (7876,'ADAMS','CLERK',7788,'07-13-87',1100,NULL,20);
INSERT INTO EMP VALUES  (7900,'JAMES','CLERK',7698,'12-3-1981',950,NULL,30);
INSERT INTO EMP VALUES  (7902,'FORD','ANALYST',7566,'12-3-1981',3000,NULL,20);
INSERT INTO EMP VALUES  (7934,'MILLER','CLERK',7782,'1-23-1982',1300,NULL,10);

CREATE TABLE BONUS
        (
        ENAME VARCHAR(10),
        JOB VARCHAR(9),
        SAL INT,
        COMM INT
        );
        
CREATE TABLE SALGRADE
      ( GRADE INT,
        LOSAL INT,
        HISAL INT
      );

INSERT INTO SALGRADE VALUES (1,700,1200);
INSERT INTO SALGRADE VALUES (2,1201,1400);
INSERT INTO SALGRADE VALUES (3,1401,2000);
INSERT INTO SALGRADE VALUES (4,2001,3000);
INSERT INTO SALGRADE VALUES (5,3001,9999);

