(localdb)\mssqllocaldb

CREATE TABLE Customers (
    Id int IDENTITY(1,1) not null,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Deleted bit not null default 0,
    FirstName nvarchar(max)  NULL,
    LastName nvarchar(max)  NULL,
    Email nvarchar(255) not NULL,
    MobilePhone nvarchar(15) not NULL,
    Phone nvarchar(15) NULL,
    CustomerType int not null default 0,
    CONSTRAINT PK_Customers_Id PRIMARY KEY CLUSTERED (Id)
);

CREATE TABLE CustomerAddresses (
    Id int IDENTITY(1,1) not null,
    CustomersId int not null,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Deleted bit not null default 0,
    AddressLine1 nvarchar(255) NULL,
    AddressLine2 nvarchar(255) NULL,
    ZipCode nvarchar(255) NULL,
    City nvarchar(255) NULL,
    Country nvarchar(255) NULL,
    State nvarchar(255) NULL,
    MobilePhone nvarchar(15) NULL,
    Phone nvarchar(15) NULL,
    CustomerType int not null default 0,
    CONSTRAINT PK_CustomerAddresses_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK__CustomerAddresses_CustomersId FOREIGN KEY (CustomersId) REFERENCES  Customers(Id),
);


CREATE TABLE Appointments (
    Id int IDENTITY(1,1) not null,
    StartDate datetime not null DEFAULT GETDATE(),
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    Description nvarchar(max) NOT NULL,
    CONSTRAINT PK_Appointments_Id PRIMARY KEY CLUSTERED (Id)
);


CREATE TABLE Brands (
    Id int IDENTITY(1,1) not null,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    CONSTRAINT PK_Brands_Id PRIMARY KEY CLUSTERED (Id)
);

CREATE TABLE PressReleases (
    Id int IDENTITY(1,1) not null,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    Description nvarchar(max) NOT NULL,
    MediaPath nvarchar(max) NULL,
    CONSTRAINT PK_PressReleases_Id PRIMARY KEY CLUSTERED (Id)
);

CREATE TABLE Media (
    Id int IDENTITY(1,1) not null,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    MediaPath nvarchar(max) NOT NULL,
    CONSTRAINT PK_Media_Id PRIMARY KEY CLUSTERED (Id)
);

CREATE TABLE Tags (
    Id int IDENTITY(1,1) not null,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    CONSTRAINT PK_Tags_Id PRIMARY KEY CLUSTERED (Id)
);

CREATE TABLE Features (
    Id int IDENTITY(1,1) not null,
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
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    CONSTRAINT PK_FeatureAttributes_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_FeatureAttributes_FeatureId FOREIGN KEY (FeatureId) REFERENCES  Features(Id),
);


CREATE TABLE BlogCategories (
    Id int IDENTITY(1,1) not null,
    Hero nvarchar(max) null,
    ParentId int null,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    CONSTRAINT PK_BlogCategories_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_BlogCategories_ParentId FOREIGN KEY (ParentId) REFERENCES  BlogCategories(Id),
);


CREATE TABLE BlogPosts (
    Id int IDENTITY(1,1) not null,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    Description nvarchar(max) NOT NULL,
    CONSTRAINT PK_BlogPosts_Id PRIMARY KEY CLUSTERED (Id),
);


CREATE TABLE BlogPostsCategories (
    BlogPostId int not null,
    BlogCategoryId int not null,
    [Order] int not null  default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_BlogPostsCategories PRIMARY KEY NONCLUSTERED ([BlogCategoryId],[BlogPostId]),
    CONSTRAINT FK_BlogPostsCategories_BlogCategoryId FOREIGN KEY (BlogCategoryId) REFERENCES  BlogCategories(Id),
    CONSTRAINT FK_BlogPostsCategories_BlogPostId FOREIGN KEY (BlogPostId) REFERENCES  BlogPosts(Id),
);

