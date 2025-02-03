
 
use contact;
CREATE TABLE Huyen (
	IDHuyen INT PRIMARY KEY,
	TenHuyen NVARCHAR(100)
	);

INSERT INTO Huyen (IDHuyen, TenHuyen) VALUES
(0, 'Khong thuoc huyen'),
(1, 'Thanh pho Ninh Binh'),
(2, 'Thanh pho Tam Diep'),
(3, 'Huyen Gia Vien'),
(4, 'Huyen Hoa Lu'),
(5, 'Huyen Kim Son'),
(6, 'Huyen Nho Quan'),
(7, 'Huyen Yen Khanh'),
(8, 'Huyen Yen Mo');

CREATE TABLE Xa (
	IDXa INT PRIMARY KEY,
	TenXa NVARCHAR(50) NOT NULL,
	TrucThuocHuyen INT NOT NULL,
	FOREIGN KEY (TrucThuocHuyen) REFERENCES Huyen(IDHuyen)
	);

INSERT INTO Xa (IDXa, TenXa, TrucThuocHuyen) VALUES
(0, 'Khong thuoc xa', 0),
-- Ninh Binh
(101, 'Dong Thanh', 1),
(102, 'Nam Binh', 1),
(103, 'Nam Thanh', 1),
(104, 'Ninh Khanh', 1),
(105, 'Ninh Phong', 1),
(106, 'Tan Thanh - Thanh pho Ninh Binh', 1),
(107, 'Van Giang', 1),
(108, 'Bich Dao', 1),
(109, 'Dong Huong - Thanh pho Ninh Binh', 1),
(110, 'Ninh Nhat', 1),
(111, 'Ninh Tien', 1),
(112, 'Ninh Phuc', 1),

-- Tam Diep
(201, 'Bac Son', 2),
(202, 'Nam Son', 2),
(203, 'Trung Son', 2),
(204, 'Tan Binh', 2),
(205, 'Tan Thanh - Thanh pho Tam Diep', 2),
(206, 'Yen Binh', 2),
(207, 'Quang Son', 2),
(208, 'Yen Son', 2),
(209, 'Dong Son', 2),

-- Gia Vien
(301, 'Me', 3),
(302, 'Gia Hoa', 3),
(303, 'Gia Hung', 3),
(304, 'Gia Minh', 3),
(305, 'Gia Phong', 3),
(306, 'Gia Sinh', 3),
(307, 'Gia Tan', 3),
(308, 'Gia Thang', 3),
(309, 'Gia Thanh', 3),
(310, 'Gia Tien', 3),
(311, 'Gia Tran', 3),
(312, 'Gia Trung', 3),
(313, 'Gia Van', 3),
(314, 'Gia Vuong', 3),
(315, 'Gia Xuan', 3),
(316, 'Gia Lap', 3),
(317, 'Gia Lac', 3),
(318, 'Gia Thinh', 3),
(319, 'Gia Son - Gia Vien', 3),
(320, 'Gia Loc', 3),

-- Hoa Lu
(401, 'Thien Ton', 4),
(402, 'Ninh An', 4),
(403, 'Ninh Giang', 4),
(404, 'Ninh Hai', 4),
(405, 'Ninh Hoa', 4),
(406, 'Ninh Khang', 4),
(407, 'Ninh My', 4),
(408, 'Ninh Van', 4),
(409, 'Truong Yen', 4),

-- Kim Son
(501, 'Phat Diem', 5),
(502, 'An Hoa', 5),
(503, 'Binh Minh', 5),
(504, 'Chat Binh', 5),
(505, 'Con Thoi', 5),
(506, 'Dinh Hoa', 5),
(507, 'Dong Huong - Huyen Kim Son', 5),
(508, 'Hoi Ninh', 5),
(509, 'Hung Tien', 5),
(510, 'Kim Chinh', 5),
(511, 'Kim Dong', 5),
(512, 'Kim Hai', 5),
(513, 'Kim My', 5),
(514, 'Kim Tan', 5),
(515, 'Kim Trung', 5),
(516, 'Lai Thanh', 5),
(517, 'Luu Phuong', 5),
(518, 'Nhu Hoa', 5),
(519, 'Quang Thien', 5),
(520, 'Tan Thanh - Huyen Kim Son', 5),
(521, 'Thuong Kiem', 5),
(522, 'Van Hai', 5),
(523, 'Xuan Chinh', 5),
(524, 'Yen Loc - Huyen Kim Son', 5),

