CREATE TABLE PressReleases (
    Id int IDENTITY(1,1) not null,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    Description nvarchar(max) NOT NULL,
    CONSTRAINT PK_PressReleases_Id PRIMARY KEY CLUSTERED (Id),
);

CREATE TABLE Events (
    Id int IDENTITY(1,1) not null,
    [Order] int not null default 0,
    StartDate datetime not null DEFAULT GETDATE(),
    EndDate datetime not null DEFAULT GETDATE(),
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    Description nvarchar(max) NOT NULL,
    CONSTRAINT PK_Events_Id PRIMARY KEY CLUSTERED (Id),
);


CREATE TABLE EventsMedia (
    Id int IDENTITY(1,1) not null,
    EventsId int not null,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    MediaPath nvarchar(max) NOT NULL,
    CONSTRAINT PK_EventsMedia_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_EventssMedia_EventsId FOREIGN KEY (EventsId) REFERENCES  Events(Id),
);



CREATE TABLE Banners (
    Id int IDENTITY(1,1) not null,
    [Order] int not null default 0,
    Location int not null,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    BannerImagePath nvarchar(max) NULL,
    BannerCode nvarchar(max) NULL,
    CONSTRAINT PK_Banners_Id PRIMARY KEY CLUSTERED (Id),
);


CREATE TABLE BannersStatistics (
    Id int IDENTITY(1,1) not null,
    SessionId nvarchar(max) null,
    BannersId int not null,
    CreatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_BannersStatistics_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_BannersStatistics_BannersId FOREIGN KEY (BannersId) REFERENCES  Banners(Id),
);


CREATE TABLE Offers (
    Id int IDENTITY(1,1) not null,
    [Order] int not null default 0,
    StartDate datetime not null DEFAULT GETDATE(),
    EndDate datetime not null DEFAULT GETDATE(),
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    Description nvarchar(max) NOT NULL,
    CONSTRAINT PK_Offers_Id PRIMARY KEY CLUSTERED (Id),
);






CREATE TABLE Categories (
    Id int IDENTITY(1,1) not null,
    Hero nvarchar(max) null,
    ParentId int null,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    CONSTRAINT PK_Categories_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Categories_ParentId FOREIGN KEY (ParentId) REFERENCES  Categories(Id),

);

CREATE TABLE Brands (
    Id int IDENTITY(1,1) not null,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    CONSTRAINT PK_Brands_Id PRIMARY KEY CLUSTERED (Id),

);


CREATE TABLE Tags (
    Id int IDENTITY(1,1) not null,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    CONSTRAINT PK_Tags_Id PRIMARY KEY CLUSTERED (Id)
);

CREATE TABLE Features (
    Id int IDENTITY(1,1) not null,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    CONSTRAINT PK_Features_Id PRIMARY KEY CLUSTERED (Id)

);

CREATE TABLE FeatureAttributes (
    Id int IDENTITY(1,1) not null,
    FeatureId int not null,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    CONSTRAINT PK_FeatureAttributes_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_FeatureAttributes_FeatureId FOREIGN KEY (FeatureId) REFERENCES  Features(Id),

);

CREATE TABLE CategoryFeatures (
    FeatureId int,
    CategoryId int,
    [Order] int not null  default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_CategoryFeatures PRIMARY KEY NONCLUSTERED ([CategoryId],[FeatureId]),
    CONSTRAINT FK_CategoryFeatures_CategoryId FOREIGN KEY (CategoryId) REFERENCES  Categories(Id),
    CONSTRAINT FK_CategoryFeatures_FeatureId FOREIGN KEY (FeatureId) REFERENCES  Features(Id),
);


CREATE TABLE Media (
    Id int IDENTITY(1,1) not null,
    [Order] int not null  default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    CONSTRAINT PK_Media_Id PRIMARY KEY CLUSTERED (Id),
    MediaPath nvarchar(max) NOT NULL
);


CREATE TABLE Products (
    Id int IDENTITY(1,1) not null,
    ParentId int null,
    BrandId int,
    Hero nvarchar(max) null,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Featured bit not null default 0,
    CONSTRAINT PK_Products_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Products_ParentId FOREIGN KEY (ParentId) REFERENCES  Categories(Id),
    CONSTRAINT FK_Products_BrandId FOREIGN KEY (BrandId) REFERENCES  Brands(Id),
    Title nvarchar(max) NOT NULL,
    Price decimal(19,10) default 0

);

