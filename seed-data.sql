INSERT INTO Categories (Id, Name) VALUES (4, 'Books');
INSERT INTO Categories (Id, Name) VALUES (3, 'Clothing');
INSERT INTO Categories (Id, Name) VALUES (1, 'Electronics');
INSERT INTO Categories (Id, Name) VALUES (2, 'Food');
INSERT INTO Categories (Id, Name) VALUES (5, 'Health');
INSERT INTO Categories (Id, Name) VALUES (6, 'Home');
INSERT INTO Categories (Id, Name) VALUES (99, 'Other');
INSERT INTO Categories (Id, Name) VALUES (7, 'Sports');
INSERT INTO Categories (Id, Name) VALUES (0, 'Unknown');


INSERT INTO Products (Id, Name, CategoryId, Price, UnitsInStock, CreatedAt, UpdatedAt) VALUES ('FAD3FD7D-A216-4DC2-93BE-3960D6E625BC', 'Protein Powder', 1, 35.50, 18, '2026-02-15 19:18:24.173', '2026-02-16 22:51:45.879');
INSERT INTO Products (Id, Name, CategoryId, Price, UnitsInStock, CreatedAt, UpdatedAt) VALUES ('DE2248B6-BA45-48EC-98BA-7BB9EB4005C4', 'Vitamins C', 5, 35.00, 50, '2026-02-15 19:18:24.173', NULL);
INSERT INTO Products (Id, Name, CategoryId, Price, UnitsInStock, CreatedAt, UpdatedAt) VALUES ('B1A702AB-1D84-46D4-A721-9036F9FB4310', 'Chocolate Bar', 2, 8.50, 100, '2026-02-15 19:18:24.173', NULL);
INSERT INTO Products (Id, Name, CategoryId, Price, UnitsInStock, CreatedAt, UpdatedAt) VALUES ('E216B8B8-259E-4AA5-800E-B19C002EAB47', 'Yoga Mat', 7, 85.00, 35, '2026-02-15 19:18:24.173', NULL);
INSERT INTO Products (Id, Name, CategoryId, Price, UnitsInStock, CreatedAt, UpdatedAt) VALUES ('59F5CD2A-4B0F-414F-8DC2-BF5E5E5F1FFD', 'cvsV', 4, 435.00, 545, '2026-02-16 22:56:37.481', NULL);
INSERT INTO Products (Id, Name, CategoryId, Price, UnitsInStock, CreatedAt, UpdatedAt) VALUES ('74329CF2-2578-4102-84FE-BF73AFCCDFB9', 'Vacuum Cleaner', 6, 480.00, 8, '2026-02-15 19:18:24.173', NULL);
INSERT INTO Products (Id, Name, CategoryId, Price, UnitsInStock, CreatedAt, UpdatedAt) VALUES ('5352D1F5-ED4B-43A0-AE77-CBDAA69583CE', 'Desk Lamp', 6, 90.00, 30, '2026-02-15 19:18:24.173', NULL);
INSERT INTO Products (Id, Name, CategoryId, Price, UnitsInStock, CreatedAt, UpdatedAt) VALUES ('3B927040-3F28-4985-B1A3-CF3DE648D879', 'Pasta Pack', 2, 12.00, 60, '2026-02-15 19:18:24.173', NULL);
INSERT INTO Products (Id, Name, CategoryId, Price, UnitsInStock, CreatedAt, UpdatedAt) VALUES ('30394437-3726-4ACA-808B-D26F1E3A5013', 'Football', 7, 70.00, 22, '2026-02-15 19:18:24.173', NULL);
INSERT INTO Products (Id, Name, CategoryId, Price, UnitsInStock, CreatedAt, UpdatedAt) VALUES ('076D85DB-F6E2-4354-A12B-D2E546BB2CB9', 'Laptop', 1, 3500.00, 10, '2026-02-15 19:18:24.173', NULL);
INSERT INTO Products (Id, Name, CategoryId, Price, UnitsInStock, CreatedAt, UpdatedAt) VALUES ('5A69A73D-8B9E-44C9-B3D9-E5A3745A4EE8', 'Clean Code', 4, 95.00, 15, '2026-02-15 19:18:24.173', NULL);
INSERT INTO Products (Id, Name, CategoryId, Price, UnitsInStock, CreatedAt, UpdatedAt) VALUES ('2F4ECBC4-4111-422E-B988-ED720DCF1850', 'Design Patterns', 4, 110.00, 12, '2026-02-15 19:18:24.173', NULL);
INSERT INTO Products (Id, Name, CategoryId, Price, UnitsInStock, CreatedAt, UpdatedAt) VALUES ('91E21E35-DF98-496F-B0D8-F42D4CB6FA4E', 'Jeans', 3, 120.00, 20, '2026-02-15 19:18:24.173', NULL);


INSERT INTO Users (Id, UserName, PasswordHash, Role, CreatedAt, LastLogin) VALUES ('11111111-1111-1111-1111-111111111111', 'admin', 'AQAAAAIAAYagAAAAENjqXaLeRvrgaonfz1Ly6WIIaEYGAf83QOWc1Jfc9bfibjcwfwgLhk9mtxSADvLgeQ==', 1, '2026-02-15 18:25:14.473', NULL);
INSERT INTO Users (Id, UserName, PasswordHash, Role, CreatedAt, LastLogin) VALUES ('22222222-2222-2222-2222-222222222222', 'viewer', 'AQAAAAIAAYagAAAAECbGvSdG1E3FXNiCucUjGaXudoSrjJwMtjrXdLDXJHTsRaaWTZQYH4DXAJWsIEmrbA==', 0, '2026-02-15 18:25:14.473', NULL);