-- Nho Quan
(601, 'Nho Quan', 6),
(602, 'Cuc Phuong', 6),
(603, 'Dong Phong', 6),
(604, 'Duc Long', 6),
(605, 'Gia Lam', 6),
(606, 'Gia Son - Huyen Nho Quan', 6),
(607, 'Gia Thuy', 6),
(608, 'Gia Tuong', 6),
(609, 'Ky Phu', 6),
(610, 'Lac Van', 6),
(611, 'Lang Phong', 6),
(612, 'Phu Long', 6),
(613, 'Phu Son', 6),
(614, 'Quang Lac', 6),
(615, 'Quynh Luu', 6),
(616, 'Son Ha', 6),
(617, 'Son Lai', 6),
(618, 'Son Thanh', 6),
(619, 'Thach Binh', 6),
(620, 'Thanh Lac', 6),
(621, 'Thuong Hoa', 6),
(622, 'Van Phong', 6),
(623, 'Van Phu', 6),
(624, 'Xich Tho', 6),
(625, 'Yen Quang', 6),
(626, 'Yen Thanh - Huyen Nho Quan', 6),

-- Yen Khanh
(701, 'Yen Ninh', 7),
(702, 'Khanh An', 7),
(703, 'Khanh Cuong', 7),
(704, 'Khanh Hai', 7),
(705, 'Khanh Hoa', 7),
(706, 'Khanh Hoi', 7),
(707, 'Khanh Loi', 7),
(708, 'Khanh Mau', 7),
(709, 'Khanh Nhac', 7),
(710, 'Khanh Phu', 7),
(711, 'Khanh Thien', 7),
(712, 'Khanh Thuy', 7),
(713, 'Khanh Tien', 7),
(714, 'Khanh Trung', 7),
(715, 'Khanh Van', 7),
(716, 'Khanh Thanh', 7),
(717, 'Khanh Cong', 7),
(718, 'Khanh Cu', 7),

-- Yen Mo
(801, 'Yen Thinh', 8),
(802, 'Khanh Duong', 8),
(803, 'Mai Son', 8),
(804, 'Yen Dong', 8),
(805, 'Yen Hoa', 8),
(806, 'Yen Hung', 8),
(807, 'Yen Lam', 8),
(808, 'Yen Loc - Huyen Yen Mo', 8),
(809, 'Yen My', 8),
(810, 'Yen Mac', 8),
(811, 'Yen Nhan', 8),
(812, 'Yen Phong', 8),
(813, 'Yen Thang', 8),
(814, 'Yen Tu', 8),
(815, 'Yen Thanh - Huyen Yen Mo', 8),
(816, 'Yen Thai', 8);

CREATE TABLE AdministratorLevel (
	LevelID INT PRIMARY KEY,
	LevelName NVARCHAR(10)
	);

INSERT INTO AdministratorLevel (LevelID, LevelName) VALUES
(1, N'Huyen'),
(2, N'Xa');

CREATE TABLE CanBoNghiepVu (
	CanBoNghiepVuID INT PRIMARY KEY,
	CanBoNghiepVuName NVARCHAR(200),
	SDT NVARCHAR(20),
	Username NVARCHAR(200),
	Password NVARCHAR(200),
	AdministratorLevel INT,
	IDXa INT,
	IDHuyen INT,
	RecoveryCode1 NVARCHAR(10),
	RecoveryCode2 NVARCHAR(10),
	RecoveryCode3 NVARCHAR(10),
	LoginTime DATETIME,
	LogoutTime DATETIME,
	isAdmin BIT,
	FOREIGN KEY (AdministratorLevel) REFERENCES AdministratorLevel(LevelID),
	FOREIGN KEY (IDXa) REFERENCES Xa(IDXa),
	FOREIGN KEY (IDHuyen) REFERENCES Huyen(IDHuyen)
	);
create table LinhVuc (
  IDLinhVuc int primary key not null,
  TenLinhVuc Nvarchar(100),
  )
create table GiongVatNuoi(
  IDGiongVatNuoi int primary key not null,
  TenGiongVatNuoi nvarchar(100),
  )
create table LoaiConVat(
  IDLoaiConVat int primary key not null,
  TenConVat Nvarchar(100),
  TrangThaiConVat nvarchar(100)
  )
