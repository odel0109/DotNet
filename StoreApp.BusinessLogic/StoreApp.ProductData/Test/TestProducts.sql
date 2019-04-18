SET IDENTITY_INSERT Product ON;
DELETE FROM Product

INSERT INTO Product(ProductID, Price, ActiveFlag, DefaultName, Category_CategoryID, NameMessageID, DescriptionMessageID)
VALUES				(1,			10.5,		1,		'Jacket',			1,				10000,			10001			),
					(2,			11.3,		1,		'Nike sneakers',	2,				10002,			10003			),
					(3,			999,		1,		'Gold Ring',		3,				10004,			10005			),
					(4,			178.9,		1,		'Sofa',				4,				10006,			10007			),
					(5,			5.3,		1,		'Collar',			5,				10008,			10009			),
					(6,			1.5,		1,		'Office paper',		6,				10010,			10011			),
					(7,			199.5,		1,		'Termos',			6,				10012,			10013			)


SET IDENTITY_INSERT Product OFF;