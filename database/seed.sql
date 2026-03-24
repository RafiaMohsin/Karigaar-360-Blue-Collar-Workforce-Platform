
INSERT INTO Customers (FullName, Phone, Email, Address, PasswordHash) VALUES
('Ahmed Khan', '03001234567', 'ahmed.khan@email.com', '123 Main Street, Karachi', '$2a$10$sampleHashedPassword1'),
('Fatima Ali', '03015678901', 'fatima.ali@email.com', '456 Park Avenue, Lahore', '$2a$10$sampleHashedPassword2'),
('Muhammad Hassan', '03025234789', 'mhasan@email.com', '789 University Road, Islamabad', '$2a$10$sampleHashedPassword3'),
('Ayesha Malik', '03012345678', 'ayesha.malik@email.com', '321 Garden Lane, Multan', '$2a$10$sampleHashedPassword4'),
('Ali Raza', '03008765432', 'ali.raza@email.com', '654 Market Street, Peshawar', '$2a$10$sampleHashedPassword5'),
('Zainab Ahmed', '03109876543', 'zainab.ahmed@email.com', '987 Civic Center, Quetta', '$2a$10$sampleHashedPassword6'),
('Hassan Abbas', '03217654321', 'hassan.abbas@email.com', '111 Commercial Road, Faisalabad', '$2a$10$sampleHashedPassword7'),
('Sara Khan', '03334567890', 'sara.khan@email.com', '222 Business Park, Rawalpindi', '$2a$10$sampleHashedPassword8');


INSERT INTO Workers (FullName, Phone, Profession, ExperienceYears, Location, PasswordHash, Rating, TotalJobsCompleted, IsAvailable) VALUES
('Tariq Ahmed', '03001111111', 'Plumber', 8, 'Karachi', '$2a$10$workerHash1', 4.8, 45, 1),
('Muhammad Ali', '03002222222', 'Electrician', 12, 'Lahore', '$2a$10$workerHash2', 4.9, 78, 1),
('Usman Khan', '03003333333', 'Carpenter', 10, 'Islamabad', '$2a$10$workerHash3', 4.7, 62, 1),
('Bilal Ahmed', '03004444444', 'Painter', 6, 'Karachi', '$2a$10$workerHash4', 4.5, 28, 1),
('Rashid Ali', '03005555555', 'Mason', 15, 'Multan', '$2a$10$workerHash5', 4.9, 95, 0),
('Karim Khan', '03006666666', 'HVAC Technician', 9, 'Peshawar', '$2a$10$workerHash6', 4.6, 38, 1),
('Iqbal Ahmed', '03007777777', 'Plumber', 7, 'Faisalabad', '$2a$10$workerHash7', 4.4, 22, 1),
('Rafiq Ali', '03008888888', 'Electrician', 11, 'Rawalpindi', '$2a$10$workerHash8', 4.8, 71, 1),
('Nasir Khan', '03009999999', 'Carpenter', 13, 'Lahore', '$2a$10$workerHash9', 4.7, 89, 0),
('Samir Ahmed', '03010101010', 'Painter', 5, 'Karachi', '$2a$10$workerHash10', 4.3, 15, 1),
('Azad Khan', '03011111111', 'Welder', 14, 'Multan', '$2a$10$workerHash11', 4.9, 102, 1),
('Faisal Ali', '03012121212', 'Plumber', 9, 'Peshawar', '$2a$10$workerHash12', 4.5, 41, 1),
('Javed Ahmed', '03013131313', 'Mason', 11, 'Faisalabad', '$2a$10$workerHash13', 4.8, 67, 1),
('Kamal Khan', '03014141414', 'HVAC Technician', 8, 'Lahore', '$2a$10$workerHash14', 4.6, 33, 0),
('Nadir Ali', '03015151515', 'Electrician', 10, 'Karachi', '$2a$10$workerHash15', 4.7, 55, 1);


INSERT INTO Jobs (Title, Description, Category, EstimatedHours, FairPrice, OfferedPrice, Location, CustomerId, CustomerName, Status, CreatedAt) VALUES


