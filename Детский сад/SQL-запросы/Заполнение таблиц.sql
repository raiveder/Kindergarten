use [41�_���01.01_�������]

INSERT INTO Genders
VALUES ('�������'),
('�������')


INSERT INTO Roles
VALUES ('�������������'),
('������������')


INSERT INTO Types_group
VALUES ('��������'),
('�������'),
('�������'),
('�������'),
('����������������')


INSERT INTO Groups
VALUES (1, 1),
(1, 2),
(2, 1),
(2, 2),
(3, 1),
(3, 2),
(4, 1),
(4, 2),
(5, 1),
(5, 2)


INSERT INTO Positions
VALUES ('����������'),
('����������� �����������'),
('������� �����������'),
('�����������'),
('�������� �����������')


INSERT INTO Children
VALUES ('�������', '�������','���������', '2020-08-08', 1, 1, '�����������', '14'),
('�������', '�������','����������', '2020-06-06', 1, 1, '��������', '16'),
('�������', '���������','����������', '2020-07-07', 2, 1, '������', '25'),
('��������', '�����','���������', '2020-05-22', 2, 1, '������', '2'),
('��������', '�������','���������', '2020-05-22', 2, 1, '������', '2'),
('�������', '������','���������', '2019-04-15', 1, 2, '�����������', '14'),
('���������', '�������','���������', '2018-08-08', 1, 3, '������������', '29')


INSERT INTO Sertificates
VALUES (1, 'I-��', 111382, '2020-09-01', '���������� ����� ���� ������������� ���.'),
(2,'IV-��', 121482, '2020-06-20', '����� ���� ������������� ������ �. ������� ���������'),
(3,'IV-���', 125482, '2020-07-30', '����� ���� �������� ������ ������������� ���.'),
(4,'XI-���', 291082, '2020-06-22', '����� ���� �������� ������ ������������� ���.'),
(5,'XI-���', 291083, '2020-06-22', '����� ���� �������� ������ ������������� ���.'),
(6,'XIV-���', 603603, '2019-05-12', '���������� ����� ���� ������������� ���.'),
(7,'XIV-���', 603603, '2018-08-31', '�������������� ����� ���� �. ������')


INSERT INTO Parents
VALUES ('��������', '�������','����������', '1995-11-14', 2, '�����������', '14', '+7 920 444-87-85'),
('�������', '������','�����������', '1995-01-22', 1, '�����������', '14', '+7 915 222-43-22'),
('�������', '�������','��������������', '1990-09-02', 1, '��������', '16', '+7 933 124-64-25'),
('���������', '�����','���������', '1998-10-30', 2, '������', '25', '+7 945 087-87-23'),
('������', '�������','����������', '1996-02-02', 1, '������', '25', '+7 980 324-98-13'),
('��������', '����','�������������', '2000-07-15', 2, '������', '2', '+7 990 142-18-16'),
('�������', '������','�����������', '1999-12-19', 1, '������', '2', '+7 904 156-35-45'),
('���������', '�����','��������', '1992-03-25', 2, '������������', '29', '+7 987 742-22-00'),
('��������', '������','��������', '1990-10-28', 1, '����������', '12', '+7 922 356-18-09')


INSERT INTO Kinships
VALUES (1, 1),
(2, 1),
(1, 6),
(2, 6),
(3, 2),
(4, 3),
(5, 3),
(6, 4),
(7, 4),
(6, 5),
(7, 5),
(8, 7),
(9, 7)