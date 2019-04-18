SET IDENTITY_INSERT Category ON;

DELETE FROM Category

INSERT INTO Category(CategoryID, DefaultName, NameMessageID)
VALUES	(1, 'Clothes', 1),
		(2, 'Footwear', 2),
		(3, 'Accessories', 3),
		(4, 'HouseholdProducts', 4),
		(5, 'GoodsForPets', 5),
		(6, 'Other', 6)

SET IDENTITY_INSERT Category OFF;