create table NguonGenGiong(
  IDNguonGenGiong int primary key not null,
  TenNguonGenGiong nvarchar(100),
  )
create table ThucAnChanNuoi(
  IDThucAnChanNuoi int primary key not null,
  TenThucAnChanNuoi nvarchar(100),
  )
  create table TrangThaiNguyenLieu(
   IDTrangThaiNguyenLieu int primary key not null,
   TenTrangThaiNguyenLieu nvarchar(50),
   )
  create table NguyenLieu(
   IDNguyenLieu int primary key not null,
   TenNguyenLieu nvarchar(50),
   IDTrangThaiNguyenLieu int ,
   foreign key (IDTrangThaiNguyenLieu) references TrangThaiNguyenLieu(IDTrangThaiNguyenLieu),
   )
create table CongTy (
  IDCongTy int primary key not null,
  Ten Nvarchar(50) not null,
  CapTrucThuoc int not null,
  Username nvarchar(100) not null,
  Password nvarchar(100) not null,
  IDXa int,
  foreign key (IDXa) references Xa(IDXa),
  IDHuyen int,
  foreign key (IDHuyen) references Huyen(IDHuyen),
  SDT Nvarchar(50),
  IDLinhVuc int,
  foreign key (IDLinhVuc) references LinhVuc(IDLinhVuc),
  IDGiongVatNuoi int,
  foreign key (IDGiongVatNuoi) references GiongVatNuoi(IDGiongVatNuoi),
  IDLoaiConVat int,
  foreign key (IDLoaiConVat) references LoaiConVat(IDLoaiConVat),
  SoLuong int,
  IDNguonGenGiong int,
  foreign key (IDNguonGenGiong) references NguonGenGiong(IDNguonGenGiong),
  IDThucAnChanNuoi int,
  foreign key (IDThucAnChanNuoi) references ThucAnChanNuoi(IDThucAnChanNuoi), 
  IDNguyenLieu int,
  foreign key (IDNguyenLieu) references NguyenLieu(IDNguyenLieu),
  LoginTime datetime,
  LogoutTime datetime,
  Banned bit,
  )


insert into GiongVatNuoi(IDGiongVatNuoi,TenGiongVatNuoi) values
     (0, 'KhongThuocLinhVuc'),
	 (1, 'SanXuat'),
	 (2, 'SanXuatTinhPhoiAuTrung'),
	 (3, 'SoHuuTrauBoDuc_Lon'),
	 (4, 'MuaBan'),
	 (5, 'KhaoNghiem')
insert into LinhVuc(IDLinhVuc,TenLinhVuc) values
         (1, 'GiongVatNuoi'),
		 (2, 'NguonGenGiong'),
		 (3, 'ThucAnChanNuoi');
insert into NguonGenGiong(IDNguonGenGiong,TenNguonGenGiong) values
         (1,'ThuThap'),
		 (2,'BaoTon'),
		 (3,'KhaiThac_PhatTrien'),
		 (0,'KhongThuocLinhVuc');
insert into ThucAnChanNuoi(IDThucAnChanNuoi,TenThucAnChanNuoi)values
         (0, 'KhongThuocLinhVuc'),
		 (1, 'SanXuat'),
		 (2, 'SanXuat_CoGiayChungNhan'),
		 (3, 'MuaBan'),
		 (4, 'KhaoNghiem');
insert into TrangThaiNguyenLieu(IDTrangThaiNguyenLieu,TenTrangThaiNguyenLieu)values 
    (1, 'CamSuDung'),
	(2,'DuocSuDung');
