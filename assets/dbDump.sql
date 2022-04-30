create table `develop2020`.`alt2022_booking` (
	`ID` INT NOT NULL AUTO_INCREMENT,
    `NAME_CLIENT` VARCHAR(60),
    `DT_START` DATE NOT NULL,
    `DT_END` DATE NOT NULL,
    `DT_RESERVATION` DATE NOT NULL,
    `ACTIVE` BIT,
    PRIMARY KEY (`ID`)
);

CREATE PROCEDURE STP_ALT2022_GET_AVAILABILITY(Pinitial_date date, Pfinal_date date)
BEGIN
	select * from alt2022_booking where DT_START >= Pinitial_date and DT_START <= Pfinal_date;
END;