CREATE TABLE BlogPostsTags (
    BlogPostId int not null,
    TagId int not null,
    [Order] int not null  default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_BlogPostsTags PRIMARY KEY NONCLUSTERED ([TagId],[BlogPostId]),
    CONSTRAINT FK_BlogPostsTags_TagId FOREIGN KEY (TagId) REFERENCES  Tags(Id),
    CONSTRAINT FK_BlogPostsTags_BlogPostId FOREIGN KEY (BlogPostId) REFERENCES  BlogPosts(Id),
);


CREATE TABLE BlogPostsMedia (
    BlogPostId int not null,
    MediaId int not null,
    [Order] int not null  default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_BlogPostsMedia PRIMARY KEY NONCLUSTERED ([MediaId],[BlogPostId]),
    CONSTRAINT FK_BlogPostsMedia_MediaId FOREIGN KEY (MediaId) REFERENCES  Media(Id),
    CONSTRAINT FK_BlogPostsMedia_BlogPostId FOREIGN KEY (BlogPostId) REFERENCES  BlogPosts(Id),
);

CREATE TABLE EventLocations (
    Id int IDENTITY(1,1) not null,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    CONSTRAINT PK_EventLocations_Id PRIMARY KEY CLUSTERED (Id)
);

CREATE TABLE Events (
    Id int IDENTITY(1,1) not null,
    EventLocationId int not null,
    StartDate datetime not null DEFAULT GETDATE(),
    EndDate datetime not null DEFAULT GETDATE(),
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    Description nvarchar(max) NOT NULL,
    CONSTRAINT PK_Events_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Events_EventsLocationId FOREIGN KEY (EventLocationId) REFERENCES  EventLocations(Id),
);

CREATE TABLE EventMedia (
    EventId int not null,
    MediaId int not null,
    [Order] int not null  default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_EventMedia PRIMARY KEY NONCLUSTERED ([MediaId],[EventId]),
    CONSTRAINT FK_EventMedia_MediaId FOREIGN KEY (MediaId) REFERENCES  Media(Id),
    CONSTRAINT FK_EventMedia_EventId FOREIGN KEY (EventId) REFERENCES  Events(Id),
);

CREATE TABLE EventTags (
    EventId int not null,
    TagId int not null,
    [Order] int not null  default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_EventTags PRIMARY KEY NONCLUSTERED ([EventId],[TagId]),
    CONSTRAINT FK_EventTags_TagId FOREIGN KEY (TagId) REFERENCES  Tags(Id),
    CONSTRAINT FK_EventTags_EventId FOREIGN KEY (EventId) REFERENCES  Events(Id),
);


CREATE TABLE ShopCategories (
    Id int IDENTITY(1,1) not null,
    Hero nvarchar(max) null,
    ParentId int null,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    CONSTRAINT PK_ShopCategories_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_ShopCategories_ParentId FOREIGN KEY (ParentId) REFERENCES  ShopCategories(Id),
);


CREATE TABLE ShopCategoryFeatures (
    ShopCategoryId int not null,
    FeatureId int not null,
    [Order] int not null  default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_ShopCategoryFeatures PRIMARY KEY NONCLUSTERED ([ShopCategoryId],[FeatureId]),
    CONSTRAINT FK_ShopCategoryFeatures_ShopCategoryId FOREIGN KEY (ShopCategoryId) REFERENCES  ShopCategories(Id),
    CONSTRAINT FK_ShopCategoryFeatures_FeatureId FOREIGN KEY (FeatureId) REFERENCES  Features(Id),
);





CREATE TABLE Products (
    Id int IDENTITY(1,1) not null,
    Hero nvarchar(max) null,
    ParentId int null,
    BrandId int not null,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Featured bit not null default 0,
    Price  decimal(19,10) not null default 0,
    Deleted bit not null default 0,
    Title nvarchar(max) NOT NULL,
    Description nvarchar(max) NULL,
    CONSTRAINT PK_Products_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Products_ParentId FOREIGN KEY (ParentId) REFERENCES  Products(Id),
    CONSTRAINT FK_Products_BrandId FOREIGN KEY (BrandId) REFERENCES  Brands(Id),
);