CREATE TABLE OffersDetail (
    Id int IDENTITY(1,1) not null,
    [Order] int not null default 0,
    OffersId int not null,
    ProductsId int not null,
    Price decimal(19,10) default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    CONSTRAINT PK_OffersDetail_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_OffersDetail_ProductsId FOREIGN KEY (ProductsId) REFERENCES  Products(Id),
    CONSTRAINT FK_OffersDetail_OffersId FOREIGN KEY (OffersId) REFERENCES  Offers(Id),
);

CREATE TABLE ProductCategories (
    ProductId int,
    CategoryId int,
    [Order] int not null  default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_ProductCategories PRIMARY KEY NONCLUSTERED ([CategoryId],[ProductId]),
    CONSTRAINT FK_ProductCategory_CategoryId FOREIGN KEY (CategoryId) REFERENCES  Categories(Id),
    CONSTRAINT FK_ProductCategory_ProductId FOREIGN KEY (ProductId) REFERENCES  Products(Id),
);

CREATE TABLE ProductTags (
    ProductId int,
    TagId int,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_ProductTags PRIMARY KEY NONCLUSTERED ([TagId],[ProductId]),
    CONSTRAINT FK_ProductTags_TagId FOREIGN KEY (TagId) REFERENCES  Tags(Id),
    CONSTRAINT FK_ProductTags_ProductId FOREIGN KEY (ProductId) REFERENCES  Products(Id),
);

CREATE TABLE ProductFeatureAttributes (
    ProductId int,
    FeatureAttributeId int,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_ProductFeatureAttributes PRIMARY KEY NONCLUSTERED ([FeatureAttributeId],[ProductId]),
    CONSTRAINT FK_ProductFeatureAttributes_FeatureAttributeId FOREIGN KEY (FeatureAttributeId) REFERENCES  FeatureAttributes(Id),
    CONSTRAINT FK_ProductFeatureAttributes_ProductId FOREIGN KEY (ProductId) REFERENCES  Products(Id),
);


CREATE TABLE ProductMedia (
    ProductId int,
    MediaId int,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_ProductMedia PRIMARY KEY NONCLUSTERED ([MediaId],[ProductId]),
    CONSTRAINT FK_ProductMedia_MediaId FOREIGN KEY (MediaId) REFERENCES  Media(Id),
    CONSTRAINT FK_ProductMedia_ProductId FOREIGN KEY (ProductId) REFERENCES  Products(Id),
);


CREATE TABLE ShoppingCartItems (
   Id int IDENTITY(1,1) not null,
   CONSTRAINT PK_ShoppingCartItems_Id PRIMARY KEY CLUSTERED (Id),
   ProductId int not null,
   Quantity int not null,
   SessionCartId nvarchar(Max) not null,
   CreatedAt datetime not null DEFAULT GETDATE(),
   UpdatedAt datetime not null DEFAULT GETDATE(),
   CONSTRAINT FK_ShoppingCartItems_ProductId FOREIGN KEY (ProductId) REFERENCES  Products(Id),
);


CREATE TABLE Orders (
    Id int IDENTITY(1,1) not null,
    FirstName nvarchar(255) NULL,
    LastName nvarchar(255) NULL,
    AddressLine1 nvarchar(255) NULL,
    AddressLine2 nvarchar(255) NULL,
    ZipCode nvarchar(255) NULL,
    City nvarchar(255) NULL,
    Email nvarchar(255) NULL,
    Country nvarchar(255) NULL,
    State nvarchar(255) NULL,
    PhoneNumber nvarchar(255) NULL,
    OrderTotal decimal(19,10) default 0,
    OrderPlaced datetime DEFAULT GETDATE(),
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_Orders_Id PRIMARY KEY CLUSTERED (Id)
);

CREATE TABLE OrdersDetail (
    Id int IDENTITY(1,1) not null,
    OrdersId int not null,
    ProductId int not null,
    Quantity int not null,
    Price decimal(19,10) not null,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_OrdersDetail_ProductId FOREIGN KEY (ProductId) REFERENCES  Products(Id),
    CONSTRAINT FK_OrdersDetail_OrdersId FOREIGN KEY (OrdersId) REFERENCES  Orders(Id)
);
