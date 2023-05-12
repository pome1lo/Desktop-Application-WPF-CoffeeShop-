INSERT INTO public."ProductTypes" ("Id", "Name") VALUES 
	(1, 'Refreashes'),
	(2, 'Meal'),
	(3, 'Coffee')

INSERT INTO public."Descriptions" ("TotalFat", "SaturatedFat", "TransFat", "Cholesterol", "Sodium", "TotalCarbohydrates", "Protein", "Caffeine") VALUES
	(110, 120, 130, 140, 150, 160, 170, 180),
	(190, 200, 210, 220, 230, 240, 250, 260),
    (290, 300, 50, 60, 70, 80, 90, 100),
	(270, 280, 290, 300, 50, 60, 70, 80),
	(90, 100, 110, 120, 130, 140, 150, 160),
	(170, 180, 190, 200, 210, 220, 230, 240),
	(250, 260, 270, 280, 290, 300, 50, 60),
	(70, 80, 90, 100, 110, 120, 130, 140),
	(150, 160, 170, 180, 190, 200, 210, 220),
	(230, 240, 250, 260, 270, 280, 290, 300),
	(50, 60, 70, 80, 90, 100, 110, 120),
	(250, 260, 270, 280, 290, 300, 50, 60),
	(70, 80, 90, 100, 110, 120, 130, 140),
	(150, 160, 170, 180, 190, 200, 210, 220),
	(230, 240, 250, 260, 270, 280, 290, 300),
	(50, 60, 70, 80, 90, 100, 110, 120)
	

INSERT INTO public."Products" ("Name", "Img", "Price", "Calories", "ProductTypeId", "DescriptionId") VALUES
	
	('Caffe Misto', '\StaticFiles\Img\CaffeMisto.png', 168, 245, 3, 1), 
	('Caramel Macchiato', '\StaticFiles\Img\CaramelMacchiato.png', 254, 145, 3, 2), 
	('Chestnut Latte', '\StaticFiles\Img\ChestnutLatte.png', 129, 195, 3, 3), 
	('Chocolate Espresso', '\StaticFiles\Img\ChocolateEspresso.png', 208, 145, 3, 4), 
	('Cinnamon Latte', '\StaticFiles\Img\CinnamonLatte.png', 237, 145, 3, 5), 
	('Cold Brew', '\StaticFiles\Img\ColdBrew.png', 182, 199, 3, 6), 
	('Vanilla Sweet', '\StaticFiles\Img\VanillaSweet.png', 162, 211, 3, 7), 
	('Veranda Blend', '\StaticFiles\Img\VerandaBlend.png', 109, 230, 3, 8), 
	('White Chocolate', '\StaticFiles\Img\WhiteChocolate.png', 172, 360, 3, 9), 
	
--	('', '\StaticFiles\Img\ProfileDefault.', , 145, , 1), 
--	('', '\StaticFiles\Img\ProfileDefault.', , 145, , 1), 
	
	('Acai Lemonade', '\StaticFiles\Img\AcaiLemonade.png', 154, 145, 1, 10), 
	('Strawberry Acai', '\StaticFiles\Img\StrawberryAcai.png', 199, 145, 1, 11), 
	('Star Drink', '\StaticFiles\Img\StarDrink.png', 189, 145, 1, 12), 
	('Mango Dragonfruit', '\StaticFiles\Img\MangoDragonfruit.png', 177, 145, 1, 13), 
	('Kiwi Starfruit', '\StaticFiles\Img\KiwiStarfruit.png', 185, 145, 1, 14),
	('Dragon Drink', '\StaticFiles\Img\DragonDrink.png', 165, 145, 1, 15) 