INSERT INTO LoaiConVat (IDLoaiConVat, TenConVat, TrangThaiConVat) VALUES
(1, 'Cho', 'BinhThuong'),
(2, 'Meo', 'BinhThuong'),
(3, 'Chim', 'BinhThuong'),
(4, 'Ca', 'BinhThuong'),
(5, 'Tho', 'BinhThuong'),
(6, 'Rua', 'BinhThuong'),
(7, 'Trau', 'BinhThuong'),
(8, 'Ngan', 'BinhThuong'),
(9, 'Ngua', 'BinhThuong'),
(10, 'BoDuc', 'BinhThuong'),
(11, 'Lon', 'BinhThuong'),
(12, 'Ga', 'BinhThuong'),
(13, 'Vit', 'BinhThuong'),
(14, 'Ngong', 'BinhThuong'),
(15, 'De', 'BinhThuong'),
(16, 'Cuu', 'BinhThuong'),
(17, 'Lac da', 'BinhThuong'),
(18, 'Lua', 'BinhThuong'),
(19, 'Chim canh cut', 'BinhThuong'),
(20, 'Chim cong', 'BinhThuong'),
(21, 'Chim se', 'CanBaoTon'),
(22, 'Chim bo cau', 'CanBaoTon'),
(23, 'Chim hoang yen', 'CanBaoTon'),
(24, 'Chim sao', 'CanBaoTon'),
(25, 'Chim chich choe', 'CanBaoTon'),
(26, 'Chim chao mao', 'CanBaoTon'),
(27, 'Chim cu gay', 'CanBaoTon'),
(28, 'Chim dai bang', 'CanBaoTon'),
(29, 'Chim ung', 'CanBaoTon'),
(30, 'Chim cu meo', 'CanBaoTon'),
(31, 'Chim vet', 'CanBaoTon'),
(32, 'Chim sao da', 'CanBaoTon'),
(33, 'Chim se ngo', 'CanBaoTon'),
(34, 'Chim se dong', 'CanBaoTon'),
(35, 'Chim se nau', 'CanBaoTon'),
(36, 'Chim se vang', 'CanBaoTon'),
(37, 'Chim se do', 'CanBaoTon'),
(38, 'Chim se xanh', 'CanBaoTon'),
(39, 'Chim se trang', 'CanBaoTon'),
(40, 'Chim se den', 'CanBaoTon'),
(41, 'Lon i', 'CamXuatKhau'),
(42, 'Lon Chu Prong', 'CamXuatKhau'),
(43, 'Lon Lang Hong', 'CamXuatKhau'),
(44, 'Ga Ho', 'CamXuatKhau'),
(45, 'Ga long chan', 'CamXuatKhau'),
(46, 'Ga lun Cao Son', 'CamXuatKhau'),
(47, 'Vit Muong Khieng', 'CamXuatKhau'),
(48, 'Vit Bau Quy', 'CamXuatKhau'),
(49, 'Ngo Mua Luong', 'CamXuatKhau'),
(50, 'Trau Quang Ngan', 'CamXuatKhau');

-- tạo công ty thuộc xã 
-- Khai báo các biến
DECLARE @i INT = 1;
DECLARE @prefix NVARCHAR(50);
DECLARE @middle NVARCHAR(50);
DECLARE @suffix NVARCHAR(50);
DECLARE @CanBoNghiepVuName NVARCHAR(255);

-- Bảng tạm để lưu trữ các phần của tên
CREATE TABLE #Parts (Part NVARCHAR(50));

-- Chèn các tiền tố
INSERT INTO #Parts VALUES ('Phu '), ('Thinh '), ('Hung '), ('Loc '), ('Minh '), ('Dai '), ('Tan '),('An '),('Khang '),('Viet '),('Son '),('Hai ');
-- Thêm các trung tố
INSERT INTO #Parts VALUES ('Thanh '), ('Dat '), ('Phat '),  ('Thien '),('Phong '),('Hoa '),('Tien '),('Ton '),('Hoa '),('Khanh ');
--Thêm các hậu tố
INSERT INTO #Parts VALUES ('Bac '), ('Trung '),('Nam ') , ('Gia '),('Doanh '),('Thuc '),('Hong '), ('Binh '), ('Quan ');

-- Vòng lặp để tạo và chèn tên công ty
WHILE @i <= 268  -- Thay đổi số lượng tùy ý
BEGIN
    -- Lấy ngẫu nhiên các phần của tên
    SELECT TOP 1 @prefix = Part FROM #Parts ORDER BY NEWID();
    SELECT TOP 1 @middle = Part FROM #Parts ORDER BY NEWID();
    SELECT TOP 1 @suffix = Part FROM #Parts ORDER BY NEWID();

    -- Ghép các phần lại thành tên công ty
    SET @CanBoNghiepVuName = @prefix + @middle + @suffix;

    -- Kiểm tra xem tên đã tồn tại chưa, nếu rồi thì bỏ qua và tạo tên khác.
    IF NOT EXISTS (SELECT 1 FROM CongTy WHERE Ten = @CanBoNghiepVuName)
    BEGIN
    -- Chèn dữ liệu vào bảng CongTy
    INSERT INTO CongTy (IDCongTy,Ten, CapTrucThuoc, IDLinhVuc, Username, Password, Banned) VALUES (@i, @CanBoNghiepVuName ,2, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, CONCAT('user', CAST(@i AS NVARCHAR)), CONCAT('pass', CAST(@i AS NVARCHAR)), 0);
    --Tăng biến đếm
    SET @i = @i + 1;
    END;