CREATE TABLE ProductShopCategories (
    ShopCategoryId int not null,
    ProductId int not null,
    [Order] int not null  default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_ProductShopCategories PRIMARY KEY NONCLUSTERED ([ShopCategoryId],[ProductId]),
    CONSTRAINT FK_ProductShopCategories_ShopCategoryId FOREIGN KEY (ShopCategoryId) REFERENCES  ShopCategories(Id),
    CONSTRAINT FK_ProductShopCategories_ProductId FOREIGN KEY (ProductId) REFERENCES  Products(Id),
);

CREATE TABLE ProductMedia (
    MediaId int not null,
    ProductId int not null,
    [Order] int not null  default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_ProductMedia PRIMARY KEY NONCLUSTERED ([MediaId],[ProductId]),
    CONSTRAINT FK_ProductMedia_MediaId FOREIGN KEY (MediaId) REFERENCES  Media(Id),
    CONSTRAINT FK_ProductMedia_ProductId FOREIGN KEY (ProductId) REFERENCES  Products(Id),
);

CREATE TABLE ProductAttributes (
    FeatureAttributesId int not null,
    ProductsId int not null,
    [Order] int not null  default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_ProductAttributes PRIMARY KEY NONCLUSTERED ([FeatureAttributesId],[ProductsId]),
    CONSTRAINT FK_ProductAttributes_FeatureAttributesId FOREIGN KEY (FeatureAttributesId) REFERENCES  FeatureAttributes(Id),
    CONSTRAINT FK_ProductAttributes_ProductsId FOREIGN KEY (ProductsId) REFERENCES  Products(Id),
);


CREATE TABLE ProductTags (
    ProductId int not null,
    TagId int not null,
    [Order] int not null default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_ProductTags PRIMARY KEY NONCLUSTERED ([TagId],[ProductId]),
    CONSTRAINT FK_ProductTags_TagId FOREIGN KEY (TagId) REFERENCES  Tags(Id),
    CONSTRAINT FK_ProductTags_ProductId FOREIGN KEY (ProductId) REFERENCES  Products(Id),
);


CREATE TABLE Banners (
    Id int IDENTITY(1,1) not null,
    [Order] int not null default 0,
    Location int not null,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    ImagePath nvarchar(max) NULL,
    Code nvarchar(max) NULL,
    CONSTRAINT PK_Banners_Id PRIMARY KEY CLUSTERED (Id),
);


CREATE TABLE BannerStatistics (
    Id int IDENTITY(1,1) not null,
    SessionId nvarchar(max) null,
    BannerId int not null,
    CreatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_BannerStatistics_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_BannerStatistics_BannerId FOREIGN KEY (BannerId) REFERENCES  Banners(Id),
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


CREATE TABLE OfferDetails (
    Id int IDENTITY(1,1) not null,
    [Order] int not null default 0,
    OfferId int not null,
    ProductId int not null,
    Price decimal(19,10) default 0,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    Published bit not null default 0,
    Deleted bit not null default 0,
    CONSTRAINT PK_OfferDetails_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_OfferDetails_ProductId FOREIGN KEY (ProductId) REFERENCES  Products(Id),
    CONSTRAINT FK_OfferDetails_OfferId FOREIGN KEY (OfferId) REFERENCES  Offers(Id),
);



CREATE TABLE ShoppingCartItems (
   Id int IDENTITY(1,1) not null,
   ProductId int not null,
   Quantity int not null,
   SessionCartId nvarchar(Max) not null,
   CreatedAt datetime not null DEFAULT GETDATE(),
   CONSTRAINT PK_ShoppingCartItems_Id PRIMARY KEY CLUSTERED (Id),
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

CREATE TABLE OrderDetails (
    Id int IDENTITY(1,1) not null,
    OrderId int not null,
    ProductId int not null,
    Quantity int not null,
    Price decimal(19,10) not null,
    CreatedAt datetime not null DEFAULT GETDATE(),
    UpdatedAt datetime not null DEFAULT GETDATE(),
    CONSTRAINT PK_Id PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_OrderDetails_ProductId FOREIGN KEY (ProductId) REFERENCES  Products(Id),
    CONSTRAINT FK_OrderDetails_OrderId FOREIGN KEY (OrderId) REFERENCES  Orders(Id)
);
