﻿DROP TABLE IF EXISTS schema_version;
CREATE TABLE schema_version
(
	version INT PRIMARY KEY,
	file_name VARCHAR(64) NOT NULL,
	update_date DATETIME NOT NULL
);