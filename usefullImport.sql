create database AgentsSales

create table Agent
(
	agentID int primary key identity,
	companyName nvarchar(max) null,
	agentType nvarchar(max) null,
	legalAddress nvarchar(max) null,
	INN int null,
	KPP int null,
	directorFullName nvarchar(max) null,
	agentPhoneNumber int null,
	agentEmail nvarchar(max) null,
	logo nvarchar(max) null,
	[priority] nvarchar(max) null,
	salePoints nvarchar(max) null,
	historyOfSales nvarchar(max) null
)

create table Worker
(
	workerID int primary key identity,
	workerFullName nvarchar(max) null,
	birthDate date null,
	passportInfo nvarchar(max) null,
	bankDetails nvarchar(max) null,
	familyInfo nvarchar(max) null,
	heathProblems nvarchar(max) null,
	specialization nvarchar(max) null
)

create table Product
(
	productID int primary key identity,
	productName nvarchar(max) null,
	productDescription nvarchar(max) null,
	productWeigth int null,
	productPackageHeight int null,
	productPackageWidth int null,
	productPackageLength int null,
	productPackageWeigth int null,
	qualityCertificate nvarchar(max) null,
	GOST nvarchar(max) null,
	vendorCode int null,
	minCostForAgent int null,
	productImage nvarchar(max) null,
	productType nvarchar(max) null,
	amountOfWorkersForProduction int null,
	manufactoryID int null,
)

create table Material
(
	materialID int primary key identity,
	materialName nvarchar(max) null,
	materialType nvarchar(max) null,
	amountInPackage int null,
	unitType nvarchar(max) null,
	amountInStock int null,
	minPossibleAmount int null,
	cost int null,
)

create table [Provider]
(
	providerID int primary key identity,
	providerName nvarchar(max) null,
	INN int null,
	providerType nvarchar(max) null,
	deliveryHistory nvarchar(max) null
)

create table [ProductMaterial]
(
	productID int foreign key references [Product](productID),
	materialID int foreign key references [Material](materialID),
	requiredAmountOfMaterial int null,
)

insert into ProductMaterial (productID, materialID, requiredAmountOfMaterial)
select Product.productID, Material.materialID, [Лист1$].[Необходимое количество материала]
from Product, Material, [Лист1$]
where [Лист1$].[Продукция] = Product.productName and [Лист1$].[Наименование материала] = Material.materialName


select * from [ProductMaterial]