END

DROP TABLE #Parts;
-- Bước 1: Tạo danh sách các xã hợp lệ
DECLARE @SoXa INT = (SELECT COUNT(*) FROM Xa WHERE IDXa != 0);
DECLARE @SoCongTy INT = (SELECT COUNT(*) FROM CongTy WHERE IDXa IS NULL);

-- Kiểm tra tính khả dụng: mỗi xã cần có đúng 2 công ty
IF @SoCongTy = @SoXa * 2
BEGIN
    -- Bước 2: Lấy danh sách công ty chưa có IDXa
    WITH CongTyNull AS (
        SELECT IDCongTy, ROW_NUMBER() OVER (ORDER BY IDCongTy) AS RowNum
        FROM CongTy
        WHERE IDXa IS NULL
    ),
    XaList AS (
        SELECT IDXa, ROW_NUMBER() OVER (ORDER BY IDXa) AS RowNum
        FROM Xa
        WHERE IDXa != 0
    )
    -- Bước 3: Ghép công ty vào xã
    UPDATE CongTy
    SET IDXa = XaList.IDXa,
        IDHuyen = (SELECT TrucThuocHuyen FROM Xa WHERE Xa.IDXa = XaList.IDXa)
    FROM CongTyNull
    JOIN XaList
        ON (CongTyNull.RowNum - 1) / 2 + 1 = XaList.RowNum
    WHERE CongTy.IDCongTy = CongTyNull.IDCongTy;
END
ELSE
    PRINT 'Số công ty không khớp với số xã * 2. Kiểm tra dữ liệu!';


--tạo congty thuộc huyện
INSERT INTO CongTy (IDCongTy, Ten, IDHuyen, CapTrucThuoc, IDLinhVuc, IDXa, Username, Password, Banned) VALUES
(269, 'xay dung thanh cong', 1, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 269), CONCAT('pass', 269), 0),
(270, 'thuc pham an binh', 2, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 270), CONCAT('pass', 270), 0),
(271, 'van tai viet thanh', 3, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 271), CONCAT('pass', 271), 0),
(272, 'co phan hoa chat', 4, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 272), CONCAT('pass', 272), 0),
(273, 'dien luc mien bac', 5, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 273), CONCAT('pass', 273), 0),
(274, 'nong nghiep viet', 6, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 274), CONCAT('pass', 274), 0),
(275, 'tnhh kim loai', 7, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 275), CONCAT('pass', 275), 0),
(276, 'may mac ha noi', 8, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 276), CONCAT('pass', 276), 0),
(277, 'xuat nhap khau nam a', 1, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 277), CONCAT('pass', 277), 0),
(278, 'san xuat may moc', 2, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 278), CONCAT('pass', 278), 0),
(279, 'kinh doanh thanh phat', 3, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 279), CONCAT('pass', 279), 0),
(280, 'cong nghe so', 4, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 280), CONCAT('pass', 280), 0),
(281, 'gach men viet nam', 5, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 281), CONCAT('pass', 281), 0),
(282, 'che bien thuy san', 6, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 282), CONCAT('pass', 282), 0),
(283, 'in an va quang cao', 7, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 283), CONCAT('pass', 283), 0),
(284, 'phan bon phuong dong', 8, 1, (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LinhVuc)) + 1, 0, CONCAT('user', 284), CONCAT('pass', 284), 0);


UPDATE CongTy
SET SDT = 
    CONCAT(
        '0',                                                      -- Chữ số đầu tiên là 0
        CASE 
            WHEN FLOOR(RAND(CHECKSUM(NEWID())) * 2) = 0 THEN '3'  -- Chữ số thứ 2 là 3 hoặc 9
            ELSE '9'
        END,
        RIGHT('00000000' + CAST(ABS(CHECKSUM(NEWID())) % 100000000 AS NVARCHAR), 8)  -- 8 chữ số ngẫu nhiên còn lại
    )
WHERE SDT IS NULL;