('Bathroom Plumbing Repair', 'Need to fix leaking faucet and replace water pipes in master bathroom', 'Plumbing', 3, 3000, 2500, 'Karachi', 1, 'Ahmed Khan', 'Open', datetime('now', '-5 days')),
('Home Electrical Wiring', 'Rewire entire apartment for safety upgrades and additional outlets', 'Electrical', 16, 18000, 15000, 'Lahore', 2, 'Fatima Ali', 'Open', datetime('now', '-3 days')),
('Kitchen Cabinet Repair', 'Repair and refinish wooden kitchen cabinets, cabinet doors hanging loose', 'Carpentry', 8, 8000, 6500, 'Islamabad', 3, 'Muhammad Hassan', 'Open', datetime('now', '-2 days')),
('Interior Wall Painting', 'Paint living room, bedroom, and hallway with interior paint. Include proper surface prep', 'Painting', 12, 12000, 9500, 'Karachi', 1, 'Ahmed Khan', 'Open', datetime('now', '-1 days')),
('Office AC Installation', 'Install new air conditioning unit in office space with ducting', 'HVAC', 10, 15000, 12000, 'Multan', 4, 'Ayesha Malik', 'Open', datetime('now')),
('Roof Repair and Waterproofing', 'Complete roof repair, leak assessment, and waterproofing treatment', 'Construction', 20, 25000, 20000, 'Peshawar', 5, 'Ali Raza', 'Open', datetime('now')),
('Metal Gate Fabrication', 'Design and create custom metal gate for residential entrance', 'Welding', 18, 20000, 16000, 'Multan', 2, 'Fatima Ali', 'Open', datetime('now', '-1 days')),
('Floor Tiling Installation', 'Install ceramic tiles in bathroom: 50 sq ft area including prep and grout', 'Masonry', 6, 7000, 5500, 'Faisalabad', 6, 'Zainab Ahmed', 'Open', datetime('now', '-4 days')),


('Concrete Foundation Pouring', 'Pour concrete foundation for house construction approximately 500 sq ft', 'Masonry', 12, 14000, 11000, 'Lahore', 3, 'Muhammad Hassan', 'InProgress', datetime('now', '-10 days')),
('Heavy Machinery Repair', 'Maintain and repair industrial welding equipment in manufacturing facility', 'Welding', 24, 28000, 24000, 'Karachi', 4, 'Ayesha Malik', 'InProgress', datetime('now', '-8 days')),


('Faucet Replacement', 'Replace kitchen faucet with new model including installation', 'Plumbing', 2, 2000, 1500, 'Karachi', 1, 'Ahmed Khan', 'Completed', datetime('now', '-30 days'), datetime('now', '-29 days')),
('Light Fixture Installation', 'Install 5 ceiling light fixtures throughout home with wiring', 'Electrical', 4, 4500, 3500, 'Lahore', 2, 'Fatima Ali', 'Completed', datetime('now', '-25 days'), datetime('now', '-24 days')),
('Door Frame Repair', 'Fix broken door frame and install new door in bedroom', 'Carpentry', 5, 5500, 4500, 'Islamabad', 3, 'Muhammad Hassan', 'Completed', datetime('now', '-20 days'), datetime('now', '-19 days')),
('Wall Paint Touch-up', 'Paint entire house interior with premium finish paint', 'Painting', 18, 16000, 13000, 'Multan', 4, 'Ayesha Malik', 'Completed', datetime('now', '-15 days'), datetime('now', '-14 days')),
('Heating System Installation', 'Install central heating system for 3-story building', 'HVAC', 14, 18000, 15000, 'Peshawar', 5, 'Ali Raza', 'Completed', datetime('now', '-12 days'), datetime('now', '-11 days')),
('Custom Staircase Metal Work', 'Fabricate and install custom metal railing staircase', 'Welding', 22, 22000, 18000, 'Multan', 2, 'Fatima Ali', 'Completed', datetime('now', '-10 days'), datetime('now', '-8 days')),
('Brick Wall Construction', 'Build exterior brick wall approximately 100 sq ft', 'Masonry', 16, 18000, 14500, 'Faisalabad', 6, 'Zainab Ahmed', 'Completed', datetime('now', '-7 days'), datetime('now', '-6 days')),
('Deck Wooden Structure Building', 'Build wooden deck in backyard: 200 sq ft with railings', 'Carpentry', 24, 25000, 20000, 'Rawalpindi', 7, 'Hassan Abbas', 'Completed', datetime('now', '-35 days'), datetime('now', '-33 days')),
('Swimming Pool Tile Work', 'Install pool tiles and finish swimming pool area', 'Masonry', 20, 25000, 21000, 'Karachi', 8, 'Sara Khan', 'Completed', datetime('now', '-40 days'), datetime('now', '-38 days')),
('Commercial Kitchen Electrical', 'Upgrade electrical system for commercial kitchen setup', 'Electrical', 10, 12000, 10000, 'Lahore', 1, 'Ahmed Khan', 'Completed', datetime('now', '-22 days'), datetime('now', '-21 days')),
('Bathroom Renovation Complete', 'Complete bathroom remodel including plumbing and tiling', 'Plumbing', 30, 35000, 28000, 'Multan', 3, 'Muhammad Hassan', 'Completed', datetime('now', '-18 days'), datetime('now', '-15 days')),
('Exterior House Painting', 'Paint entire exterior of house with weather-resistant paint', 'Painting', 20, 18000, 14000, 'Peshawar', 4, 'Ayesha Malik', 'Completed', datetime('now', '-28 days'), datetime('now', '-26 days'));


-- Total Customers: 8
-- Total Workers: 15
-- Total Jobs: 18 (8 Open, 2 In Progress, 12 Completed)
-- Job Categories: Plumbing, Electrical, Carpentry, Painting, HVAC, Welding, Masonry, Construction
