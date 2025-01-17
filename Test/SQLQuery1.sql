
USE contact;

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
	TrucThuocHuyen INT NOT NULL
	);

INSERT INTO Xa (IDXa, TenXa, TrucThuocHuyen) VALUES
(0, 'Khong thuoc xa', 0),
-- Ninh Binh
(101, 'Dong Thanh', 1),
(102, 'Nam Binh', 1),
(103, 'Nam Thanh', 1),
(104, 'Ninh Khanh', 1),
(105, 'Ninh Phong', 1),
(106, 'Tan Thanh', 1),
(107, 'Van Giang', 1),
(108, 'Bich Dao', 1),
(109, 'Dong Huong', 1),
(110, 'Ninh Nhat', 1),
(111, 'Ninh Tien', 1),
(112, 'Ninh Phuc', 1),

-- Tam Diep
(201, 'Bac Son', 2),
(202, 'Nam Son', 2),
(203, 'Trung Son', 2),
(204, 'Tan Binh', 2),
(205, 'Tan Thanh', 2),
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
(319, 'Gia Son', 3),
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
(507, 'Dong Huong', 5),
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
(520, 'Tan Thanh', 5),
(521, 'Thuong Kiem', 5),
(522, 'Van Hai', 5),
(523, 'Xuan Chinh', 5),
(524, 'Yen Loc', 5),

-- Nho Quan
(601, 'Nho Quan', 6),
(602, 'Cuc Phuong', 6),
(603, 'Dong Phong', 6),
(604, 'Duc Long', 6),
(605, 'Gia Lam', 6),
(606, 'Gia Son', 6),
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
(626, 'Yen Thanh', 6),

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
(808, 'Yen Loc', 8),
(809, 'Yen My', 8),
(810, 'Yen Mac', 8),
(811, 'Yen Nhan', 8),
(812, 'Yen Phong', 8),
(813, 'Yen Thang', 8),
(814, 'Yen Tu', 8),
(815, 'Yen Thanh', 8),
(816, 'Yen Thai', 8);

CREATE TABLE AdministratorLevel (
	LevelID INT PRIMARY KEY,
	LevelName NVARCHAR(10)
	);

INSERT INTO AdministratorLevel (LevelID, LevelName) VALUES
(1, N'Huyen'),
(2, N'Xa');

CREATE TABLE Company (
	CompanyID INT PRIMARY KEY,
	CompanyName NVARCHAR(200),
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

CREATE TABLE MoTa(
    MoTaID INT PRIMARY KEY NOT NULL,
	NoiDung NVARCHAR(200),
	)

--Tạo bảng LinhVuc
CREATE TABLE LinhVuc (
    IDLinhVuc INT PRIMARY KEY NOT NULL , 
    IDCongTy INT NOT NULL,                    
    MoTaID INT,                       
    VatNuoi NVARCHAR(100),                    
    SoLuong INT,                              
    KetQua NVARCHAR(100),                     
	FOREIGN KEY (MoTaID) REFERENCES MoTa(MoTaID)
);
-- Tạo bảng CongTy
CREATE TABLE CongTy (
    IDCongTy INT PRIMARY KEY NOT NULL, 
    Ten NVARCHAR(255) NOT NULL,              
    Xa NVARCHAR(255),                       
    SDT VARCHAR(15),                         
    IDLinhVuc INT,                           -- ID lĩnh vực (khóa ngoại)
    FOREIGN KEY (IDLinhVuc) REFERENCES LinhVuc(IDLinhVuc) -- Liên kết tới IDLinhVuc trong bảng LinhVuc
);

CREATE TABLE GiongBaoTon ( -- danh sách vật nuôi cần bảo tồn / cấm xuất khẩu
        IDGiong INT PRIMARY KEY NOT NULL,
		VatNuoi NVARCHAR(50),
		MoTaID INT,
		FOREIGN KEY (MoTaID) REFERENCES MoTa(MoTaID)
		);
CREATE TABLE NguyenLieu (-- danh sách hóa chất, sản phẩm, nguyên liệu bị cấm sử dụng / được phép sử dụng)
        IDNguyenLieu INT PRIMARY KEY NOT NULL,
		Ten NVARCHAR(50),
		MoTaID INT,
		FOREIGN KEY (MoTaID) REFERENCES MoTa(MoTaID)
		);