INSERT INTO NguyenLieu (IDNguyenLieu, TenNguyenLieu, IDTrangThaiNguyenLieu) VALUES
(0, 'khong san xuat', null),
(1, 'Carbuterol', 1),
(2, 'Cimaterol', 1),
(3, 'Clenbuterol', 1),
(4, 'Chloramphenicol', 1),
(5, 'Diethylstilbestrol (DES)', 1),
(6, 'Dimetridazole', 1),
(7, 'Fenoterol', 1),
(8, 'Furazolidon va dan xuat nitrofuran', 1),
(9, 'Isoxuprin', 1),
(10, 'Methyl-testosterone', 1),
(11, 'Metronidazole', 1),
(12, '19 Nor-testosterone', 1),
(13, 'Salbutamol', 1),
(14, 'Terbutaline', 1),
(15, 'Stilbenes', 1),
(16, 'Melamine lon hon 2,5 mg tren kg ', 1),
(17, 'Bacitracin Zn', 1),
(18, 'Carbadox', 1),
(19, 'Olaquindox', 1),
(20, 'Vat Yellow 1 ' , 1),
(21, 'Vat Yellow 2 ', 1),
(22, 'Vat Yellow 3 ', 1),
(23, 'Vat Yellow 4 ', 1),
(24, 'Auramine ', 1),
(25, 'Cysteamine', 1),
(26, 'Cam ngu coc', 2),
(27, 'Khoai lang', 2),
(28, 'San', 2),
(29, 'Dau nanh', 2),
(30, 'Bot ca', 2),
(31, 'Bot dau', 2),
(32, 'Dau thuc vat', 2),
(33, 'Mo dong vat', 2),
(34, 'Vitamin A', 2),
(35, 'Canxi', 2),
(36, 'Phot pho', 2),
(37, 'Natri', 2),
(38, 'Kali', 2),
(39, 'Magie', 2),
(40, 'Rom ra', 2),
(41, 'Co kho', 2),
(42, 'Co voi', 2),
(43, 'Ri mat duong', 2),
(44, 'Ba bia', 2),
(45, 'Enzyme tieu hoa', 2),
(46, 'Khang sinh', 2);

-- Update records based on IDLinhVuc
UPDATE CongTy
SET
  IDNguonGenGiong = CASE
    WHEN IDLinhVuc = 1 THEN 0
    WHEN IDLinhVuc = 2 THEN (ABS(CHECKSUM(NEWID())) % ((SELECT COUNT(*) FROM NguonGenGiong) - 1)) + 1
    ELSE 0
  END,
  IDThucAnChanNuoi = CASE
    WHEN IDLinhVuc = 1 THEN 0
    WHEN IDLinhVuc = 3 THEN (ABS(CHECKSUM(NEWID())) % ((SELECT COUNT(*) FROM ThucAnChanNuoi) - 1)) + 1
    ELSE 0
   END,
  IDGiongVatNuoi = CASE
    WHEN IDLinhVuc = 1 THEN (ABS(CHECKSUM(NEWID())) % ((SELECT COUNT(*) FROM GiongVatNuoi) - 1)) + 1
    ELSE 0
  END,
   IDLoaiConVat = (ABS(CHECKSUM(NEWID())) % (SELECT COUNT(*) FROM LoaiConVat)) + 1,
   SoLuong = (ABS(CHECKSUM(NEWID())) % 1000) + 100;
UPDATE CongTy
SET IDLoaiConVat = CASE 
    WHEN IDLinhVuc = 1 AND IDGiongVatNuoi = 3 THEN 
        CASE 
            WHEN (ABS(CHECKSUM(NEWID())) % 3) = 0 THEN 7
            WHEN (ABS(CHECKSUM(NEWID())) % 3) = 1 THEN 10
            ELSE 12
        END
    ELSE IDLoaiConVat
END
WHERE IDLinhVuc = 1 AND IDGiongVatNuoi = 3;
UPDATE CongTy
SET IDNguyenLieu = 
    CASE 
        WHEN IDThucAnChanNuoi IN (1, 2) THEN ABS(CHECKSUM(NEWID())) % 46 + 1
        ELSE 0
    END;


-- Check 
--SELECT * FROM CongTy
--insert into CanBoNghiepVu(CanBoNghiepVuID, Username, Password, isAdmin) values (0, 'admin', 'admin', 1);



